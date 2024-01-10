﻿using System;
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
    /// Logika interakcji dla klasy ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        DBConnect dBConnect = new DBConnect();
        StorageEntry StorageEntry;

        public ConfirmDeleteWindow(StorageEntry storageEntry)
        {
            InitializeComponent();
            StorageEntry = storageEntry;
        }

        private void ConfirmDeleteClick(object sender, RoutedEventArgs e)
        {
            String toPutAfterWhere = "id=" + StorageEntry.id.ToString();
            dBConnect.Delete(toPutAfterWhere);
            this.Close();
        }

        private void CancelDeleteClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
