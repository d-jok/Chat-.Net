using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace chatServer
{
    [Serializable]
    public struct Message
    {
        public string User;
        public string Text;

        public Message(string user, string text)
        {
            this.User = user;
            this.Text = text;
        }
    }

    [Serializable]
    class Backup
    {
        public string Name;
        public string Surname;
        public string NickName;
        public string Email;
        public string Phone;
        public List<Message> messages;

        public Backup() { }

        public Backup(string name, string sur, string nick,
           string email, string phone, List<Message> messages)
        {
            Name = name;
            Surname = sur;
            NickName = nick;
            Email = email;
            Phone = phone;
            this.messages = messages;
        }

        public string GetBackup(string phone)
        {
            int Id = 0;
            string answer = "";
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = "SELECT ID FROM Users WHERE Phone = '" + phone + "'";
            string SqlCheck = "SELECT BackupFile FROM UsersBackup WHERE UserID = ";

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                conn.Open();
                using (SqlCommand check = new SqlCommand(sql, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    while (reader.Read())
                        Id = (int)reader["ID"];

                    SqlCheck += "'" + Id + "'";
                }

                using (SqlCommand check = new SqlCommand(SqlCheck, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    try
                    {
                        if (!reader.HasRows)
                        {
                            answer = "#Answer You have not backup";
                        }
                        else
                        {
                            byte[] array = null;
                            while(reader.Read())
                                array = (byte[])reader["BackupFile"];

                            answer = "#GetBackup " + Encoding.UTF8.GetString(array);
                        }
                    }
                    catch
                    {
                        answer = "Something went wrong";
                    }
                    conn.Close();
                }
            }

            return answer;
        }

        public string SaveBackup(byte[] array, string phone)  //User want to make backup(click on button "Backup")
        {
            int Id = 0;
            string status = "None";
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = "SELECT ID FROM Users WHERE Phone = '" + phone + "'";
            string SqlCheck = "SELECT ID FROM UsersBackup WHERE UserID = ";

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                conn.Open();
                using (SqlCommand check = new SqlCommand(sql, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    while (reader.Read())
                        Id = (int)reader["ID"];

                    SqlCheck += "'" + Id + "'";
                }

                using (SqlCommand check = new SqlCommand(SqlCheck, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    try
                    {
                        if (!reader.HasRows)
                        {
                            InsertDB(array, ref Id, ref _conLine);
                            status = "Upload Backup Success";
                        }
                        else
                        {
                            UpdateDB(array, ref Id, ref _conLine);
                            status = "Upload Backup Success";
                        }
                    }
                    catch
                    {
                        status = "Upload Backup Error";
                    }
                    conn.Close();
                }
            }

            return status;
        }

        /*public string SaveInDB(string phone, List<Message> mList)
        {
            int Id = 0;
            string status = "Save-Error";
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlGetID = "SELECT ID FROM Users WHERE Phone = '" + phone + "'";
            
            List<Backup> list = new List<Backup>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_conLine))
                {
                    conn.Open();
                    using (SqlCommand check = new SqlCommand(sqlGetID, conn))
                    using (SqlDataReader reader = check.ExecuteReader())
                    {
                        while (reader.Read())
                            Id = (int)reader["ID"];
                    }
                    conn.Close();

                    string sqlGetValueID = "SELECT BackupFile FROM UsersBackup WHERE UserID = " + Id;

                    conn.Open();
                    using (SqlCommand check = new SqlCommand(sqlGetValueID, conn))
                    using (SqlDataReader reader = check.ExecuteReader())
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        byte[] G = null;

                        try
                        {
                            Backup temp = GetUser(ref _conLine, ref phone, mList);

                            if (reader.Read())
                            {
                                try
                                {
                                    G = (byte[])reader["BackupFile"];
                                    using (MemoryStream ms = new MemoryStream(G))
                                    {
                                        list = (List<Backup>)formatter.Deserialize(ms);
                                        Console.WriteLine(list[0].messages[0].Text);
                                    }
                                    //Data update                                  
                                    UpdateList(ref list, ref temp);
                                    UpdateDB(ref list, ref Id, ref _conLine);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Deserialization ERROR:");
                                    Console.WriteLine(ex);
                                }
                            }
                            else if (!reader.HasRows)
                            {
                                List<Backup> obj = new List<Backup>();
                                obj.Add(new Backup(temp.Name, temp.Surname, temp.NickName, temp.Email,
                                    temp.Phone, temp.messages));

                                InsertDB(ref obj, ref Id, ref _conLine);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Reader ERROR:");
                            Console.WriteLine(ex);
                        }
                    }
                    conn.Close();

                    status = "Save-Success";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return status;
        }*/

        private void InsertDB(byte[] array, ref int Id, ref string _conLine)
        {
            string sql = "INSERT INTO dbo.UsersBackup(UserID, BackupFile, DateOfChange) VALUES(@Id, @File, @Date)";

            using (SqlConnection conn = new SqlConnection(_conLine))
            using (SqlCommand insert = new SqlCommand(sql, conn))
            {
                insert.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                insert.Parameters.Add("@File", SqlDbType.VarBinary).Value = array;
                insert.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;

                conn.Open();
                insert.ExecuteNonQuery();
                conn.Close();
            }
            //Console.WriteLine("IN");
        }

        private void UpdateDB(byte[] array, ref int Id, ref string _conLine)
        {
            string SqlUpdate = "UPDATE UsersBackup SET BackupFile = @File, DateOfChange = @Date WHERE UserID = " + Id;

            using (SqlConnection conn = new SqlConnection(_conLine))
            using (SqlCommand update = new SqlCommand(SqlUpdate, conn))
            {
                update.Parameters.Add("@File", SqlDbType.VarBinary).Value = array;
                update.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;

                conn.Open();
                update.ExecuteNonQuery();
                conn.Close();
            }

            //Console.WriteLine("UP");
        }

        private void UpdateList(ref List<Backup> list, ref Backup file)
        {
            foreach (var V in list)
            {
                if (V.Phone == file.Phone)
                    V.messages.Add(new Message(file.NickName, file.messages[0].Text));
            }
        }

        private Backup GetUser(ref string _conLine, ref string phone, List<Message> mList)
        {
            string sqlGetUser = "SELECT Name, Surname, NickName, Email, Phone FROM Users WHERE Phone = '@phone'";
            Backup obj = new Backup();

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                conn.Open();
                using (SqlCommand check = new SqlCommand(sqlGetUser, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        obj = new Backup(reader["Name"].ToString(), 
                            reader["Surname"].ToString(), 
                            reader["NickName"].ToString(),
                            reader["Email"].ToString(), 
                            reader["Phone"].ToString(), 
                            mList);
                    }                       
                }
                conn.Close();
            }

            return obj;
        }
    }
}
