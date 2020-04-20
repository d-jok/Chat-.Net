using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using chatClient.Models;
using System.Runtime.Serialization;

namespace chatClient
{
    class Backup
    {
        [Serializable]
        private struct File
        {
            public string Name;
            public string Surname;
            public string NickName;
            public string Email;
            public string Phone;
            public List<Models.Message> messages;

            public File(string name, string surname, string nick, string email, string phone, List<Models.Message> list)
            {
                this.Name = name;
                this.Surname = surname;
                this.NickName = nick;
                this.Email = email;
                this.Phone = phone;
                this.messages = list;
            }
        }

        public byte[] ConvertInByte(object obj)
        {
            byte[] file = null;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, obj);

                    ms.Position = 0;
                    file = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return file;
        }

        public List<ListOfUsers> Decoder(byte[] array)
        {
            List<ListOfUsers> list = new List<ListOfUsers>();

            try
            {
                using (MemoryStream ms = new MemoryStream(array))
                {
                    MessageBox.Show("Blyt");
                    BinaryFormatter formatter = new BinaryFormatter();
                    list = (List<ListOfUsers>)formatter.Deserialize(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //MessageBox.Show(list.Count.ToString());

            return list;
        }

        public void LoadFromFile(ref List<ListOfUsers> listOfUsers, ref string fileOfChats)
        {
            FileStream fs = new FileStream(fileOfChats, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                listOfUsers = (List<ListOfUsers>)formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fs.Close();
            }
        }

        public byte[] UserListToBackup(ref List<Models.ListOfUsers> list)
        {
            byte[] file;
            List<File> temp = new List<File>();

            foreach(var V in list)
            {
                temp.Add(new File(V.Name, V.Surname, V.NickName, V.Email, V.Phone, V.messages));
            }

            file = ConvertInByte(temp);

            return file;
        }
    }
}
