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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Wulkanizacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RecordInsertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertRecordWindow = new InsertRecordWindow();
            //ShowDialog() blokuje interakcje z oknem, w którym wywołane zostało nowe
            //w przeciwieństwie do Show()
            insertRecordWindow.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void HideRemarks(object sender, RoutedEventArgs e)
        {

        }
    }
}
