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
    /// Interaction logic for Vendors.xaml
    /// </summary>
    public partial class Vendors : Window
    {
        public Vendors()
        {
            InitializeComponent();
            try
            {
                using (var context = new StoreContext() { })
                {
                    var vendors = context.Vendors.ToList();
                    EditVendorsDataGrid.ItemsSource = vendors;
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
                
                var companyName = CompanyNameTextBox.Text;
                var adress = ADressTextBox.Text;
                var city = CityTextBox.Text;
                var postalCode = PostalCodeTextBox.Text;
                var country = CountryTextBox.Text;
                var phone = PhoneTextBox.Text;
                var createdOn = DateTime.Now;                
                try
                {
                    var vendor = new Vendor
                    {
                        CompanyName=companyName,
                        Adress=adress,
                        City=city,
                        PostalCode=Int32.Parse(postalCode),
                        Country=country,
                        Phone=phone,
                        CreatedOn=createdOn
                        
                    };
                    context.Vendors.Add(vendor);
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
                var companyName = CompanyNameTextBox.Text;
                var adress = ADressTextBox.Text;
                var city = CityTextBox.Text;
                var postalCode = PostalCodeTextBox.Text;
                var country = CountryTextBox.Text;
                var phone = PhoneTextBox.Text;
                try
                {

                    var selectedVendor = (Vendor)EditVendorsDataGrid.SelectedItem;
                    var vendor = context.Vendors.Find(selectedVendor.Id);
                    if (vendor == null)
                    {
                        MessageBox.Show("Vendor with given ID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    vendor.CompanyName = companyName;
                    vendor.Adress = adress;
                    vendor.City = city;
                    vendor.PostalCode =Int32.Parse(postalCode);
                    vendor.Country = country;
                    vendor.Phone = phone;
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
        private void UpdateDataGrid()
        {
            using (var context = new StoreContext() { })
            {
                EditVendorsDataGrid.ItemsSource = context.Vendors.ToList();
            }
        }
        private void ClearTextBox()
        {
            CompanyNameTextBox.Text = "";
            ADressTextBox.Text = "";
            CityTextBox.Text = "";
            PostalCodeTextBox.Text = "";
            CountryTextBox.Text = "";
            PhoneTextBox.Text = "";

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                var selectedVendor=(Vendor)EditVendorsDataGrid.SelectedItem;

                try
                {

                    var vendor = context.Vendors.Find(selectedVendor.Id);
                    if (vendor == null)
                    {
                        MessageBox.Show("Product with given ProductID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    context.Vendors.Remove(vendor);
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                try
                {
                    if (EditVendorsDataGrid.Items.Count > 0 && (EditVendorsDataGrid.SelectedItem) != null)
                    {
                        
                        var selectedVendor = (Vendor)EditVendorsDataGrid.SelectedItem;
                        var companyName=selectedVendor.CompanyName;
                        var adress=selectedVendor.Adress;
                        var city=selectedVendor.City;
                        var postalCode=selectedVendor.PostalCode;
                        var country=selectedVendor.Country;
                        var phone=selectedVendor.Phone;

                        CompanyNameTextBox.Text = companyName;
                        ADressTextBox.Text = adress;
                        CityTextBox.Text = city;
                        PostalCodeTextBox.Text = postalCode.ToString();
                        CountryTextBox.Text = country;
                        PhoneTextBox.Text = phone;
                        //Todo Listbox
                        //context.Products.Find(selectedProduct).Vendor.Id;






                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}There was a problem with gettin Row index");
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }
    }
}
