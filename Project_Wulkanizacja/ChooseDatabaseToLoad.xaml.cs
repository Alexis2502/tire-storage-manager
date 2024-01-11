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

namespace Project_Wulkanizacja
{
    /// <summary>
    /// Logika interakcji dla klasy ChooseDatabaseToLoad.xaml
    /// </summary>
    public partial class ChooseDatabaseToLoad : Window
    {
        public ChooseDatabaseToLoad()
        {
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
    }
}
