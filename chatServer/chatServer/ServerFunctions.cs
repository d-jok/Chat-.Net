using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace chatServer
{
    class ServerFunctions
    {
        public static List<Client> Clients = new List<Client>();

        public static void NewClient(Socket handle)
        {
            try
            {
                Client newClient = new Client(handle);
                Clients.Add(newClient);
                Console.WriteLine("New client connected: {0}", handle.RemoteEndPoint);
            }
            catch (Exception exp) { Console.WriteLine("Error with addNewClient: {0}.", exp.Message); }
        }

        public static void EndClient(Client client)
        {
            try
            {
                client.End();
                Clients.Remove(client);
                Console.WriteLine("User {0} has been disconnected.", client.UserName);
            }
            catch (Exception exp) { Console.WriteLine("Error with endClient: {0}.", exp.Message); }
        }

        public static void UpdateAllChats()
        {
            try
            {
                int countUsers = Clients.Count;
                for (int i = 0; i < countUsers; i++)
                {
                    //Console.WriteLine(Clients[i]._email);
                    Clients[i].UpdateChat();
                }
            }
            catch (Exception exp) { Console.WriteLine("Error with updateAlLChats: {0}.", exp.Message); }
        }

        public static void UpdateChats(string userName, string phone, string from, string message)
        {
            string status = "none";
            Backup backup = new Backup();
            List<Message> list = new List<Message>();
            list.Add(new Message(userName, message));

            if(Clients.Exists(x => x._phone.Contains(phone)) == true)   //may be phone without email. Need to think!!!
            {
                Client temp = Clients.Find(x => x._phone.Contains(phone));
                Console.WriteLine("User online FIND");
                temp.Send("#updatechat " + from + " " + userName + " " + message);
            }
            else
            {
                Console.WriteLine("NewMsg");
                //status = backup.SaveInDB(phone, list);
                //Console.WriteLine(status);
            }
        }
    }
}
