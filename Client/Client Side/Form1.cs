using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Side
{
    public partial class Form1 : Form
    {

        private UdpClient client;
        private IPEndPoint ip;
        string hostAddress = "127.0.0.1";


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.client = new UdpClient(5001);
            this.ip = new IPEndPoint(new IPAddress(0), 0);

            Thread thread = new Thread(new ThreadStart(this.waitMessage));
            thread.Start();
        }

        private delegate void displayDelegateMessage(string message);

        private void displayMessage(string message)
        {
            if (this.monitor.InvokeRequired)
            {
                Invoke(new displayDelegateMessage(displayMessage), new object[] { message });
            }
            else
            {
                this.monitor.Text += message;
            }
        }

        private void waitMessage()
        {
            while (true)
            {
                byte[] data = this.client.Receive(ref ip);
                string message = System.Text.Encoding.ASCII.GetString(data);
                this.displayMessage("Server: " + message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes("Client: " + this.message.Text);
            this.client.Send(data, data.Length, hostAddress, 5000);
            this.displayMessage("Client send message.");
        }
    }
}
