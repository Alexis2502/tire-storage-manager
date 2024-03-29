﻿using System;
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

        List<String> WheelOrTire = new List<String> { "koła", "opony", "" };
        List<String> Qualities = new List<String> { "A", "B", "C", "D", "" };
        //List<String> WarehouseNumbers = new List<String> {"1", "2", "3", "4" };
        List<String> Statuses = new List<String> { "odebrane", "nieodebrane", "" };

        List<StorageEntry> AllStorageEntries;
        StorageEntry SelectedStorageEntry;
        int TargetId;

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
            SelectedStorageEntry = ResultsGrid.SelectedItem as StorageEntry;

            if(SelectedStorageEntry == null)
            {
                RecordEditButton.IsEnabled = false;
                RecordDeleteButton.IsEnabled = false;
                RemarkShowButton.IsEnabled = false;
                RemarkTextBox.IsEnabled = false;
                RemarkTextBox.Visibility = Visibility.Hidden;
                RemarkLabel.IsEnabled = false;
                RemarkLabel.Visibility = Visibility.Hidden;
                RemarkSaveButton.IsEnabled = false;
                RemarkSaveButton.Visibility = Visibility.Hidden;
                RemarkHideButton.IsEnabled = false;
                RemarkHideButton.Visibility = Visibility.Hidden;
            }
            else
            {
                RecordEditButton.IsEnabled = true;
                RecordDeleteButton.IsEnabled = true;
                RemarkShowButton.IsEnabled = true;
            }

            if(RemarkTextBox.Visibility == Visibility.Visible && IsIdInShownRecords(TargetId))
            {
                RemarkTextBox.Text = SelectedStorageEntry.notatki;
            }
            else
            {
                RemarkTextBox.Text = "";
            }

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
            RemarkLabel.IsEnabled = true;
            RemarkLabel.Visibility = Visibility.Visible;

            RemarkTextBox.Text = SelectedStorageEntry.notatki;
        }

        private void HideRemarks(object sender, RoutedEventArgs e)
        {
            RemarkTextBox.IsEnabled = false;
            RemarkTextBox.Visibility = Visibility.Hidden;
            RemarkSaveButton.IsEnabled = false;
            RemarkSaveButton.Visibility = Visibility.Hidden;
            RemarkHideButton.IsEnabled = false;
            RemarkHideButton.Visibility = Visibility.Hidden;
            RemarkLabel.IsEnabled = false;
            RemarkLabel.Visibility = Visibility.Hidden;
        }

        private void SaveRemark(object sender, RoutedEventArgs e)
        {
            string setString = "notatki='"+RemarkTextBox.Text+"'";
            string afterWhereString = "id="+SelectedStorageEntry.id.ToString();
            SelectedStorageEntry.notatki = RemarkTextBox.Text;
            dBConnect.Update(setString, afterWhereString);

            TargetId = SelectedStorageEntry.id;

            AllStorageEntries = dBConnect.SelectFromTable();
            SelectDataGridItemById(TargetId);
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
                String registrationAfterWhere = "'"+RegistrationInputTextBox.Text.Trim().ToLower() + "'";
                AllStorageEntries = dBConnect.SelectLicenseFromTable(registrationAfterWhere);
                ResultsGrid.ItemsSource = AllStorageEntries;
            }
        }

        private bool IsIdInShownRecords(int targetId)
        {
            foreach (StorageEntry entry in ResultsGrid.Items)
            {
                if(entry.id == targetId)
                {
                    return true;
                }
            }
            return false;
        }

        private void SelectDataGridItemById(int targetId)
        {

            foreach (StorageEntry entry in ResultsGrid.Items)
            {
                if(entry.id == targetId)
                {
                    ResultsGrid.SelectedItem = entry;

                    break;
                }
            }

        }

        private void FilterRecords(object sender, RoutedEventArgs e)
        {
            String filterString = "";
            //TextBox
            if (!(string.IsNullOrWhiteSpace(FilterCarManufacturerTextBox.Text.Trim())))
            {
                filterString += "marka_samochodu='" + FilterCarManufacturerTextBox.Text.Trim().ToLower() + "' AND ";
            }

            //checkbox
            if(FilterWheelTiresComboBox.SelectedValue != null)
            {
                filterString += "kola_opony='"+FilterWheelTiresComboBox.Text.Trim().ToLower() + "' AND ";
            }

            //checkbox
            if (FilterQualityComboBox.SelectedValue != null)
            {
                filterString += "jakosc='" + FilterQualityComboBox.Text.Trim().ToLower() + "' AND ";
            }

            //TextBox
            if (!(string.IsNullOrWhiteSpace(FilterWarehouseNumberTextBox.Text.Trim())))
            {
                if(int.TryParse(FilterWarehouseNumberTextBox.Text.Trim(), out int parsedWarehouseNumber))
                {
                    filterString += "nr_magazynu=" + parsedWarehouseNumber + " AND ";
                }
            }

            //checkbox
            if (FilterStatusComboBox.SelectedValue != null)
            {
                filterString += "status='" + FilterStatusComboBox.Text.Trim().ToLower() + "' AND ";
            }

            //removing last ','
            if(!string.IsNullOrWhiteSpace(filterString) && filterString.EndsWith(" AND "))
            {
                filterString = filterString.Substring(0, filterString.Length - 5);
                AllStorageEntries = dBConnect.SelectWithFilters(filterString);
            }
            else if(string.IsNullOrWhiteSpace(filterString)){
                //in case of no filters applied
                AllStorageEntries = dBConnect.SelectFromTable();
            }

            ResultsGrid.ItemsSource = AllStorageEntries;
        }

        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            FilterCarManufacturerTextBox.Text = "";
            FilterWheelTiresComboBox.SelectedValue = null;
            FilterQualityComboBox.SelectedValue = null;
            FilterWarehouseNumberTextBox.Text = "";
            FilterStatusComboBox.SelectedValue = null;
        }

        private void GoToUsedTires(object sender, RoutedEventArgs e)
        {
            UsedTiresWindow usedTiresWindow = new UsedTiresWindow();
            usedTiresWindow.Show();
            this.Close();
        }
    }
}
