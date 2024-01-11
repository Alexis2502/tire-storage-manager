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
    /// Logika interakcji dla klasy InsertRecordWindow.xaml
    /// </summary>
    public partial class UpdateRecordWindow : Window
    {

        DBConnect dBConnect = new DBConnect();

        List<String> WheelOrTire;
        List<String> Qualities;
        List<String> Statuses;

        StorageEntry StorageEntryToBeEdited;

        public UpdateRecordWindow(List<String> wheelOrTire, List<String> qualities, List<String> statuses, StorageEntry storageEntry)
        {
            WheelOrTire = wheelOrTire;
            Qualities = qualities;
            Statuses = statuses;
            StorageEntryToBeEdited = storageEntry;

            InitializeComponent();

            UpdateWheelTireComboBox.ItemsSource = WheelOrTire;
            UpdateQualityComboBox.ItemsSource = Qualities;
            UpdateStatusComboBox.ItemsSource = Statuses;

            //set initial textbox values
            UpdateRegistrationNumberTextBox.Text = StorageEntryToBeEdited.nr_rejestracyjny;
            UpdateCarBrandTextBox.Text = StorageEntryToBeEdited.marka_samochodu;
            UpdateSizeTextBox.Text = StorageEntryToBeEdited.rozmiar;
            UpdateWarehouseNumberTextBox.Text = StorageEntryToBeEdited.nr_magazynu.ToString();
            
            //set initial comboboxes' values
            UpdateWheelTireComboBox.SelectedItem = StorageEntryToBeEdited.kola_opony;
            UpdateQualityComboBox.SelectedItem = StorageEntryToBeEdited.jakosc;
            UpdateStatusComboBox.SelectedItem = StorageEntryToBeEdited.status;
        }

        private void UpdateRecord(object sender, RoutedEventArgs e)
        {
            if (IsSetCorrectly())
            {
                String setString = "";
                String afterWhereString = "id="+StorageEntryToBeEdited.id;

                setString += "nr_rejestracyjny='" + UpdateRegistrationNumberTextBox.Text.Trim().ToLower() + "', ";
                setString += "marka_samochodu='" + UpdateCarBrandTextBox.Text.Trim().ToLower() + "', ";
                setString += "kola_opony='" + UpdateWheelTireComboBox.Text.Trim().ToLower() + "', ";
                setString += "rozmiar='" + UpdateSizeTextBox.Text.Trim().ToLower() + "', ";
                setString += "jakosc='" + UpdateQualityComboBox.Text.Trim().ToLower() + "', ";
                setString += "nr_magazynu=" + UpdateWarehouseNumberTextBox.Text.Trim().ToLower() + ", ";
                setString += "status='" + UpdateStatusComboBox.Text.Trim().ToLower() + "'";



                dBConnect.Update(setString, afterWhereString);
                this.Close();
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Któraś z wartości nie została poprawnie wprowadzona");
                messageWindow.ShowDialog();
            }
        }

        private bool IsSetCorrectly()
        {
            if (!(string.IsNullOrWhiteSpace(UpdateRegistrationNumberTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(UpdateCarBrandTextBox.Text.Trim()) && UpdateWheelTireComboBox.SelectedValue == null && string.IsNullOrWhiteSpace(UpdateSizeTextBox.Text.Trim()) && UpdateWarehouseNumberTextBox == null && UpdateStatusComboBox == null))
            {
                return true;

            }
            return false;
        }

    }
}
