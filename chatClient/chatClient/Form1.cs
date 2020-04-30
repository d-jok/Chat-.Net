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
using chatClient.SaveAndLoad;
using chatClient.Models;
using chatClient.Assistant;
using chatClient.User;
using System.Text.RegularExpressions;
using Microsoft.Speech.Recognition;

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
        private Recognizer recognizer;

        private User.Info User; //CURENT user
        private FileCheck _fileCheck;
        public List<ListOfUsers> _listOfUsers;
        private bool _checkNew;
        private string _settingsPath;
        private string _usersPath;
        private string _fileOfChats;
        public string ChatListShow;

        public Form1() { }

        public Form1(Socket socket, Thread thread, Login.Log obj)
        {
            InitializeComponent();
            Printer = new printer(print);
            Cleaner = new cleaner(clearChat);
            recognizer = new Recognizer(this);  //start assistant;

            CurrentUser(ref obj);

            _checkNew = true;
            _settingsPath = "..//..//Settings//Settings.json";
            _fileOfChats = "..//..//ListOfChats//ListOfChats.dat";
            //_usersPath = "..//..//json//users";  //it was json.
            _serverSocket = socket;

            _fileCheck = new FileCheck();
            _listOfUsers = new List<ListOfUsers>();

            connect();

            //File Control
            _fileCheck.FileExist(_settingsPath);
            _fileCheck.FileExist(_fileOfChats);

            if (new FileInfo(_settingsPath).Length != 0)
            {
                try
                {
                    JObject jObj = JObject.Parse(File.ReadAllText(_settingsPath));
                    bool status = jObj.SelectToken("AsisStatus").Value<bool>();
                    string mFont = jObj.SelectToken("MessageFont").Value<string>();
                    string inFont = jObj.SelectToken("inputTextFont").Value<string>();
                    string NLOU = jObj.SelectToken("NamesListOfUsers").Value<string>();

                    recognizer.Status = status;
                    chatBox.Font = new Font(mFont, 7.8f);
                    chat_msg.Font = new Font(inFont, 7.8f);
                    ChatListShow = NLOU;
                }
                catch(SerializationException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                Models.Setting settings = new Models.Setting(recognizer.Status, null, 
                    chatBox.Font.Name, chat_msg.Font.Name, "Nick");

                using (StreamWriter file = File.CreateText(_settingsPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, settings);
                }
            }

            if (new FileInfo(_fileOfChats).Length != 0)
            {
                Load load = new Load();
                load.LoadFromFile(ref _listOfUsers, ref _fileOfChats);
            }
            if (_listOfUsers != null)
                ChatListItemsShow();

            //Testing Zone
            /*string path = "..//..//ListOfChats//list.json";
            _fileCheck.FileExist(path);
            string jsonString = JsonConvert.SerializeObject(_listOfUsers, Formatting.Indented);
            File.WriteAllText(path, jsonString);

            string readData = File.ReadAllText(path);
            List<ListOfUsers> L = JsonConvert.DeserializeObject<List<ListOfUsers>>(readData);
            //MessageBox.Show(_listOfUsers[0].NickName);
           //MessageBox.Show(L[0].NickName);
            _listOfUsers = L;
            ChatListItemsShow();*/
            //Testing Zone
        }

        public List<ListOfUsers> GetList
        {
            get { return _listOfUsers; }
        }

        private void CurrentUser(ref Login.Log obj)
        {
            User = new User.Info(obj.Name, obj.Surname, obj.NickName, obj.Email, obj.Phone);
        }

        public Info GetUserInfo
        {
            get { return User; }
        }

        private void listner()
        {
            try
            {
                while (_serverSocket.Connected)
                {
                    byte[] buffer = new byte[_serverSocket.ReceiveBufferSize];
                    int bytesRec = _serverSocket.Receive(buffer, SocketFlags.None);
                    if (bytesRec == 0)
                        break;

                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);

                    /*switch(data.Split(' ')[0])
                    {
                        case "#Search":
                            if (data.Contains("No result"))
                                MessageBox.Show("There are no users with this phone number");
                            else
                                AddNewFriend(data);

                            break;
                    }*/

                    if (data.Contains("#updatechat"))
                    {
                        UpdateChat(data);
                        continue;
                    }
                    if(data.Contains("#Search"))
                    {
                        if (data.Contains("No result"))
                            MessageBox.Show("There are no users with this phone number");
                        else
                            AddNewFriend(data);
                        return;
                    }
                    if (data.Split(' ')[0] == "#Answer")
                    {
                        string answer = "";

                        for (int i = 8; i < data.Length; i++)
                            answer += data[i];
                        MessageBox.Show(answer);

                        if(answer == "Info change success")
                        {
                            MessageBox.Show("Close this window and start this program again",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            Form1.ActiveForm.Close();
                        }
                    }

                    if(data.Split(' ')[0] == "#GetBackup")
                    {
                        string backup = "";
                        Save obj = new Save();

                        try
                        {
                            for (int i = 11; i < data.Length; i++)
                                backup += data[i];

                            _listOfUsers = JsonConvert.DeserializeObject<List<ListOfUsers>>(backup);
                            this.Invoke((MethodInvoker)delegate
                            {
                                ChatListItemsShow();
                                obj.SaveInFile(ref _listOfUsers, ref _fileOfChats);
                                MessageBox.Show("Backup load success");
                            });
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                print("Связь с сервером потеряна!");
            }
        }

        private void connect()
        {
            try
            {
                _clientThread = new Thread(this.listner);
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
            int number = 0;
            int counter = 0;
            int selected = ChatList.SelectedIndex;
            string author = data.Split(' ')[2];
            string message = "";
            string phone = data.Split(' ')[1];
            _checkNew = true;

            for (int i = 0; i < data.Length; i++)
            {
                if (counter == 3)
                    message += data[i];
                else if (data[i] == ' ' && counter < 3)
                    counter++;
            }

            counter = 0;    //Maybe I need it later!
            foreach (var V in _listOfUsers)
            {
                if (V.Phone == phone)
                {
                    V.messages.Add(new Models.Message(data.Split(' ')[2], message));
                    V.NewMsg = true;
                    number = V.ListNumber;
                    break;
                }
                counter++;
            }
                
            Sort(counter);

            this.Invoke((MethodInvoker)delegate
            {
                if (ChatList.SelectedIndex == number)
                    print(author + ": " + message);
                else
                {
                    this.ChatListItemsShow();
                    //_checkNew = false;
                    //ChatList.SetSelected(selected + 1, true);
                }
                //this.ChatList.Items[0] += "   *";
                
                /*if (ChatList.SelectedIndex == 0)    //ERROR!!!
                    print(message);*/
            });

            //#updatechat&userName~data|username~data
            //clearChat();                                  //HERE
            /*string[] messages = data.Split('&')[1].Split('|');
            int countMessages = messages.Length;
            if (countMessages <= 0) return;
            for (int i = 0; i < countMessages; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(messages[i])) continue;
                    print(String.Format("[{0}]:{1}.", messages[i].Split('~')[0], messages[i].Split('~')[1]));
                    MessageBox.Show(data);
                }
                catch { continue; }
            }*/
        }

        private void send(byte[] array)
        {
            try
            {
                int bytesSent = _serverSocket.Send(array);
            }
            catch
            {
                print("Связь с сервером прервалась...");
            }
        }

        public void send(string data)
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
            string phone = _listOfUsers[ChatList.SelectedIndex].Phone;
            Save save = new Save();

            _listOfUsers[ChatList.SelectedIndex].messages.Add(new Models.Message(User.NickName, chat_msg.Text));
            print(User.NickName + ": " + chat_msg.Text);

            try
            {
                string data = chat_msg.Text;
                if (string.IsNullOrEmpty(data))
                    return;
                send("#newmsg " + phone + " " + data);    //CHANGE email on sender phone
                chat_msg.Text = string.Empty;
            }
            catch { MessageBox.Show("Ошибка при отправке сообщения!"); }

            if (ChatList.SelectedIndex != 0)
            {
                Sort(ChatList.SelectedIndex);
                ChatListItemsShow();
            }

            save.SaveInFile(ref _listOfUsers, ref _fileOfChats);        
        }

        private void AddNewFriend(string data)  //Add new friends
        {
            this.Invoke((MethodInvoker)delegate
            {
                Save save = new Save();
                Load load = new Load();
                //List<Models.ListOfUsers> temp = new List<Models.ListOfUsers>();
                List<Models.Message> list = new List<Models.Message>();

                _listOfUsers.Add(new ListOfUsers(ChatList.Items.Count, data.Split(' ')[1],
                    data.Split(' ')[2], data.Split(' ')[3], data.Split(' ')[4], data.Split(' ')[5], list, false, DateTime.Now));

                //Sort listOfUsers and ChatList
                Sort(ref _listOfUsers);
                save.SaveInFile(ref _listOfUsers, ref _fileOfChats);
                ChatListItemsShow();   //Show friends in listbox.
            });
        }

        private void Sort(int number)
        {
            List<ListOfUsers> temp = new List<ListOfUsers>();
            temp.Add(_listOfUsers[number]);
            temp[0].ListNumber = 0;

            foreach(var V in _listOfUsers)
            {
                if (V.Phone != _listOfUsers[number].Phone)
                    temp.Add(new ListOfUsers(V.ListNumber + 1, V.Name, V.Surname, 
                        V.NickName, V.Email, V.Phone, V.messages, false, DateTime.Now));
            }

            _listOfUsers = temp;
        }

        private void Sort(ref List<Models.ListOfUsers> list)
        {
            ListOfUsers temp = new ListOfUsers();
            List<ListOfUsers> SortList = new List<ListOfUsers>();

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
            }
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

        public void Search(string number)
        {
            try
            {
                bool check = true;

                if (string.IsNullOrEmpty(number))
                    return;

                if (User.Phone == number)
                {
                    MessageBox.Show("This is your number");
                    check = false;
                    return;
                }
                else if (_listOfUsers != null)
                    foreach (var V in _listOfUsers)
                        if (V.Phone == number)
                            check = false;

                if (check == true)
                    send("#Search&" + number);
                else MessageBox.Show("This user is already in friends");
            }
            catch
            {
                MessageBox.Show("Ошибка при отправке запроса!");
            }
        }

        public void ChatListItemsShow()     //Chat list show
        {
            ChatList.Items.Clear();

            if(ChatListShow == "Nick")
                foreach (var V in _listOfUsers)
                {
                    if (V.NewMsg == false)
                        ChatList.Items.Add(V.NickName);
                    else
                        ChatList.Items.Add(V.NickName + "   *");
                }
            else if(ChatListShow == "Phone")
                foreach (var V in _listOfUsers)
                {
                    if (V.NewMsg == false)
                        ChatList.Items.Add(V.Phone);
                    else
                        ChatList.Items.Add(V.Phone + "   *");
                }
            else if(ChatListShow == "Name")
                foreach (var V in _listOfUsers)
                {
                    if (V.NewMsg == false)
                        ChatList.Items.Add(V.Name);
                    else
                        ChatList.Items.Add(V.Name + "   *");
                }
            else if(ChatListShow == "Surname")
                foreach (var V in _listOfUsers)
                {
                    if (V.NewMsg == false)
                        ChatList.Items.Add(V.Surname);
                    else
                        ChatList.Items.Add(V.Surname + "   *");
                }
        }

        private void ChatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_checkNew == true)
            {
                int selected = ChatList.SelectedIndex;
                string text = ChatList.SelectedItem.ToString();
                //MessageBox.Show("IN");

                clearChat();

                if (text.Contains('*'))
                {
                    _checkNew = false;
                    _listOfUsers[ChatList.SelectedIndex].NewMsg = false;
                    ChatListItemsShow();
                    ChatList.SetSelected(selected, true);
                }

                ChatName.Text = ChatList.SelectedItem.ToString() + " Chat";

                foreach (var V in _listOfUsers[ChatList.SelectedIndex].messages)
                {
                    print(V.User + ": " + V.Text);
                }
            }
            _checkNew = true;
        }

        //Controls
        public void assistantStatus(bool status)
        {
            recognizer.Status = status;
        }

        private void searchUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.Owner = this;
            search.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(_settingsPath);
            settings.Owner = this;
            settings.Show();
        }

        private void myProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserProfile profile = new UserProfile(User);
            profile.Owner = this;
            profile.Show();
        }
    }
}