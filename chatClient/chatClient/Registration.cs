using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace chatClient
{
    public partial class Registration : Form
    {
        private Connection ConObj;
        private Thread th;

        public Registration()
        {
            InitializeComponent();
            ConObj = new Connection();
            connect();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConObj.Disconnect();
            this.Close();

            th = new Thread(OpenNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OpenNewForm(object obj)
        {
            Application.Run(new Login());
        }

        public void connect()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ConObj._serverHost);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, ConObj._serverPort);
                ConObj._serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                ConObj._serverSocket.Connect(ipEndPoint);

                ConObj._clientThread = new Thread(listner);
                ConObj._clientThread.IsBackground = true;
                ConObj._clientThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //ConnectionStatus(false);
            }
        }

        private void listner()
        {
            while (ConObj._serverSocket.Connected)
            {
                byte[] buffer = new byte[8196];
                int bytesRec = ConObj._serverSocket.Receive(buffer);
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);

                if (data != null)
                {
                    MessageBox.Show(data);
                    //LogParser(data);
                    continue;
                }
            }
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            if(_check() == true)
            {
                string line = textName.Text + "," + textSurname.Text + "," + textNick.Text + "," + textPhone.Text 
                    + "," + textEmail.Text + "," + textPassword.Text + ",";

                ConObj.send("#Registration&" + line);
            }
        }

        private void RegParser(string data)
        {
            int count = data.Count(c => c == '&');

            if(count == 2)
            {
                ErrEmail.Text = "Такой email уже существует";
                ErrPhone.Text = "Такой номер телефона уже существует";
            }

            if(count == 1)
            {
                if(data.Split('&')[0] == "Email")
                    ErrEmail.Text = "Такой email уже существует";
                else
                    ErrPhone.Text = "Такой номер телефона уже существует";
            }
        }

        private bool _check()
        {
            if (textName.Text == "")
            {
                ErrName.Text = "* Input Name";
                return false;
            }
            else
                ErrName.Text = "* ";

            if (textSurname.Text == "")
            {
                ErrSurname.Text = "* Input Surname";
                return false;
            }
            else
                ErrSurname.Text = "* ";

            if (textNick.Text == "")
            {
                ErrNick.Text = "* Input Nick Name";
                return false;
            }
            else
                ErrNick.Text = "* ";

            if (textPhone.Text == "")
            {
                ErrPhone.Text = "* Input Phone";
                return false;
            }
            else
                ErrPhone.Text = "* ";

            if (textEmail.Text == "")
            {
                ErrEmail.Text = "* Input Email";
                return false;
            }
            else
                ErrEmail.Text = "* ";

            if (textPassword.Text == "")
            {
                ErrPassword.Text = "* Input Password";
                return false;
            }
            else
                ErrPassword.Text = "* ";

            return true;
        }
    }
}