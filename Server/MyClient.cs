using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MyClient
    {

        private MyServer _server;

        public string Ip { get; private set; }
        protected TcpClient tcpClient;
        internal NetworkStream networkStream { get; set; }
        private Task _workTask;
        private string _defaulthPath;
        private FileStream fs;

        public MyClient(TcpClient tcpClient, MyServer myServer)
        {
            _server = myServer;
            _defaulthPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.tcpClient = tcpClient;
            _workTask = new Task(Work);
            _workTask.Start();
            
        }

        private void Work()
        {
            try
            {
                networkStream = tcpClient.GetStream();
                bool isFile = false;
                while (true)
                {
                    byte[] data = new byte[1024];
                    StringBuilder builder = new StringBuilder();
                    int byteCount = 0;
                    do
                    {
                        byteCount = networkStream.Read(data, 0, data.Length);
                        if (isFile)
                        {
                            fs.Write(data, 0, data.Length);
                        }
                        else
                        {
                            builder.Append(Encoding.Unicode.GetString(data, 0, byteCount));
                        }

                    } while (networkStream.DataAvailable);


                    fs?.Close();
                    fs?.Dispose();
                    isFile = false;

                    if (builder.ToString().Contains("--close"))
                    {
                        _server.DeleteConnetion(Ip);
                        Close();
                    }
                    if (builder.ToString().Contains("--file"))
                    {
                        isFile = true;
                        fs = CreateStream();
                    }
                    if(builder.ToString().Contains("--Ip"))
                    {
                        Ip = builder.ToString().Substring(builder.ToString().IndexOf('p')+1);
                        _server.AddConnection(this);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                _server.DeleteConnetion(Ip);
                Close();
            }
        }


        private FileStream CreateStream()
        {
            if (File.Exists(Path.Combine(_defaulthPath, $"history_{Ip}")))
                File.Delete(Path.Combine(_defaulthPath, $"history_{Ip}"));
            return new FileStream(Path.Combine(_defaulthPath, $"history_{Ip}"), FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
        }

        public void Close()
        {
            tcpClient.Close();
            networkStream.Close();
            fs.Close();
            fs.Dispose();
        }

        public override string ToString()
        {
            return Ip;
        }
    }
}
