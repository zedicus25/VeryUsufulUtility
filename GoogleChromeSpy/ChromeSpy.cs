using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleChromeSpy
{
    public class ChromeSpy
    {
        public TimeSpan MaxUseTime { get; private set; }
        public string ProcessName { get; private set; }
        public bool KeyIsCorrect => _isCorrect;
        private Task _closingTask;
        private Task _autoLoadTask;
        private string _defaultPathForCopy;
        private string _key;
        private KeyInputForm _form;
        private bool _isCorrect = false;

        public ChromeSpy(TimeSpan maxUseTime, string processName)
        {
            this.MaxUseTime = maxUseTime;
            this.ProcessName = processName;
            this._defaultPathForCopy = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            this._closingTask = new Task(ClosingProcess);
            this._autoLoadTask = new Task(AddToAutoLoad);
            this._closingTask.Start();
            this._autoLoadTask.Start();
            _key = GenerateKey();
            this._form = new KeyInputForm(_key.Length);
            this._form.KeyInsert += IsCorrect;

            Task.Factory.StartNew(() => File.WriteAllText("data.txt", _key));
        }

        private void CopyHistory()
        {
            string historyFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                @"\Google\Chrome\User Data\Default\History";

            if (File.Exists(historyFile))
            {
                if (File.Exists(Path.Combine(_defaultPathForCopy, "historyCopy")))
                    File.Delete(Path.Combine(_defaultPathForCopy, "historyCopy"));
                File.Copy(historyFile, Path.Combine(_defaultPathForCopy, "historyCopy"));
            }
        }

        private void ClosingProcess()
        {
            Process[] processes = new Process[0];
            while (true)
            {
                DateTime date = DateTime.Now;
                if (processes.Length == 0)
                {
                    try
                    {
                        processes = Process.GetProcessesByName(ProcessName);
                        if (processes.Length != 0)
                        {
                            CopyHistory();

                            processes[0].EnableRaisingEvents = true;
                            processes[0].Exited += (sender, e) => 
                            {
                                CopyHistory();
                            };
                        }

                    }
                    catch (Exception)
                    {
                    }
                }

                if (processes.Length == 0)
                    continue;



                if (date.Subtract(processes[0].StartTime) >= MaxUseTime)
                    try
                    {
                        CopyHistory();
                        foreach (var item in processes)
                        {
                            try
                            {
                                item.Kill();
                            }
                            catch (Exception)
                            {
                            }
                        }
                        processes = new Process[0];
                        _form.ShowDialog();
                        
                    }
                    catch (Exception)
                    {
                    }


                Thread.Sleep(1000);
            }
        }


        private void AddToAutoLoad()
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey myProgKey = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            string progName = "spy.exe";
            string path = Environment.CurrentDirectory + $"\\{progName}";
            myProgKey.SetValue(progName, path);
            myProgKey.Close();
        }

        private string GenerateKey()
        {
            List<string> keys = new List<string>();
            keys.Add(Environment.UserName);
            keys.Add(Environment.Version.ToString());
            keys.Add(GetMacAdress());
            keys.Add(GetIpAdress());
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keys.Count; i++)
            {
                int ind = rnd.Next(0, keys.Count);
                sb.Append(keys[ind]);
                keys.Remove(keys[ind]);
                --i;
            }
            GC.Collect(GC.GetGeneration(keys));
            GC.Collect(GC.GetGeneration(rnd));
            GC.Collect(GC.GetGeneration(sb));
            return sb.ToString();
        }

        private bool IsCorrect(string key)
        {
            _isCorrect = _key.Equals(key);
            return _key.Equals(key);
        }

        #region MAGIC
        private string GetMacAdress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }
        
        private string GetIpAdress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "";
        }
        #endregion
    }
}
