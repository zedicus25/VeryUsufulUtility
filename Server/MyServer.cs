using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MyServer
    {
        private TcpListener tcpListener;
        private List<MyClient> clients;
        readonly int PORT;
        public  event Action<MyClient> ConnectClient;
        public  event Action<MyClient> DisconnectClient;
        private Task LisenTask;

        public MyServer(int port = 8008)
        {
            clients = new List<MyClient>();
            this.PORT = port;
            LisenTask = new Task(Listen);
            LisenTask.Start();
        }

        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, PORT);
                tcpListener.Start();
                Console.WriteLine("SERVER START");
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    MyClient myClient = new MyClient(client, this);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CloseServer();
            }
        }

        public void GetHistory(string ip)
        {
            MyClient client = clients.FirstOrDefault(x => x.Ip.Equals(ip));
            if(client != null)
            {
                byte[] b = Encoding.Unicode.GetBytes("--getFile");
                client.networkStream.Write(b,0,b.Length);
            }
        }

        public void GetVersion(string ip)
        {
            MyClient client = clients.FirstOrDefault(x => x.Ip.Equals(ip));
            if (client != null)
            {
                byte[] b = Encoding.Unicode.GetBytes("--getVersion");
                client.networkStream.Write(b, 0, b.Length);
            }
        }

        public void DeleteConnetion(string ip)
        {
            MyClient client = clients.FirstOrDefault(x => x.Ip.Equals(ip));
            if (client != null)
            {
                clients.Remove(client);
                client.Close();
                DisconnectClient?.Invoke(client);
            }
        }

        public void AddConnection(MyClient myClient)
        {
            clients.Add(myClient);
            ConnectClient?.Invoke(clients.Last());
        }

        public void CloseServer()
        {
            tcpListener.Stop();
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
        }


    }
}
