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
        DBConnect dBConnect = new DBConnect();

        List<String> WheelOrTire = new List<String> { "koła", "opony" };
        List<String> Qualities = new List<String> { "A", "B", "C", "D" };
        //List<String> WarehouseNumbers = new List<String> {"1", "2", "3", "4" };
        List<String> Statuses = new List<String> { "odebrane", "nieodebrane" };

        List<StorageEntry> AllStorageEntries;
        StorageEntry SelectedStorageEntry;

        public MainWindow()
        {
            InitializeComponent();
            AllStorageEntries = dBConnect.SelectFromTable();
            ResultsGrid.ItemsSource = AllStorageEntries;

            FilterWheelTiresComboBox.ItemsSource = WheelOrTire;
            FilterQualityComboBox.ItemsSource = Qualities;
            FilterStatusComboBox.ItemsSource = Statuses;

        }

        private void DataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RecordEditButton.IsEnabled = true;
            RecordDeleteButton.IsEnabled = true;
            RemarkShowButton.IsEnabled = true;

            SelectedStorageEntry = ResultsGrid.SelectedItem as StorageEntry;
        }

        private void RecordInsertButton_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertRecordWindow = new InsertRecordWindow(WheelOrTire, Qualities, Statuses);
            //ShowDialog() blokuje interakcje z oknem, w którym wywołane zostało nowe
            //w przeciwieństwie do Show()
            insertRecordWindow.ShowDialog();
        }

        private void UpdateRecord(object sender, RoutedEventArgs e)
        {
            UpdateRecordWindow updateRecordWindow = new UpdateRecordWindow(WheelOrTire, Qualities, Statuses, SelectedStorageEntry);
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow(SelectedStorageEntry);
            confirmDeleteWindow.ShowDialog();
        }

        private void ShowRemarks(object sender, RoutedEventArgs e)
        {

        }

        private void HideRemarks(object sender, RoutedEventArgs e)
        {

        }

        private void SaveRemark(object sender, RoutedEventArgs e)
        {

        }

        private void SearchByRegistrationNumber(object sender, RoutedEventArgs e)
        {
            if(RegistrationInputTextBox.Text.Trim() == String.Empty)
            {
                AllStorageEntries = dBConnect.SelectFromTable();
            }
            else
            {
                dBConnect.SelectLicenseFromTable(RegistrationInputTextBox.Text);
            }
        }
    }
}
