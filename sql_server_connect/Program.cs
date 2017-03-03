using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace sql_server_connect
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection();
            //sqlConnection = new SqlConnection("Data Source=SQLEXPRESS; Integrated Security=SSPI; Initial Catalog=db3;");
            //sqlConnection = new SqlConnection("Server=localhost\SQLEXPRESS; Database=db3; uid= sa; pwd=password1");
            sqlConnection = new SqlConnection("Server=localhost" + "\\" + "SQLEXPRESS; Database=db3; uid= sa; pwd=password1");
            //sqlConnection.Open();
            Console.WriteLine("open connection");
            HasRows5(sqlConnection);
            sqlConnection.Close();
            Console.WriteLine("close connection");
            Console.ReadLine();
            sqlConnection.Dispose();
        }


        static void HasRows5(SqlConnection connection)
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                    // "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE' and TABLE_NAME != 'sysdiagrams'",
                     "SELECT name FROM sysobjects WHERE status>=0 AND type='U' ORDER BY name;",
                  connection);
                connection.Open();


                /*                                            */

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Михаил\Documents\doc\write1.sql", true))
                        {
                            // file.WriteLine("Fourth line");
                            file.WriteLine("{0}", reader.GetString(0));
                            Console.WriteLine("{0}", reader.GetString(0));
                            Console.WriteLine("записано в файл.");
                            string name = reader.GetString(0);
                            Console.WriteLine(name + " name");



                            // "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE' and TABLE_NAME != 'sysdiagrams'",

                        }
                        Console.WriteLine("{0}", reader.GetString(0));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            connection.Close();
            SqlConnection connection1 = new SqlConnection();
            connection1 = new SqlConnection("Server=localhost" + "\\" + "SQLEXPRESS; Database=db3; uid= sa; pwd=password1");
            connection1.Open();
            string s1;
            /***************************************/
            string path = @"C:\Users\Михаил\Documents\doc\write1.sql";



            // This text is always added, making the file longer over time
            // if it is not deleted.
            /*Working*/
            // Open the file to read from.co
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                string connectionString = "Server=localhost" + "\\" + "SQLEXPRESS; Database=db3; uid= sa; pwd=password1";
                SqlDataReader reader;
                Console.WriteLine("s= " + s);
                string query = "Select * from " + s;
                string connStr = @"Server=localhost" + "\\" + "SQLEXPRESS; Database=db3; uid= sa; pwd=password1";
                string strDelimiter = ", ";
                string strFilePath = @"C:\\Users\\Михаил\\Documents\\doc\\" + s + ".sql";
                SqlCommand command1 = new SqlCommand(
                    // "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE' and TABLE_NAME != 'sysdiagrams'",
                     "select column_name from information_schema.columns where table_name ='" + s + "' order by ordinal_position", connection1);
                //connection1.Open();
                Console.WriteLine("Hello world!");
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (reader = new SqlCommand(query, conn).ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            StringBuilder sb = new StringBuilder();
                            Object[] items = new Object[reader.FieldCount];
                            int counter = 0;
                            while (reader.Read())
                            {
                                reader.GetValues(items);
                                foreach (var item in items)
                                {
                                    // Using two sb.Append is better because if you 
                                    // concat the two strings it will first build a new string
                                    // and then discard it after its use.
                                   
                                    
                                   // sb.Append("\n");

                                    sb.Append(item.ToString());
                                    sb.Append(strDelimiter);
                                    counter += 1;
                                    Console.WriteLine("counter " + counter);
                                    if (counter % items.Length == 0)
                                    {
                                        sb.AppendLine();
                                    }
                                }
                                sb.Append("\n");
                                File.WriteAllText(strFilePath, sb.ToString());
                            }
                        }
                    }
                    conn.Close();
                }


                // Console.WriteLine(s);
            }
        }
    }
}