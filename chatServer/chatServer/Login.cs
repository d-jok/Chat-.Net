using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace chatServer
{
    class Login
    {
        private string Email;
        private string Password;
        private string status;

        private bool LogCheck = true;

        public string Log(string data)
        {
            Console.WriteLine("Login!!!");

            Email = data.Split(',')[0];
            Password = data.Split(',')[1];

            Console.WriteLine(Email + " " + Password);

            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "SELECT NickName, Email, Password, Phone FROM Users";

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Email == reader["Email"].ToString() && Password == reader["Password"].ToString())
                            {
                                /*status += "Error ";

                                if (Email != reader["Email"].ToString())
                                {
                                    Console.WriteLine("Wrong Email");
                                    LogCheck = false;

                                    status += "Email ";
                                }
                                if (Password != reader["Password"].ToString())
                                {
                                    Console.WriteLine("Wrong Password");
                                    LogCheck = false;

                                    status += "Password ";
                                }*/
                                status = "Success " + Email + " " + Password + " " + reader["NickName"].ToString() 
                                    + " " + reader["Phone"].ToString();
                                break;
                            }
                            else
                            {
                                status = "Error Email Password";
                            }
                            /*if (LogCheck == true)
                            {
                                status = "Success " + Email + " " + Password + " " + reader["NickName"].ToString();
                            }*/
                        }
                    }
                }
            }

            return status;
        }
    }
}
