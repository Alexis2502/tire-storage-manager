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

    public partial class InsertRecordWindow : Window
    {

        DBConnect dBConnect = new DBConnect();

        List<String> WheelOrTire;
        List<String> Qualities;
        List<String> Statuses;


        public InsertRecordWindow(List<String> wheelOrTire, List<String> qualities, List<String> statuses)
        {
            WheelOrTire = wheelOrTire;
            Qualities = qualities;
            Statuses = statuses;

            InitializeComponent();

            InsertWheelTireComboBox.ItemsSource = WheelOrTire;
            InsertQualityComboBox.ItemsSource = Qualities;
            InsertStatusComboBox.ItemsSource = Statuses;
        }

        private void InsertRecord(object sender, RoutedEventArgs e)
        {
            if (IsSetCorrectly())
            {
                String valuesString = "";
                valuesString += "'" + InsertRegistrationNumberTextBox.Text.Trim() + "', ";
                valuesString += "'" + InsertCarBrandTextBox.Text + "', ";
                valuesString += "'" + InsertWheelTireComboBox.Text.Trim() + ", ";
                valuesString += "'" + InsertSizeTextBox.Text.Trim() + ", ";
                valuesString += "'" + InsertQualityComboBox.Text.Trim() + "', ";
                valuesString += "" + InsertWarehouseNumberTextBox.Text.Trim() + ", ";
                valuesString += "'" + InsertStatusComboBox.Text.Trim() + "'";

                dBConnect.Insert(valuesString);

            }
            else
            {
                MessageWindow messageWindow = new MessageWindow("Któraś z wartości nie została poprawnie wprowadzona");
                messageWindow.ShowDialog();
            }
        }

        private bool IsSetCorrectly()
        {
            if (!(string.IsNullOrWhiteSpace(InsertRegistrationNumberTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(InsertCarBrandTextBox.Text.Trim()) && InsertWheelTireComboBox.SelectedValue == null && string.IsNullOrWhiteSpace(InsertSizeTextBox.Text.Trim()) && InsertWarehouseNumberTextBox == null && InsertStatusComboBox == null))
            {
                if (int.TryParse(InsertSizeTextBox.Text.Trim(), out int parsedSize) && int.TryParse(InsertWarehouseNumberTextBox.Text.Trim(), out int parsed2))
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
