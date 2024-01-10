using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            string query = "INSERT INTO opony (nr_rejestracyjny, marka_samochodu, kola_opony, rozmiar, jakosc, nr_magazynu, status) VALUES("+valuesString+")";

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
            string query = "SELECT id, nr_rejestracyjny, marka_samochodu, kola_opony, rozmiar, jakosc, nr_magazynu, status, notatki FROM opony";

            if (this.OpenConnection() == true)
            {
                results = connection.Query<StorageEntry>(query).AsList();

                this.CloseConnection();
            }
            return results;
        }

        public List<StorageEntry> SelectLicenseFromTable(String nr_rejestracyjny)
        {
            List<StorageEntry> results = new List<StorageEntry>();
            String query = "SELECT * FROM opony WHERE nr_rejestracyjny="+nr_rejestracyjny;

            if (this.OpenConnection() == true)
            {
                results = connection.Query<StorageEntry>(query).AsList();

                this.CloseConnection();
            }
            return results;
        }

        public void Update(String setString, String afterWHereString)
        {
            string query = "UPDATE opony SET "+setString+" WHERE "+afterWHereString;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Delete(String stringAfterWhere)
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
