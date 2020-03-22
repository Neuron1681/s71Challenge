using MySql.Data.MySqlClient;
using System;

namespace s71Challenge
{
    class MySqlSender
    {
        string server = AppConfigManager.GetConfigString("DestinationServer", "localhost");
        string database = AppConfigManager.GetConfigString("DestinationDatabase", "localhost");
        string table = AppConfigManager.GetConfigString("DestinationTable", "localhost");
        string username = "";
        string password = "";
        string credentials = AppConfigManager.GetConfigString("DestinationCredentials", "localhost");
        string[] splitCredentials;
        MySqlManager mySqlmanager;
        public MySqlSender()
        {
            splitCredentials = credentials.Split(';');
            username = splitCredentials[0];
            password = splitCredentials[1];
            mySqlmanager = new MySqlManager(server, database, username, password);
        }
        /// <summary>
        /// Attempts to insert the given value to MySQL destination.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Bool value to reflect if insert was successful</returns>
        public bool InsertToMySql(string value)
        {
            string query = string.Format(@"INSERT INTO {0} (Value) VALUES (@Value)", table);
            using (MySqlConnection connection = new MySqlConnection(mySqlmanager.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("@Value", MySqlDbType.VarChar);
                    command.Parameters["@Value"].Value = value;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Writes the contents of the database to the console.
        /// </summary>
        public void WriteDbContentsToConsole()
        {
            string query = string.Format(@"SELECT Value FROM {0}.{1}", database, table);
            using (MySqlConnection connection = new MySqlConnection(mySqlmanager.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Value"]);
                        }
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
