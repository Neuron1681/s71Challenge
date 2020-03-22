using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace s71Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = AppConfigManager.GetConfigString("DestinationServer", "localhost");
            string database = AppConfigManager.GetConfigString("DestinationDatabase", "localhost");
            string table = AppConfigManager.GetConfigString("DestinationTable", "localhost");
            string username = "";
            string password = "";
            string credentials = AppConfigManager.GetConfigString("DestinationCredentials", "localhost");
            string[] splitCredentials = credentials.Split(';');
            username = splitCredentials[0];
            password = splitCredentials[1];
            MySqlManager mySqlmanager = new MySqlManager(server, database, username, password);
            string query = string.Format(@"INSERT INTO {0} (Value) VALUES('abcd')", table);
            using (MySqlConnection connection = new MySqlConnection(mySqlmanager.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
