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
            insertRecordWindow.ShowDialog();
            AllStorageEntries = dBConnect.SelectFromTable();
            ResultsGrid.ItemsSource = AllStorageEntries;
        }

        private void UpdateRecord(object sender, RoutedEventArgs e)
        {
            UpdateRecordWindow updateRecordWindow = new UpdateRecordWindow(WheelOrTire, Qualities, Statuses, SelectedStorageEntry);
            updateRecordWindow.ShowDialog();
            AllStorageEntries = dBConnect.SelectFromTable();
            ResultsGrid.ItemsSource = AllStorageEntries;
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteWindow confirmDeleteWindow = new ConfirmDeleteWindow(SelectedStorageEntry);
            confirmDeleteWindow.ShowDialog();
            AllStorageEntries = dBConnect.SelectFromTable();
            ResultsGrid.ItemsSource = AllStorageEntries;
        }

        private void ShowRemarks(object sender, RoutedEventArgs e)
        {
            RemarkTextBox.IsEnabled = true;
            RemarkTextBox.Visibility = Visibility.Visible;            
            RemarkSaveButton.IsEnabled = true;
            RemarkSaveButton.Visibility = Visibility.Visible;            
            RemarkHideButton.IsEnabled = true;
            RemarkHideButton.Visibility = Visibility.Visible;
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
                ResultsGrid.ItemsSource = AllStorageEntries;
            }
            else
            {
                String registrationAfterWhere = "'"+RegistrationInputTextBox.Text+"'";
                AllStorageEntries = dBConnect.SelectLicenseFromTable(registrationAfterWhere);
                ResultsGrid.ItemsSource = AllStorageEntries;
            }
        }
    }
}
