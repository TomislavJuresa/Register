using Models.EntityFramework;
using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    /// Interaction logic for EditUsers.xaml
    /// </summary>
    public partial class EditAccounts : Window
    {



        public EditAccounts()
        {
            InitializeComponent();

            try
            {
                using (var context = new StoreContext() { })
                {
                    var accounts = context.Accounts.Include("Employee").ToList();
                    editUsersDataGrid.ItemsSource = accounts;
                    EmployeeComboBox.ItemsSource = context.Employees.Select(e => e.Name).ToList();
                    RoleComboBox.ItemsSource = Enum.GetValues(typeof(Role));
                    usernameDisplayLabel.Content = "Welcome " + MainWindow.CurrentUser.DisplayUsername + "!";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
        private void UpdateDataGrid()
        {
            using (var context = new StoreContext() { })
            {
                editUsersDataGrid.ItemsSource = context.Accounts.Include("Employee").ToList();
            }
        }
        private void ClearTextBox()
        {
            this.PasswortTextBox.Text = "";
            this.UsernameTextBox.Text = "";
            this.DisplayUsernametextBox.Text = "";
        }
        private void InsertButton_Click(object sender, RoutedEventArgs e)//TODO: check pre requirements
        {
            var displayUsername = DisplayUsernametextBox.Text;
            var username = UsernameTextBox.Text;
            var password = PasswortTextBox.Text;
            var role = (Role)Enum.Parse(typeof(Role), RoleComboBox.SelectedItem.ToString());
            var createdOn = DateTime.Now;
            //To Do: Transaktion with 2 inserts
            using (var context = new StoreContext() { })
            {

                //var vendor = context.Vendors.Where(v => v.CompanyName == vendorCompanyName).SingleOrDefault();
                //VendorID = ((Vendor)VendorsListBox.SelectedItem).Id;
                try
                {
                    var emloyee = context.Employees.Where(ee => ee.Name == (EmployeeComboBox.SelectedItem)).FirstOrDefault();

                    var account = new Account
                    {
                        DisplayUsername = displayUsername,
                        Username = username,
                        Password = password,
                        Role = role,
                        Employee = emloyee,
                        CreatedOn=createdOn
                    };
                    context.Accounts.Add(account);
                    context.SaveChanges();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

            UpdateDataGrid();
            ClearTextBox();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
            EmployeeComboBox.SelectedItem = null;
            RoleComboBox.SelectedItem = null;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                var displayUsername = DisplayUsernametextBox.Text;
                var username = UsernameTextBox.Text;
                var password = PasswortTextBox.Text;
                var role = (Role)Enum.Parse(typeof(Role), RoleComboBox.SelectedItem.ToString());
                var emloyee = context.Employees.Where(ee => ee.Name == (EmployeeComboBox.SelectedItem)).FirstOrDefault();
                try
                {

                    var selectedAccount = (Account)editUsersDataGrid.SelectedItem;
                    var account = context.Accounts.Find(selectedAccount.Id);
                    if (account == null)
                    {
                        MessageBox.Show("Account with given ID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    account.DisplayUsername = displayUsername;
                    account.Username = username;
                    account.Password = password;
                    account.Role = role;
                    account.Employee = emloyee;
                    context.SaveChanges();
                    UpdateDataGrid();
                    ClearTextBox();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);

                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                var selectedAccount = (Account)editUsersDataGrid.SelectedItem;

                try
                {

                    var account = context.Accounts.Find(selectedAccount.Id);
                    if (account == null)
                    {
                        MessageBox.Show("Account with given ID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    context.Accounts.Remove(account);
                    context.SaveChanges();

                    UpdateDataGrid();
                    ClearTextBox();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);

                }
            }
        }

        private void EditUsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                try
                {
                    if (editUsersDataGrid.Items.Count > 0 && (editUsersDataGrid.SelectedItem) != null)
                    {
                        var selectedAccount = (Account)editUsersDataGrid.SelectedItem;
                        var displayUsername = selectedAccount.DisplayUsername;
                        var username = selectedAccount.Username;
                        var password = selectedAccount.Password;
                        var role = selectedAccount.Role;
                        var employee = selectedAccount.Employee;
                        DisplayUsernametextBox.Text = displayUsername;
                        UsernameTextBox.Text = username;
                        PasswortTextBox.Text = password;
                        RoleComboBox.SelectedItem = role;
                        EmployeeComboBox.SelectedItem = employee.Name;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}There was a problem with gettin Row index");
                }
            }
        }
    }
}
    

