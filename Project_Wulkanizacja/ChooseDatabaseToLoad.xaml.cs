using System;
using System.IO;
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

namespace Project_Wulkanizacja
{
    /// <summary>
    /// Logika interakcji dla klasy ChooseDatabaseToLoad.xaml
    /// </summary>
    public partial class ChooseDatabaseToLoad : Window
    {
        string filepath = "credentials.json";
        public ChooseDatabaseToLoad()
        {
            if (!File.Exists(filepath))
            {
                CreateJsonFile(filepath);
            }
            InitializeComponent();
        }

        private void ShowWulkanizacja(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ShowUsedTires(object sender, RoutedEventArgs e)
        {
            UsedTiresWindow usedTiresWindow = new UsedTiresWindow();
            usedTiresWindow.Show();
            this.Close();
        }

        private void CreateJsonFile(String filePath)
        {
            var credentials = new MyCredentials
            {
                server = "localhost",
                database = "magazyn_opon",
                uid = "root",
                password = "kazz"
            };

            string jsonContent = JsonConvert.SerializeObject(credentials, Formatting.Indented);

            File.WriteAllText(filepath, jsonContent);
        }
    }
}
