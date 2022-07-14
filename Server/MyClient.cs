using System;
using System.Collections.Generic;
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

        public MyClient(TcpClient tcpClient, MyServer myServer)
        {
            _server = myServer;
            this.tcpClient = tcpClient;
            _workTask = new Task(Work);
            _workTask.Start();
            _server.AddConnection(this);
        }

        private void Work()
        {
            try
            {
                string msg;
                while (true)
                {
                    networkStream = tcpClient.GetStream();
                    msg = GetMsg();
                    if (msg.Contains("--close"))
                    {
                        _server.DeleteConnetion(Ip);
                        Close();
                    }
                    else
                    {
                        Ip = msg;
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

        private string GetMsg()
        {
            byte[] data = new byte[1024];
            StringBuilder builder = new StringBuilder();
            int byteCount = 0;
            do
            {
                byteCount = networkStream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, byteCount));
            } while (networkStream.DataAvailable);

            return builder.ToString();
        }

        public void Close()
        {
            tcpClient.Close();
            networkStream.Close();
        }

        public override string ToString()
        {
            return Ip;
        }
    }
}
