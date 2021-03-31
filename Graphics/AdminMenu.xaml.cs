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
        }

        private void ProductManagementButton_Click(object sender, RoutedEventArgs e)
        {
            EditProducts editProducts = new EditProducts();
            editProducts.Show();
        }

        private void UsersSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            EditUsers editUsers = new EditUsers();
            editUsers.Show();
        }
    }
}
