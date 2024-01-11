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
    /// Logika interakcji dla klasy InsertUsedRecordWindow.xaml
    /// </summary>
    public partial class InsertUsedRecordWindow : Window
    {
        UsedDBConnect usedDBConnect = new UsedDBConnect();

        public InsertUsedRecordWindow()
        {
            InitializeComponent();
        }

        private void InsertUsedEntry(object sender, RoutedEventArgs e)
        {
            String valuesString = "";

            if (IsSetCorrectly())
            {
                

                valuesString += "'" + InsertSizeTextBox.Text.Trim().ToLower() + "',";
                valuesString += "" + InsertPriceTextBox.Text.Trim().ToLower().Replace(',', '.') + "";

                usedDBConnect.Insert(valuesString);
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
            if (!(string.IsNullOrWhiteSpace(InsertSizeTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(InsertPriceTextBox.Text.Trim())))
            {
                if (float.TryParse(InsertSizeTextBox.Text.Trim(), out float parsedSize))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
