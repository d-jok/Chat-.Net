using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace chatClient
{
    class Connection
    {
        public int _serverPort;
        public string _serverHost;
        public Socket _serverSocket;
        public Thread _clientThread;

        public Connection()
        {
            _serverHost = "192.168.0.100";
            _serverPort = 9933;
        }

        public void Disconnect()
        {
            _serverSocket.Close();
            _clientThread.Abort();
        }

        public void send(string data)
        {
            try
            {
                Stream stream = new NetworkStream(_serverSocket);
                var bin = new BinaryFormatter();
                bin.Serialize(stream, data);
                stream.Close(); //HERE
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //print("Связь с сервером прервалась...");
            }
        }
    }
}
