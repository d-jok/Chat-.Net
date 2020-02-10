using System;
using System.Collections.Generic;
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
        private const string _serverIP = "192.168.1.2";
        private const int _serverPORT = 9933;
        private static Thread _serverThread;

        private static void StartServer()
        {
            // Получение имени компьютера.
            String host = Dns.GetHostName();
            // Получение ip-адреса.
            IPAddress ipAddress = Dns.GetHostByName(host).AddressList[0];
            //string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
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
                for (int i = 0; i < countUsers; i++)
                {
                    Console.WriteLine("[{0}]: {1}", i, ServerFunctions.Clients[i].UserName);
                }
            }
            if (cmd.Contains("/poweroff"))
            {
                _serverThread.Abort();
            }
        }

        static void Main(string[] args)
        {
<<<<<<< Updated upstream
=======
            Server obj = new Server();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение к базе данных открыто");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

>>>>>>> Stashed changes
            _serverThread = new Thread(StartServer);
            _serverThread.IsBackground = true;
            _serverThread.Start();
            while (true)
                handlerCommands(Console.ReadLine());
        }
    }
}
