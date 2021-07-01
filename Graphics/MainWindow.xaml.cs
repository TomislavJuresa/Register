using Models.EntityFramework;
using Store.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Account CurrentUser;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                string username = UsernameInput.Text;
                string password = PasswordInput.Text;

                var account = context.Accounts.Where(acc => (acc.Username == username) && (acc.Password == password)).SingleOrDefault();
                if (account == null)
                {
                    MessageBox.Show("username or password false");
                }
                else
                {
                    CurrentUser = account;
                    MessageBox.Show($"welcome {account.DisplayUsername}");
                    var adminWindow = new AdminMenu();
                    adminWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
