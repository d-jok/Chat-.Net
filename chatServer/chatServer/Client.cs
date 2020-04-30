using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace chatServer
{
    class Client
    {
        private string _userName;
        public string _email;
        public string _phone;   //HERE
        private string _command = "";
        private Socket _handler;
        private Thread _userThread;

        public Client() { }

        public Client(Socket socket)
        {
            _handler = socket;
            _userThread = new Thread(listener);
            _userThread.IsBackground = true;
            _userThread.Start();
        }

        public string UserName
        {
            get { return _userName; }
        }

        private void listener()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[_handler.ReceiveBufferSize];
                    int bytesRec = _handler.Receive(buffer, SocketFlags.None);

                    //string temp = Encoding.UTF8.GetString(buffer, 0, 22);

                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                    handleCommand(data, this, buffer);
                }
                catch
                { 
                    ServerFunctions.EndClient(this); return; 
                }
            }
        }

        public void End()
        {
            try
            {
                _handler.Close();
                try
                {
                    _userThread.Abort();
                }
                catch { } // г
            }
            catch (Exception exp) { Console.WriteLine("Error with end: {0}.", exp.Message); }
        }

        private void handleCommand(string data, Client client, byte[] buffer)
        {
            if (data.Contains("#setname"))
            {
                _userName = data.Split('&')[1];
                UpdateChat();
                return;
            }
            if (data.Contains("#newmsg"))
            {
                int counter = 0;
                string phone = data.Split(' ')[1];
                string message = "";

                for (int i = 0; i < data.Length; i++)
                {
                    if (counter == 2)
                        message += data[i];
                    else if (data[i] == ' ' && counter < 2)
                        counter++;
                }

                ServerFunctions.UpdateChats(_userName, phone, _phone, message);
                return;
            }
            if (data.Split(' ')[0] == "#UpdateChats")
            {
                string number = data.Split(' ')[1];
                TempMessage obj = new TempMessage();
                List<TempMessage> list = new List<TempMessage>();
                try
                {
                    list = obj.GetTempMsgForUser(number);

                    if (list.Count != 0)
                        foreach (var V in list)
                            for(int i = 0; i < V._msgList.Count(); i++)
                                Send("#updatechat " + V._fromUser + " " + V._msgList[i].User + " " + V._msgList[i].Text);

                    obj.DeleteTemp(ref number);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return;
            }
            /*if (data.Split(' ')[0] == "#DeleteTemp")
            {
                string number = data.Split(' ')[0];
                TempMessage obj = new TempMessage();
                obj.DeleteTemp(ref number);
            }*/
            if (data.Contains("#Registration"))
            {
                Registration obj = new Registration();
                string answer = obj.Register(data.Split('&')[1]);

                _handler.Send(Encoding.UTF8.GetBytes(answer));
                return;
            }
            if(data.Contains("#Login"))
            {
                Login obj = new Login();
                string answer = obj.Log(data.Split('&')[1]);

                if (answer.Contains("Success"))
                {
                    _email = answer.Split(' ')[1];
                    _userName = answer.Split(' ')[3];
                    _phone = answer.Split(' ')[4];
                }

                _handler.Send(Encoding.UTF8.GetBytes(answer));
                return;
            }
            if(data.Contains("#Search"))
            {
                Search obj = new Search();
                string answer = obj.FindUser(data.Split('&')[1]);
                Console.WriteLine(data.Split('&')[1]);

                if (answer != null)
                    _handler.Send(Encoding.UTF8.GetBytes(answer));

                return;
            }
            if (data.Split(' ')[0] == "#Change")
            {
                int counter = 0;
                string temp = "";
                ChangeUserInfo obj = new ChangeUserInfo();

                for(int i = 0; i < data.Length; i++)
                {
                    if (counter == 3)
                        temp += data[i];
                    if (counter < 3 && data[i] == ' ')
                        counter++;
                }

                temp = "#Answer " + obj.Change(data.Split(' ')[1], data.Split(' ')[2], temp);
                Send(temp);
            }

            if (data.Split(' ')[0] == "#Backup")
            {
                string answer = "#Answer ";
                string backup = "";
                string number = data.Split(' ')[1];
                Backup obj = new Backup();

                for (int i = 22; i < data.Length; i++)
                    backup += data[i];

                answer += obj.SaveBackup(Encoding.UTF8.GetBytes(backup), number);
                Send(answer);

                return;
            }
            if (data.Split(' ')[0] == "#GetBackup")
            {
                string backup = "";
                string number = data.Split(' ')[1];
                Backup obj = new Backup();

                backup += obj.GetBackup(number);
                Send(backup);
            }
        }

        public void UpdateChat()
        {
            Send(ChatController.GetChat());
        }

        public void Send(string command)
        {
            try
            {
                int bytesSent = _handler.Send(Encoding.UTF8.GetBytes(command));
                if (bytesSent > 0) Console.WriteLine("Success");
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error with send command: {0}.", exp.Message);
                ServerFunctions.EndClient(this);
            }
        }
    }
}
