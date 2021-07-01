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
    /// Interaction logic for EditProducts.xaml
    /// </summary>
    public partial class EditProducts : Window
    {





        public EditProducts()
        {
            InitializeComponent();
            try
            {
                using (var context = new StoreContext() { })
                {
                    var products = context.Products.Include("Vendor").ToList();
                    EditProductsDataGrid.ItemsSource = products;
                    VendorsListBox.ItemsSource = context.Vendors.Select(v=>v.CompanyName).ToList();
                    usernameDisplayLabel.Content = "Welcome " + MainWindow.CurrentUser.DisplayUsername + "!";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
            

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                try
                {
                    if (EditProductsDataGrid.Items.Count > 0 && (EditProductsDataGrid.SelectedItem) != null)
                    {
                        //ProductIDTextBox.Text = ((DataRowView)EditProductsDataGrid.SelectedItem).Row["ProductID"].ToString();
                        var selectedProduct= (Product)EditProductsDataGrid.SelectedItem;
                        NameTextBox.Text = selectedProduct.Name;
                        PriceTextBox.Text = selectedProduct.Price.ToString();
                        //Todo Listbox
                        //context.Products.Find(selectedProduct).Vendor.Id;
                        var originalProduct = context.Products.Include("Vendor").Where(p => p.Id == selectedProduct.Id).FirstOrDefault();
                        var vendor = originalProduct.Vendor;
                        VendorsListBox.SelectedItem = vendor.CompanyName;
                            

                        //var product = (Product)EditProductsDataGrid.SelectedItem.;
                        //NameTextBox.Text = product.Name;
                        //PriceTextBox.Text = product.Price.ToString();
                        //VendorIdTextBox.Text = product.Vendor.Id.ToString();


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}There was a problem with gettin Row index");
                }
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                var name = NameTextBox.Text;
                string vendorCompanyName = VendorsListBox.SelectedValue.ToString();
                var vendor = context.Vendors.Where(v => v.CompanyName == vendorCompanyName).SingleOrDefault();
                //VendorID = ((Vendor)VendorsListBox.SelectedItem).Id;
                var price = Double.Parse(PriceTextBox.Text);
                var createdOn = DateTime.Now;
                try
                {
                   

                        var product = new Product
                        {
                            Name = name,
                            Price = price,
                            Vendor = vendor,
                            CreatedOn=createdOn
                            
                        };
                        context.Products.Add(product);
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
                string vendorCompanyName = VendorsListBox.SelectedValue.ToString();
                var vendor = context.Vendors.Where(v => v.CompanyName == vendorCompanyName).SingleOrDefault();
                var price = Double.Parse(PriceTextBox.Text);
                var ProductID = Int32.Parse(((Product)(EditProductsDataGrid.SelectedItem)).Id.ToString());
                try
                {

                    var product = context.Products.Find(ProductID);
                    if (product == null)
                    {
                        MessageBox.Show("Product with given ProductID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    product.Price = price;
                    product.Name = name;
                    product.Vendor = vendor;
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
        private void UpdateDataGrid() {
            using (var context = new StoreContext() { })
            {
                EditProductsDataGrid.ItemsSource = context.Products.Include("Vendor").ToList();
            }
        }
        private void ClearTextBox()
        {
            this.PriceTextBox.Text = "";
            this.NameTextBox.Text = "";
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StoreContext() { })
            {
                var ProductID = Int32.Parse(((Product)(EditProductsDataGrid.SelectedItem)).Id.ToString());
                
                try
                {

                    var product = context.Products.Find(ProductID);
                    if (product == null)
                    {
                        MessageBox.Show("Product with given ProductID does not exists");
                        UpdateDataGrid();
                        ClearTextBox();
                        return;
                    }
                    context.Products.Remove(product);
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
            VendorsListBox.ItemsSource = null;
        }
    }
}
