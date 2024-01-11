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
    /// Logika interakcji dla klasy UsedTiresWindow.xaml
    /// </summary>
    public partial class UsedTiresWindow : Window
    {
        UsedDBConnect usedDBConnect = new UsedDBConnect();

        List<UsedEntry> UsedStorageEntries;
        UsedEntry SelectedUsedEntry;


        public UsedTiresWindow()
        {
            InitializeComponent();
            UsedStorageEntries = usedDBConnect.SelectFromTable();
            UsedResultsGrid.ItemsSource = UsedStorageEntries;
        }

        private void UsedResultsGridSelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectedUsedEntry = UsedResultsGrid.SelectedItem as UsedEntry;

            UpdateRecordButton.IsEnabled = true;
            DeleteRecordButton.IsEnabled = true;
        }

        private void GoToWulkanizacja(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteUsedWindow confirmDeleteUsedWindow = new ConfirmDeleteUsedWindow(SelectedUsedEntry);
            confirmDeleteUsedWindow.ShowDialog();
            UsedStorageEntries = usedDBConnect.SelectFromTable();
            UsedResultsGrid.ItemsSource = UsedStorageEntries;
        }

        private void UpdateRecordClick(object sender, RoutedEventArgs e)
        {
            UpdateUsedRecordWindow updateUsedRecordWindow = new UpdateUsedRecordWindow(SelectedUsedEntry);
            updateUsedRecordWindow.ShowDialog();
            UsedStorageEntries = usedDBConnect.SelectFromTable();
            UsedResultsGrid.ItemsSource = UsedStorageEntries;
        }

        private void InsertRecord(object sender, RoutedEventArgs e)
        {
            InsertUsedRecordWindow insertUsedRecordWindow = new InsertUsedRecordWindow();
            insertUsedRecordWindow.ShowDialog();

            UsedStorageEntries = usedDBConnect.SelectFromTable();
            UsedResultsGrid.ItemsSource = UsedStorageEntries;
        }

        private void SearchBySize(object sender, RoutedEventArgs e)
        {

        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            UsedStorageEntries = usedDBConnect.SelectFromTable();
            UsedResultsGrid.ItemsSource = UsedStorageEntries;
        }
    }
}
