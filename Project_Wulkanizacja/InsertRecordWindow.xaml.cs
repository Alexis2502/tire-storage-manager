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
    }
}
