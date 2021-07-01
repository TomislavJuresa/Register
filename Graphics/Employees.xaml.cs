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
using System.Windows.Shapes;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Window
    {
        public Employees()
        {
            InitializeComponent();
            try
            {
                using (var context = new StoreContext() { })
                {
                    var employees = context.Employees.ToList();
                    EmployeesDataGrid.ItemsSource = employees;
                    ReportsToEmployeeComboBox.ItemsSource = context.Employees.Select(e => e.Id).ToList();
                    usernameDisplayLabel.Content = "Welcome " + MainWindow.CurrentUser.DisplayUsername + "!";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {

                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                var adress = AdressTextBox.Text;
                var city = CityTextBox.Text;
                var postalCode =Int32.Parse(PostalCodeTextBox.Text);
                var country = CountryTextBox.Text;
                var phone = PhoneTextBox.Text;
                var title = TitleTextBox.Text;
                var notes = NotesTextBox.Text;
                //var reportsToEmployee= context.Employees.Where(v => v.Id == (Int32.Parse(ReportsToEmployeeComboBox.SelectedValue.ToString()))).SingleOrDefault();
                var id = ReportsToEmployeeComboBox.SelectedValue.ToString();


                try
                {
                    var employee = new Employee
                    {
                        Name = NameTextBox.Text,
                        Surname = SurnameTextBox.Text,
                        Adress = AdressTextBox.Text,
                        City = CityTextBox.Text,
                        PostalCode = Int32.Parse(PostalCodeTextBox.Text),
                        Country = CountryTextBox.Text,
                        Phone = PhoneTextBox.Text,
                        Title = TitleTextBox.Text,
                        Notes = NotesTextBox.Text,
                        ReportsToEmployeeID = Int32.Parse(id)
                };
                    context.Employees.Add(employee);
                    context.SaveChanges();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);

                }
            }
            UpdateDataGrid();
            ClearTextBox();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {

                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                var adress = AdressTextBox.Text;
                var city = CityTextBox.Text;
                var postalCode = Int32.Parse(PostalCodeTextBox.Text);
                var country = CountryTextBox.Text;
                var phone = PhoneTextBox.Text;
                var title = TitleTextBox.Text;
                var notes = NotesTextBox.Text;
                var id=Int32.Parse((ReportsToEmployeeComboBox.SelectedValue).ToString());
                var reportsToEmployee = context.Employees.Where(v => v.Id == id).SingleOrDefault();
                var employeeID = Int32.Parse(((Employee)(EmployeesDataGrid.SelectedItem)).Id.ToString());


                try
                {
                    var employee = context.Employees.Find(employeeID);
                    {
                        if (employee == null)
                        {
                            MessageBox.Show("Employee with given ID does not exists");
                            UpdateDataGrid();
                            ClearTextBox();
                            return;
                        }
                        employee.Name = NameTextBox.Text;
                        employee.Surname = SurnameTextBox.Text;
                        employee.Adress = AdressTextBox.Text;
                        employee.City = CityTextBox.Text;
                        employee.PostalCode = Int32.Parse(PostalCodeTextBox.Text);
                        employee.Country = CountryTextBox.Text;
                        employee.Phone = PhoneTextBox.Text;
                        employee.Title = TitleTextBox.Text;
                        employee.Notes = NotesTextBox.Text;
                        employee.ReportsToEmployeeID = reportsToEmployee.Id;

                        context.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);

                }
            }
            UpdateDataGrid();
            ClearTextBox();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                var employeeID = Int32.Parse(((Employee)(EmployeesDataGrid.SelectedItem)).Id.ToString());

                try
                {

                    var employee = context.Employees.Find(employeeID);
                    if (employee == null)
                    {
                        MessageBox.Show("Employee with given ID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    context.Employees.Remove(employee);
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

        private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (EmployeesDataGrid.Items.Count > 0 && (EmployeesDataGrid.SelectedItem) != null)
                {
                    var employee = (Employee)EmployeesDataGrid.SelectedItem;
                    NameTextBox.Text = employee.Name;
                    SurnameTextBox.Text = employee.Surname;
                    AdressTextBox.Text = employee.Adress;
                    CityTextBox.Text = employee.City;
                    PostalCodeTextBox.Text = employee.PostalCode.ToString();
                    CountryTextBox.Text = employee.Country;
                    PhoneTextBox.Text = employee.Phone;
                    TitleTextBox.Text = employee.Title;
                    NotesTextBox.Text = employee.Notes;
                    ReportsToEmployeeComboBox.SelectedItem = employee.ReportsToEmployeeID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}There was a problem with gettin Row index");
            }
        }
        private void UpdateDataGrid()
        {
            using (var context = new StoreContext() { })
            {
                EmployeesDataGrid.ItemsSource = context.Employees.ToList();
            }
        }
        private void ClearTextBox()
        {
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            AdressTextBox.Text = "";
            CityTextBox.Text = "";
            PostalCodeTextBox.Text = "";
            CountryTextBox.Text = "";
            PhoneTextBox.Text = "";
            TitleTextBox.Text = "";
            NotesTextBox.Text = "";


        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
            ReportsToEmployeeComboBox.SelectedItem = null;
        }
    }
}
