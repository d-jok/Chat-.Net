using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chatClient
{
    public partial class Login : Form
    {
        private Connection ConObj;
        private Thread th;
        private string _path;

        private struct Log
        {
            public string Email;
            public string Password;
        }

        public Login()
        {
            InitializeComponent();

            FileExist("D:..//..//json//pass.json");

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
            Application.Run(new Registration());
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

                ConnectionStatus(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ConnectionStatus(false);
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

                    if (this.InvokeRequired)
                        LogParser(data);
                }            
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)      //Login
        {
            if (_inputCheck() == true)
            {
                string line = textEmail.Text + "," + textPassword.Text + ",";

                ConObj.send("#Login&" + line);
            }
        }
        
        private void ConnectionStatus(bool status)
        {
            if (status == false)
            {
                StatusText.ForeColor = Color.Red;
                StatusText.Text = "Disconnected";
            }
            else
            {
                StatusText.ForeColor = Color.Green;
                StatusText.Text = "Connected";
            }
        }

        private void LogParser(string data)
        {
            ActiveForm.Invoke((MethodInvoker)delegate {       //YES IT`S WORKING!!!
                // Running on the UI thread
                if(data.Split(' ')[0] == "Error")
                {
                    if(data.Count(c => c == ' ') == 3)
                    {
                        ErrEmail.Text = "Wrong Email";
                        ErrPassword.Text = "Wrong Password";
                    }
                    else
                    {
                        if (data.Split(' ')[1] == "Email")
                            ErrEmail.Text = "Wrong Email";
                        else
                            ErrPassword.Text = "Wrong Password";
                    }
                }
                else
                {
                    if(RememberMe.Checked)
                    {
                        Log log = new Log
                        {
                            Email = data.Split(' ')[1],
                            Password = data.Split(' ')[2]
                        };

                        using (StreamWriter file = File.CreateText(_path))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(file, log);
                        }
                    }

                    this.Close();

                    ConObj._clientThread.Abort();

                    th = new Thread(OpenNew);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
            });
        }

        private void OpenNew(object obj)
        {
            Application.Run(new Form1(ConObj._serverSocket, ConObj._clientThread));
        }

        private void FileExist(string path)
        {
            _path = path;

            if(File.Exists(_path) != true)
            {
                File.Create(_path);
            }
            else
            {
                using (StreamReader reader = new StreamReader(_path))
                {
                    JObject o1 = JObject.Parse(File.ReadAllText(_path));
                    textEmail.Text = o1.SelectToken("Email").Value<string>();
                    textPassword.Text = o1.SelectToken("Password").Value<string>();
                }      
            }
        }

        private bool _inputCheck()
        {
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