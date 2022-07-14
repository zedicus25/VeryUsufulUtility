using System;
using System.Diagnostics;
using System.IO;
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

        private void getInfoBtn_Click(object sender, EventArgs e)
        {
            if (usersLB.SelectedItem == null)
                return;
            myServer.GetHistory(usersLB.SelectedItem.ToString());
        }

        private void getVersBtn_Click(object sender, EventArgs e)
        {
            if (usersLB.SelectedItem == null)
                return;
            myServer.GetVersion(usersLB.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(usersLB.SelectedItem == null)
                return;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\Server\\{usersLB.SelectedItem.ToString()}";
            Process.Start(path);
        }
    }
}
