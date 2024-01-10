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
                valuesString += "'" + UpdateRegistrationNumberTextBox.Text + "', ";
                valuesString += "'" + UpdateCarBrandTextBox.Text + "', ";
                valuesString += "'" + UpdateWheelTireComboBox.Text + ", ";
                valuesString += "'" + UpdateSizeTextBox.Text + ", ";
                valuesString += "'" + UpdateQualityComboBox.Text + "', ";
                valuesString += "" + UpdateWarehouseNumberTextBox.Text + ", ";
                valuesString += "'" + UpdateStatusComboBox.Text + "'";


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
            if (!(string.IsNullOrWhiteSpace(UpdateRegistrationNumberTextBox.Text) && string.IsNullOrWhiteSpace(UpdateCarBrandTextBox.Text) && UpdateWheelTireComboBox.SelectedValue == null && string.IsNullOrWhiteSpace(UpdateSizeTextBox.Text) && UpdateWarehouseNumberTextBox == null && UpdateStatusComboBox == null))
            {
                if (int.TryParse(UpdateSizeTextBox.Text, out int parsedSize) && int.TryParse(UpdateWarehouseNumberTextBox.Text, out int parsed2))
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
