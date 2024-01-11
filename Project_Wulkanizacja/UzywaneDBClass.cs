using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Project_Wulkanizacja
{
    class UsedDBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public UsedDBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            MyCredentials credentials = new MyCredentials();
            using (StreamReader r = new StreamReader("credentials.json"))
            {
                string json = r.ReadToEnd();
                credentials = JsonConvert.DeserializeObject<MyCredentials>(json);
            }
            server = credentials.server;
            database = credentials.database;
            uid = credentials.uid;
            password = credentials.password;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void Insert(String valuesString)
        {
            string query = "INSERT INTO uzywane_opony (rozmiar, cena) VALUES(" + valuesString + ")";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public List<UsedEntry> SelectFromTable()
        {
            List<UsedEntry> results = new List<UsedEntry>();
            string query = "SELECT * FROM uzywane_opony";

            if (this.OpenConnection() == true)
            {
                results = connection.Query<UsedEntry>(query).AsList();

                this.CloseConnection();
            }
            return results;
        }

        public List<UsedEntry> SelectSizeFromTable(String rozmiar)
        {
            List<UsedEntry> results = new List<UsedEntry>();
            String query = "SELECT * FROM uzywane_opony WHERE rozmiar=" + rozmiar;

            if (this.OpenConnection() == true)
            {
                results = connection.Query<UsedEntry>(query).AsList();

                this.CloseConnection();
            }
            return results;
        }

        public void Update()
        {
            string query = "UPDATE uzywane_opony SET # WHERE #";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Delete(String afterWhere)
        {
            string query = "DELETE FROM uzywane_opony WHERE "+afterWhere;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
