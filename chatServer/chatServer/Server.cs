using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chatServer
{
    class Server
    {
        private bool _power = true;

        private const string _serverIP = "192.168.1.2";
        private const int _serverPORT = 9933;
        private static Thread _serverThread;

        public void SetPower(bool obj)
        {
            _power = obj;
        }

        private static void StartServer()
        {
            // Получение имени компьютера.
            String host = Dns.GetHostName();
            // Получение ip-адреса.
            IPAddress ipAddress = Dns.GetHostByName(host).AddressList[0];

            //IPAddress ipAddress = IPAddress.Parse(_serverIP);

            //ipAddress = IPAddress.Any;
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _serverPORT);
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEndPoint);
            socket.Listen(1000);
            Console.WriteLine("Server has been started on IP: {0}.", ipEndPoint);

            while (true)
            {
                try
                {
                    Socket user = socket.Accept();
                    ServerFunctions.NewClient(user);
                }
                catch (Exception exp) { Console.WriteLine("Error: {0}", exp.Message); }
            }
        }

        private static void handlerCommands(string cmd)
        {
            cmd = cmd.ToLower();
            if (cmd.Contains("/getusers"))
            {
                int countUsers = ServerFunctions.Clients.Count;

                if (ServerFunctions.Clients.Count() != 0)
                {
                    for (int i = 0; i < countUsers; i++)
                    {
                        Console.WriteLine("[{0}]: {1}", i, ServerFunctions.Clients[i].UserName);
                    }
                }
                else
                    Console.WriteLine("There are no users");
            }
            else if (cmd.Contains("/poweroff"))
            {
                Server obj = new Server();

                _serverThread.Abort();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Command was not found");
            }
        }

        static void Main(string[] args)
        {
            Server obj = new Server();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение открыто");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            _serverThread = new Thread(StartServer);
            _serverThread.IsBackground = true;
            _serverThread.Start();
            while (true)
            {
                //Console.Write("$ ");
                handlerCommands(Console.ReadLine());
            }
        }
    }
}
