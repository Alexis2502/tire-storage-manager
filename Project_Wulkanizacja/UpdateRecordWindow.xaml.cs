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

                setString += "nr_rejestracyjny='" + UpdateRegistrationNumberTextBox.Text.Trim() + "', ";
                setString += "marka_samochodu'" + UpdateCarBrandTextBox.Text.Trim() + "', ";
                setString += "kola_opony='" + UpdateWheelTireComboBox.Text.Trim() + "', ";
                setString += "rozmiar='" + UpdateSizeTextBox.Text.Trim() + "', ";
                setString += "jakosc='" + UpdateQualityComboBox.Text.Trim() + "', ";
                setString += "nr_magazynu=" + UpdateWarehouseNumberTextBox.Text.Trim() + ", ";
                setString += "status='" + UpdateStatusComboBox.Text.Trim() + "'";



                dBConnect.Update(setString, afterWhereString);

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
                if (int.TryParse(UpdateSizeTextBox.Text.Trim(), out int parsedSize) && int.TryParse(UpdateWarehouseNumberTextBox.Text.Trim(), out int parsed2))
                {
                    if (!(parsed2 > 0 && parsed2 <= 210))
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

    }
}
