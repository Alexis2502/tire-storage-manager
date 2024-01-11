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
    public partial class UpdateUsedRecordWindow : Window
    {
        UsedDBConnect usedDBConnect = new UsedDBConnect();
        UsedEntry usedEntry;

        public UpdateUsedRecordWindow(UsedEntry usedEntry)
        {
            InitializeComponent();
            this.usedEntry = usedEntry;

            UpdatePriceTextBox.Text = usedEntry.cena.ToString();
            UpdateSizeTextBox.Text = usedEntry.rozmiar;
        }

        private void UpdateUsedEntry(object sender, RoutedEventArgs e)
        {

            if (IsSetCorrectly())
            {
                String setString = "";
                String afterWhereString = "id=" + usedEntry.id;

                setString += "rozmiar='" + UpdateSizeTextBox.Text.Trim().ToLower() + "',";
                setString += "cena=" + UpdatePriceTextBox.Text.Trim().ToLower().Replace(',', '.') + "";

                usedDBConnect.Update(setString, afterWhereString);
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
            if (!(string.IsNullOrWhiteSpace(UpdateSizeTextBox.Text.Trim()) && string.IsNullOrWhiteSpace(UpdatePriceTextBox.Text.Trim())))
            {
                return true;
            }
            return false;
        }

    }
}
