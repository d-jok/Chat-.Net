using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace chatServer
{
    class Search
    {
        public string FindUser(string phone)
        {
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = "SELECT Name, Surname, NickName, Email, Phone FROM Users WHERE Phone = " + phone;
            string answer = "#Search No result";

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            if(phone == reader["Phone"].ToString())
                            {
                                answer = "#Search " + reader["Name"].ToString() + " " + reader["Surname"].ToString() + " " +
                                    reader["NickName"].ToString() + " " + reader["Email"].ToString() + " " + 
                                    reader["Phone"].ToString();
                            }
                        }
                    }
                }
            }

            return answer;
        }
    }
}
