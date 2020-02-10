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
        private string _email;
        private string _phone;
        private Socket _handler;
        private Thread _userThread;

        public Client(Socket socket)
        {
            _handler = socket;
            _userThread = new Thread(listner);
            _userThread.IsBackground = true;
            _userThread.Start();
        }

        public string UserName
        {
            get { return _userName; }
        }

        private void listner()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRec = _handler.Receive(buffer);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                    handleCommand(data);
                }
                catch { ServerFunctions.EndClient(this); return; }
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

        private void handleCommand(string data)
        {
            if (data.Contains("#setname"))
            {
                _userName = data.Split('&')[1];
                UpdateChat();
                return;
            }
            if (data.Contains("#newmsg"))
            {
                string message = data.Split('&')[1];
                ChatController.AddMessage(_userName, message);
                return;
            }
<<<<<<< Updated upstream
=======
            if (data.Contains("#Registration"))
            {
                Registration obj = new Registration();
                string answer = obj.Register(data.Split('&')[1]);

                _handler.Send(Encoding.UTF8.GetBytes(answer));
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
            }
            if(data.Contains("#Search"))
            {
                Search obj = new Search();
                string answer = obj.FindUser(data.Split('&')[1]);
                Console.WriteLine(data.Split('&')[1]);

                if (answer != null)
                    _handler.Send(Encoding.UTF8.GetBytes(answer));
            }
            if(data.Contains("#Backup"))
            {
                Console.WriteLine("Backup " + data.Split(' ')[1]);
            }
>>>>>>> Stashed changes
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
