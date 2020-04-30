using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace chatServer
{
    public class TempMessage
    {
        public string _fromUser;
        public List<Message> _msgList;

        public TempMessage() { }

        public TempMessage(string fromUser, List<Message> msgList)
        {
            this._fromUser = fromUser;
            this._msgList = msgList;
        }

        private List<TempMessage> GetMessages(string number, ref int Id)
        {
            string sql = "SELECT ID FROM Users WHERE Phone = '" + number + "'";
            string SqlCheck = "SELECT MsgText FROM MsgTemp WHERE UserID = ";
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<TempMessage> list = new List<TempMessage>();

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
                            return null;
                        }
                        else
                        {
                            byte[] array = null;
                            while (reader.Read())
                                array = (byte[])reader["MsgText"];

                            string json = Encoding.UTF8.GetString(array);
                            list = JsonConvert.DeserializeObject<List<TempMessage>>(json);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("GetMessages() Error:\n" + ex);
                    }
                }
                conn.Close();
            }

            return list;
        }

        private void SaveMessages(string method, ref int Id, ref List<TempMessage> list)
        {
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string jsonString = JsonConvert.SerializeObject(list, Formatting.Indented);
            byte[] array = Encoding.UTF8.GetBytes(jsonString);

            if (method == "INSERT")
            {
                InsertDB(ref _conLine, Id, ref array);
            }
            else      //UPDATE
            {
                UpdateDB(ref _conLine, Id, ref array);
            }
        }

        public List<TempMessage> GetTempMsgForUser(string number)     //When user login
        {
            int Id = 0;
            List<TempMessage> list = new List<TempMessage>();
            list = GetMessages(number, ref Id);

            return list;
        }

        public void AddNewMessage(ref string number, ref string from, ref string nick, ref string text)
        {
            int Id = 0;
            bool find = false;
            List<TempMessage> list = GetMessages(number, ref Id);

            try
            {

                if (list != null)
                {
                    foreach (var V in list)
                    {
                        if (V._fromUser == from)
                        {
                            V._msgList.Add(new Message(nick, text));
                            find = true;
                            SaveMessages("UPDATE", ref Id, ref list);
                            return;
                        }
                    }

                    if (find == false)
                    {
                        List<Message> msg = new List<Message>();
                        msg.Add(new Message(nick, text));
                        list.Add(new TempMessage(from, msg));
                        SaveMessages("UPDATE", ref Id, ref list);
                    }
                }
                else
                {
                    List<Message> msg = new List<Message>();
                    list = new List<TempMessage>();
                    msg.Add(new Message(nick, text));
                    list.Add(new TempMessage(from, msg));
                    SaveMessages("INSERT", ref Id, ref list);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void DeleteTemp(ref string number)
        {
            int Id = 0;
            string sqlID = "SELECT ID FROM Users WHERE Phone = '" + number + "'";
            string sqlDelete = "DELETE FROM MsgTemp WHERE UserID = ";
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                conn.Open();
                using (SqlCommand check = new SqlCommand(sqlID, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    while (reader.Read())
                        Id = (int)reader["ID"];

                    sqlDelete += "'" + Id + "'";
                }
                conn.Close();

                using (SqlCommand comm = new SqlCommand(sqlDelete, conn))
                {
                    conn.Open();

                    try
                    {
                        comm.ExecuteNonQuery();
                        Console.WriteLine("TEMP DELETED");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: " + ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void InsertDB(ref string _conLine, int Id, ref byte[] array)
        {
            string sql = "INSERT INTO MsgTemp(UserID, MsgText) VALUES(@Id, @File)";

            using (SqlConnection conn = new SqlConnection(_conLine))
            using (SqlCommand insert = new SqlCommand(sql, conn))
            {
                insert.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                insert.Parameters.Add("@File", SqlDbType.VarBinary).Value = array;

                conn.Open();
                insert.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("INSERT");
        }

        private void UpdateDB(ref string _conLine, int Id, ref byte[] array)
        {
            string SqlUpdate = "UPDATE MsgTemp SET MsgText = @File WHERE UserID = " + Id;

            using (SqlConnection conn = new SqlConnection(_conLine))
            using (SqlCommand update = new SqlCommand(SqlUpdate, conn))
            {
                update.Parameters.Add("@File", SqlDbType.VarBinary).Value = array;

                conn.Open();
                update.ExecuteNonQuery();
                conn.Close();
            }

            Console.WriteLine("UPDATE");
        }
    }
}
