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
        List<String> WarehouseNumbers;
        List<String> Statuses;


        public InsertRecordWindow(List<String> wheelOrTire, List<String> qualities, List<String> warehouseNumbers, List<String> statuses)
        {
            WheelOrTire = wheelOrTire;
            Qualities = qualities;
            WarehouseNumbers = warehouseNumbers;
            Statuses = statuses;

            InitializeComponent();

            InsertWheelTireComboBox.ItemsSource = WheelOrTire;
            InsertQualityComboBox.ItemsSource = Qualities;
            InsertWarehouseComboBox.ItemsSource = WarehouseNumbers;
            InsertStatusComboBox.ItemsSource = Statuses;
        }

        private void InsertRecord(object sender, RoutedEventArgs e)
        {
            if (IsSetCorrectly())
            {
                String valuesString = "";
                valuesString += "'" + InsertRegistrationNumberTextBox.Text + "', ";
                valuesString += "'" + InsertCarBrandTextBox.Text + "', ";
                valuesString += "'" + InsertWheelTireComboBox.Text + ", ";
                valuesString += InsertSizeTextBox.Text + ", ";
                valuesString += "'" + InsertQualityComboBox.Text + "', ";
                valuesString += "'" + InsertWarehouseComboBox.Text + "', ";
                valuesString += "'" + InsertStatusComboBox.Text + "'";

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
            if (!(string.IsNullOrWhiteSpace(InsertRegistrationNumberTextBox.Text) && string.IsNullOrWhiteSpace(InsertCarBrandTextBox.Text) && InsertWheelTireComboBox.SelectedValue == null && string.IsNullOrWhiteSpace(InsertSizeTextBox.Text) && InsertWarehouseComboBox == null && InsertStatusComboBox == null))
            {
                if(int.TryParse(InsertSizeTextBox.Text, out int parsedSize)){
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
