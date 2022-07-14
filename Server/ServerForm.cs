using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form
    {
        MyServer myServer;
        public ServerForm()
        {
            InitializeComponent();
            myServer = new MyServer();
            myServer.ConnectClient += MyServer_ConnectClient;
            myServer.DisconnectClient += MyServer_DisconnectClient;
        }

        private void MyServer_DisconnectClient(MyClient obj)
        {
            if (obj.Ip.Equals(String.Empty))
                return;
            usersLB.Invoke((MethodInvoker)delegate
            {
                usersLB.Items.Remove(obj);
            });
        }

        private void MyServer_ConnectClient(MyClient obj)
        {
            if (obj.Ip == null)
                return;
            usersLB.Invoke((MethodInvoker)delegate
            {
                usersLB.Items.Add(obj);
            });
        }
    }
}
