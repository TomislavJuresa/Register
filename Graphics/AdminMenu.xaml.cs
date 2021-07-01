using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
            usernameDisplayLabel.Content = "Welcome "+MainWindow.CurrentUser.DisplayUsername+"!";
        }

        private void ProductManagementButton_Click(object sender, RoutedEventArgs e)
        {
            EditProducts editProducts = new EditProducts();
            editProducts.Show(); 
        }

        private void UsersSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            EditAccounts editUsers = new EditAccounts();
            editUsers.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Vendors vendors = new Vendors();
            vendors.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
        }
    }
}
