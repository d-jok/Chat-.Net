using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace chatClient
{
    [Serializable]
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

<<<<<<< Updated upstream
        public Form1()
=======
        private List<Models.ListOfUsers> _listOfUsers;
        private string _usersPath;

        public Form1(Socket socket, Thread thread)
>>>>>>> Stashed changes
        {
            InitializeComponent();
            Printer = new printer(print);
            Cleaner = new cleaner(clearChat);
<<<<<<< Updated upstream
            exitChat.Enabled = false;
=======

            _usersPath = "D:..//..//json//users";  //it was json.
            _serverSocket = socket;
            _listOfUsers = new List<Models.ListOfUsers>();

            connect();
            _loadFromFile();
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                    UpdateChat(data);
                    continue;
                }
            }
=======
                    byte[] buffer = new byte[8196];
                    int bytesRec = _serverSocket.Receive(buffer);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);

                    switch(data.Split(' ')[0])
                    {
                        case "#Search":
                            if (data.Contains("No result"))
                                MessageBox.Show("There are no users with this phone number");
                            else
                                AddNewFriend(data);

                            break;

                        /*case "#updatechat":
                            UpdateChat(data);
                            continue;
                            //break;*/
                    }

                    if (data.Contains("#updatechat"))
                    {
                        UpdateChat(data);
                        continue;
                    }
                    /*if(data.Contains("#Search"))
                    {
                        if (data.Contains("No result"))
                            MessageBox.Show("There are no users with this phone number");
                        else
                            AddNewFriend(data);
                    }*/
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                print("Связь с сервером потеряна!");
            }
>>>>>>> Stashed changes
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
                    //print(String.Format("[{0}]:{1}.", messages[i].Split('~')[0], messages[i].Split('~')[1]));
                    MessageBox.Show(data);
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

        private void AddNewFriend(string data)  //Add new friends
        {
            ActiveForm.Invoke((MethodInvoker)delegate
            {
                List<Models.Message> list = new List<Models.Message>();

                //test
                list.Add(new Models.Message("Sinned", "Hello"));
                list.Add(new Models.Message("Lynx", "Hi, how are you?"));
                //test

                _listOfUsers.Add(new Models.ListOfUsers(ChatList.Items.Count, data.Split(' ')[1],
                    data.Split(' ')[2], data.Split(' ')[3], data.Split(' ')[4], data.Split(' ')[5], list, DateTime.Now));

<<<<<<< Updated upstream
            connect();
=======
                //Sort listOfUsers and ChatList
                Sort(ref _listOfUsers);
>>>>>>> Stashed changes

                _ChatListItemsShow();   //Show friends in listbox. 

                _saveInFile();  //Save friends and chats in file.

                //Add users to json
                /*if (File.Exists(_usersPath) != true)
                {
                    File.Create(_usersPath);
                }

                Models.Users obj = new Models.Users
                {
                    Name = data.Split(' ')[1],
                    Surname = data.Split(' ')[2],
                    NickName = data.Split(' ')[3],
                    Email = data.Split(' ')[4],
                    Phone = data.Split(' ')[5]
                };

                using (StreamWriter file = File.CreateText(_usersPath))     //ERROR: ONLY FILE REWRITE. 
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, obj);
                }*/
            });
        }

        private void Sort(ref List<Models.ListOfUsers> list)
        {
            Models.ListOfUsers temp = new Models.ListOfUsers();
            List<Models.ListOfUsers> SortList = new List<Models.ListOfUsers>();

            if (list.Count > 1)
            {
                foreach (var V in list)
                    foreach (var N in list)
                        if (V.DataOfChange > N.DataOfChange)    //!!! > this is true
                            temp = V;

                SortList.Add(temp);
                SortList[0].ListNumber = 0;

                foreach (var V in list)
                    if (V != temp)
                    {
                        V.ListNumber += 1;
                        SortList.Add(V);
                    }

                list = SortList;

                foreach (var V in SortList)
                    MessageBox.Show(V.ListNumber.ToString());
            }
        }

        private void _saveInFile()
        {
            FileExist(_usersPath);

            FileStream SaveInFile = File.OpenWrite(_usersPath);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(SaveInFile, _listOfUsers);
            }
            catch(SerializationException ex)
            {
                MessageBox.Show("Failed to serialize. Reason: " + ex);
            }
            finally
            {
                SaveInFile.Close();
            }
        }

        private void _loadFromFile()
        {
            FileExist(_usersPath);

            FileStream LoadFromFile = File.OpenRead(_usersPath);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                if (LoadFromFile.Length != 0)
                {
                    _listOfUsers = (List<Models.ListOfUsers>)formatter.Deserialize(LoadFromFile);
                    _ChatListItemsShow();
                }
            }
            catch(SerializationException ex)
            {
                MessageBox.Show("Failed to deserialize. Reason: " + ex);
            }
            finally
            {
                LoadFromFile.Close();
            }
        }

        public void FileExist(string filepath)
        {
            if (!File.Exists(filepath))
                File.Create(filepath);
        }

        private void chat_send_Click(object sender, EventArgs e)
        {
            sendMessage();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SearchBox.Text))
                    return;

                send("#Search&" + SearchBox.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка при отправке запроса!");
            }
        }

        private void _ChatListItemsShow()
        {
            ChatList.Items.Clear();
            foreach (var V in _listOfUsers)
                ChatList.Items.Add(V.NickName);
        }

        private void ChatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearChat();

            foreach (var V in _listOfUsers[ChatList.SelectedIndex].messages)
            {
                print(V.User + ": " + V.Text);
            }
        }

        private void Backup_Click(object sender, EventArgs e)
        {
            string data = "#Backup " + _listOfUsers.ToString(); //Wrong!!!!
            send(data);
        }
    }
}