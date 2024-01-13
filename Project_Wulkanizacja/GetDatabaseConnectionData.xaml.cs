using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace Project_Wulkanizacja
{
    /// <summary>
    /// Logika interakcji dla klasy GetDatabaseConnectionData.xaml
    /// </summary>
    public partial class GetDatabaseConnectionData : Window
    {
        string filepath = "credentials.json";
        bool properlyFilled = false;
        public GetDatabaseConnectionData()
        {

            if (File.Exists(filepath))
            {
                FillInputsIfFileExist();
            }

            InitializeComponent();

            Closing += GetDatabaseConnectionData_Closing;
        }

        private void SaveCredentials(object sender, RoutedEventArgs e)
        {
            if(!(string.IsNullOrWhiteSpace(ServerTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(DatabaseNameTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(UIDTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(PasswordTextBox.Text.Trim())))
            {
                string server = ServerTextBox.Text.Trim().ToLower();
                string database = DatabaseNameTextBox.Text.Trim().ToLower();
                string username = UIDTextBox.Text.Trim().ToLower();
                string password = PasswordTextBox.Text.Trim();//password is the only thing not made to lowercase for increased security reason i guess?

                if (!File.Exists(filepath))
                {
                    CreateJsonFile(server, database, username, password);
                }
                else
                {
                    string existingCredentialsContent = File.ReadAllText(filepath);

                    var existingCredentials = JsonConvert.DeserializeObject<MyCredentials>(existingCredentialsContent);

                    existingCredentials.server = server;
                    existingCredentials.database = database;
                    existingCredentials.uid = username;
                    existingCredentials.password = password;

                    string updatedCredentialsContent = JsonConvert.SerializeObject(existingCredentials);

                    File.WriteAllText(filepath, updatedCredentialsContent);
                }
                properlyFilled = true;
                CloseThisWindow();
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić dane!");
            }
        }

        private void CreateJsonFile(String server, String database, String username, String password)
        {
            var credentials = new MyCredentials
            {
                server = server,
                database = database,
                uid = username,
                password = password
            };

            string jsonContent = JsonConvert.SerializeObject(credentials, Formatting.Indented);

            File.WriteAllText(filepath, jsonContent);
        }

        private void FillInputsIfFileExist()
        {
            string existingCredentialsContent = File.ReadAllText(filepath);

            var existingCredentials = JsonConvert.DeserializeObject<MyCredentials>(existingCredentialsContent);

            ServerTextBox.Text = existingCredentials.server;
            DatabaseNameTextBox.Text = existingCredentials.database;
            UIDTextBox.Text = existingCredentials.uid;
            PasswordTextBox.Text = existingCredentials.password;
        }

        private void GetDatabaseConnectionData_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(properlyFilled == false)
            {
                e.Cancel = true;
                MessageBox.Show("Proszę wprowadzić dane i wyjść przyciskiem 'Zapisz'");
            }
        }

        private void CloseThisWindow()
        {
            this.Close();
        }
    }
}
