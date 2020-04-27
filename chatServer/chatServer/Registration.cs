using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace chatServer
{
    class Registration
    {
        public string Name;
        public string Surname;
        public string NickName;
        public string Phone;
        public string Email;
        public string Password;

        public Registration()
        {

        }

        public Registration(string name, string surname, string nick, string phone, string email, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.NickName = nick;
            this.Phone = phone;
            this.Email = email;
            this.Password = password;
        }

        public string Register(string data)
        {
            //Console.WriteLine("GET!!!");
            bool RegCheck = true;
            string status = "";

            Name = data.Split(',')[0];
            Surname = data.Split(',')[1];
            NickName = data.Split(',')[2];
            Phone = data.Split(',')[3];
            Email = data.Split(',')[4];
            Password = data.Split(',')[5];

            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "INSERT INTO dbo.Users (Name, Surname, NickName, Email, Password, Phone) " +
                "VALUES ('" + Name + "', '" + Surname + "', '" + NickName + "', '" + Email + "', '" +
                Password + "', '" + Phone + "')";

            string sqlCheck = "SELECT Email, Phone FROM Users";
            
            Console.WriteLine(sql);

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlCommand check = new SqlCommand(sqlCheck, conn))
                    using (SqlDataReader reader = check.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Email == reader["Email"].ToString())
                            {
                                Console.WriteLine("Email already exist");
                                RegCheck = false;

                                status = "Email&";
                            }
                            if (Phone == reader["Phone"].ToString())
                            {
                                Console.WriteLine("Phone already exist");
                                RegCheck = false;

                                status += "Phone&";
                            }
                        }
                    }

                    conn.Close();

                    if (RegCheck == true)
                    {
                        conn.Open();

                        try
                        {
                            comm.ExecuteNonQuery();
                            Console.WriteLine("New client " + Email + " Saved in db :)");

                            status = "Ви були успішно зареєстровані";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("ERROR: " + ex);

                            status = "ERROR: Сбой на сервере";
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            return status;

            /*using (SqlConnection conn = new SqlConnection(_conLine))
            using (SqlCommand comm = new SqlCommand(sql, conn))
            { 
                comm.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = Name;
                comm.Parameters.Add("@surname", System.Data.SqlDbType.VarChar).Value = Surname;
                comm.Parameters.Add("@nickName", System.Data.SqlDbType.VarChar).Value = NickName;
                comm.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = Email;
                comm.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = Password;
                comm.Parameters.Add("@phone", System.Data.SqlDbType.VarChar).Value = Phone;

                conn.Open();

                try
                {
                    comm.ExecuteNonQuery();
                    Console.WriteLine("New client " + Email + " Saved in db :)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex);
                }
                finally
                {
                    conn.Close();
                }
            }*/
        }
    }
}
