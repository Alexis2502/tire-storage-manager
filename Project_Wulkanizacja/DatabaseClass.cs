using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Dapper;

namespace Project_Wulkanizacja
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnect()
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
            string query = "INSERT INTO opony (nr_rejestracyjny, marka_samochodu, kola/opony, rozmiar, jakosc, nr_magazynu, status) VALUES("+valuesString+")";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public List<StorageEntry> SelectFromTable()
        {
            List<StorageEntry> results = new List<StorageEntry>();
            string query = "SELECT * FROM opony";

            if (this.OpenConnection() == true)
            {
                results = connection.Query<StorageEntry>(query).AsList();

                this.CloseConnection();
            }
            return results;
        }

        public void Update()
        {//
            string query = "UPDATE opony SET # WHERE #";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Delete()
        {
            string query = "DELETE FROM opony WHERE #";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
