using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

using System.Windows.Forms;
using System.Threading;

namespace Client_Server_App
{
    public partial class Form1 : Form
    {
        private UdpClient client;
        private IPEndPoint ip;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.client = new UdpClient(5000);
            this.ip = new IPEndPoint(new IPAddress(0), 0);
            Thread thread = new Thread(new ThreadStart(this.waitPackets));
            thread.Start();
        }

        private delegate void displayDelegateMassage(string message);

        private void displayMassage(string message)
        {
            if (this.monitor.InvokeRequired)
            {
                Invoke(new displayDelegateMassage(displayMassage), new object[] { message });
            }
            else
            {
                this.monitor.Text += message;
            }
        }

        private void waitPackets()
        {
            while (true)
            {
                byte[] data = this.client.Receive(ref ip);
                String message = System.Text.Encoding.ASCII.GetString(data);

                this.displayMassage("/n Receiving.....");
                this.displayMassage(message);

                this.displayMassage("/n Sending.....");
                this.client.Send(data, data.Length, ip);
                this.displayMassage("/n Packet sent");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
