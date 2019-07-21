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
using System.Windows.Forms;

namespace chatClient
{
    public partial class Form1 : Form
    {
        private delegate void printer(string data);
        private delegate void cleaner();
        printer Printer;
        cleaner Cleaner;
        private Socket _serverSocket;
        private Thread _clientThread;
        private string _serverHost;
        private int _serverPort;

        public Form1()
        {
            InitializeComponent();
            Printer = new printer(print);
            Cleaner = new cleaner(clearChat);
            exitChat.Enabled = false;
        }

        private void listner()
        {
            while (_serverSocket.Connected)
            {
                byte[] buffer = new byte[8196];
                int bytesRec = _serverSocket.Receive(buffer);
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                if (data.Contains("#updatechat"))
                {
                    UpdateChat(data);
                    continue;
                }
            }
        }

        private void connect()
        {
            try
            {
                //IPHostEntry ipHost = Dns.GetHostEntry(_serverHost);
                //IPAddress ipAddress = ipHost.AddressList[0];
                _serverHost = textBox1.Text;
                _serverPort = Int32.Parse(textBox2.Text);

                IPAddress ipAddress = IPAddress.Parse(_serverHost);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _serverPort);
                _serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Connect(ipEndPoint);

                _clientThread = new Thread(listner);
                _clientThread.IsBackground = true;
                _clientThread.Start();
            }
            catch
            {
                print("Сервер недоступен!");

                userName.Enabled = true;
                enterChat.Enabled = true;
                exitChat.Enabled = false;
            }
        }

        private void clearChat()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Cleaner);
                return;
            }
            chatBox.Clear();
        }

        private void UpdateChat(string data)
        {
            //#updatechat&userName~data|username~data
            clearChat();
            string[] messages = data.Split('&')[1].Split('|');
            int countMessages = messages.Length;
            if (countMessages <= 0) return;
            for (int i = 0; i < countMessages; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(messages[i])) continue;
                    print(String.Format("[{0}]:{1}.", messages[i].Split('~')[0], messages[i].Split('~')[1]));
                }
                catch { continue; }
            }
        }

        private void send(string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int bytesSent = _serverSocket.Send(buffer);
            }
            catch
            {
                enterChat.Enabled = true;
                print("Связь с сервером прервалась...");
            }
        }

        private void print(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Printer, msg);
                return;
            }
            if (chatBox.Text.Length == 0)
                chatBox.AppendText(msg);
            else
                chatBox.AppendText(Environment.NewLine + msg);
        }

        private void sendMessage()
        {
            try
            {
                string data = chat_msg.Text;
                if (string.IsNullOrEmpty(data)) return;
                send("#newmsg&" + data);
                chat_msg.Text = string.Empty;
            }
            catch { MessageBox.Show("Ошибка при отправке сообщения!"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = userName.Text;

            connect();

            chatBox.Enabled = true;
            chat_msg.Enabled = true;
            chat_send.Enabled = true;
            userName.Enabled = false;
            enterChat.Enabled = false;
            exitChat.Enabled = true;

            _serverHost = textBox1.Text;
            _serverPort = Int32.Parse(textBox2.Text);

            if (string.IsNullOrEmpty(Name)) return;
            send("#setname&" + Name);
        }

        private void chat_send_Click(object sender, EventArgs e)
        {
            sendMessage();
        }

        private void exitChat_Click(object sender, EventArgs e)
        {
            _serverSocket.Close();
            _clientThread.Abort();
            userName.Enabled = true;
            enterChat.Enabled = true;
            exitChat.Enabled = false;
        }

        private void chatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                sendMessage();
        }

        private void chat_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                sendMessage();
        }
    }
}
