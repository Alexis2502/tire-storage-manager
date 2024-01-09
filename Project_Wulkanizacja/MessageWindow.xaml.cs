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
    /// Logika interakcji dla klasy MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {

        String Message;

        public MessageWindow(string message)
        {
            Message = message;
            InitializeComponent();
            MessageDisplayLabel.Content = Message;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
