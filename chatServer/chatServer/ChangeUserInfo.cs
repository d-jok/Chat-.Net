using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace chatServer
{
    class ChangeUserInfo
    {
        private struct Parse
        {
            public string field;
            public string value;

            public Parse(string field, string value)
            {
                this.field = field;
                this.value = value;
            }
        }

        private void Parser(ref List<Parse> list, ref string fields, ref string data)
        {
            int fCounter = 0;

            for(int i = 0; i < fields.Length; i++)
                if (fields[i] == '/')
                    fCounter++;

            //fCounter++;
            for(int i = 0; i < fCounter; i++)
            {
                list.Add(new Parse(fields.Split('/')[i], data.Split(' ')[i]));
            }
        }

        private string CheckPassword(ref string passwords, ref string number, ref string _conLine)
        {
            string password = "";
            string sql = "SELECT Password FROM Users WHERE Phone = '" + number + "'";

            using (SqlConnection conn = new SqlConnection(_conLine))
            {
                conn.Open();
                using (SqlCommand check = new SqlCommand(sql, conn))
                using (SqlDataReader reader = check.ExecuteReader())
                {
                    while (reader.Read())
                        password = (string)reader["Password"];
                }
                conn.Close();
            }

            if (passwords.Split('/')[0] != password)
                return "Wrong";
            else if (passwords.Split('/')[0] == passwords.Split('/')[1])
                return "Same";

            return "OK";
        }

        public string Change(string number, string fields, string data)
        {
            int n = 0;
            string answer = "";
            string sqlUpdate = "UPDATE Users";
            string values = " SET";
            string _conLine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            List<Parse> parses = new List<Parse>();

            Console.WriteLine(fields);
            Console.WriteLine(data);

            Parser(ref parses, ref fields, ref data);
            n = parses.Count;
            string pass = parses[n - 1].value;

            if (parses[n - 1].field == "Password")
            {
                string temp = CheckPassword(ref pass, ref number, ref _conLine);

                if(temp == "OK")
                {
                    parses[n - 1] = new Parse("Password", pass.Split('/')[1]);

                    for(int i = 0; i < n; i++)
                    {
                        if (i == n - 1)
                            values += " " + parses[i].field + " = '" + parses[i].value + "'";
                        else
                            values += " " + parses[i].field + " = '" + parses[i].value + "',";
                    }
                }
                else if (temp == "Wrong")
                    return "Your old password is incorrect";
                else if (temp == "Same")
                    return "Your new password is the same as old one";
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (i == n - 1)
                        values += " " + parses[i].field + " = '" + parses[i].value + "'";
                    else
                        values += " " + parses[i].field + " = '" + parses[i].value + "',";
                }
            }

            foreach(var V in parses)
            {
                Console.WriteLine(V.field + " " + V.value);
            }

            sqlUpdate += values;
            Console.WriteLine(sqlUpdate);

            try
            {
                using (SqlConnection conn = new SqlConnection(_conLine))
                using (SqlCommand update = new SqlCommand(sqlUpdate, conn))
                {
                    conn.Open();
                    update.ExecuteNonQuery();
                    conn.Close();
                }

                answer = "Info change success";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                answer = "Info change error";
            }

            return answer;
        }
    }
}
