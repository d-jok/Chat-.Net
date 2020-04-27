using System;
using System.Configuration;
using System.Data.SqlClient;

namespace chatServer
{
    class Login
    {
        private string Email;
        private string Password;
        private string status;

        public string Log(string data)
        {
            //Console.WriteLine("Login!!!");

            Email = data.Split(',')[0];
            Password = data.Split(',')[1];

            //Console.WriteLine(Email + " " + Password);

            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sql = "SELECT Name, Surname, NickName, Email, Password, Phone FROM Users";

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
                                status = "Success " + Email + " " + Password + " " + reader["NickName"].ToString() 
                                    + " " + reader["Phone"].ToString() + " " + reader["Name"] + " " + reader["Surname"];
                                break;
                            }
                            else
                            {
                                status = "Error Email Password";
                            }
                        }
                    }
                }
            }

            return status;
        }
    }
}
