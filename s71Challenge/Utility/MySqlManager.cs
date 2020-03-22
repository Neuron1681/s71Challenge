using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace s71Challenge
{
    public class MySqlManager
    {
        public string Server { get; set; } = "localhost";
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private string _ConnectionString;
        public MySqlManager()
        {

        }
        public MySqlManager(string inServer, string inDatabase, string inUsername, string inPassword)
        {
            SetConnectionString(inServer, inDatabase, inUsername, inPassword);
        }
        /// <summary>
        /// Attempts to return a closed connection using the connection string associated with the class.
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_ConnectionString))
            {
                return new MySqlConnection(_ConnectionString);
            }
            else // if the connection string wasn't formatted yet, attempt to do so with current properties.
            {
                if ((string.IsNullOrEmpty(Server)) || (string.IsNullOrEmpty(Database)) || (string.IsNullOrEmpty(Username))) // We don't check the password in case the credentials are set to windows credentials. 
                {
                    throw new Exception("SqlManager properties have not been initialized yet!");
                }
                SetConnectionString(Server, Database, Username, Password);
                return new MySqlConnection(_ConnectionString);
            }
        }
        /// <summary>
        /// Sets the connection string property of the class.
        /// </summary>
        /// <param name="inServer"></param>
        /// <param name="inDatabase"></param>
        /// <param name="inUsername"></param>
        /// <param name="inPassword"></param>
        public void SetConnectionString(string inServer, string inDatabase, string inUsername, string inPassword)
        {
            Server = inServer.Trim();
            Database = inDatabase.Trim();
            Username = inUsername.Trim();
            Password = inPassword.Trim();
            _ConnectionString = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, Database, Username, Password);
        }
        /// <summary>
        /// Returns the connection string belonging to the class.
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return _ConnectionString;
        }
    }
}
