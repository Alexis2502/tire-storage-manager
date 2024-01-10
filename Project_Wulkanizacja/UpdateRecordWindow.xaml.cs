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

        public UpdateRecordWindow(List<String> wheelOrTire, List<String> qualities, List<String> statuses)
        {
            WheelOrTire = wheelOrTire;
            Qualities = qualities;
            Statuses = statuses;

            InitializeComponent();

            UpdateWheelTireComboBox.ItemsSource = WheelOrTire;
            UpdateQualityComboBox.ItemsSource = Qualities;
            UpdateStatusComboBox.ItemsSource = Statuses;
        }

        private void UpdateRecord(object sender, RoutedEventArgs e)
        {
            if (IsSetCorrectly())
            {
                String setString = "";
                String valuesString = "";
                valuesString += "'" + UpdateRegistrationNumberTextBox.Text.Trim() + "', ";
                valuesString += "'" + UpdateCarBrandTextBox.Text.Trim() + "', ";
                valuesString += "'" + UpdateWheelTireComboBox.Text.Trim() + ", ";
                valuesString += "'" + UpdateSizeTextBox.Text.Trim() + ", ";
                valuesString += "'" + UpdateQualityComboBox.Text.Trim() + "', ";
                valuesString += "" + UpdateWarehouseNumberTextBox.Text.Trim() + ", ";
                valuesString += "'" + UpdateStatusComboBox.Text.Trim() + "'";


                dBConnect.Update();

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
