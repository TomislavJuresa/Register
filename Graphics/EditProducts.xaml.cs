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
        readonly SqlConnection sqlCon ;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private int ProductID;
        private String Name;
        private Double Price;
        int VendorID;

        public EditProducts()
        {
            InitializeComponent();
            try
            {
                using (var context = new StoreContext() { })
                {
                    var products = context.Products.ToList();
                    EditProductsDataGrid.ItemsSource = products;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
            

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (EditProductsDataGrid.Items.Count > 0 && (EditProductsDataGrid.SelectedItem)!=null)
                {
                    //ProductIDTextBox.Text = ((DataRowView)EditProductsDataGrid.SelectedItem).Row["ProductID"].ToString();
                    NameTextBox.Text = ((Product)EditProductsDataGrid.SelectedItem).Name;
                    PriceTextBox.Text = ((Product)EditProductsDataGrid.SelectedItem).Price.ToString();
                    //Todo Listbox
                    var vendor = ((Product)EditProductsDataGrid.SelectedItem).Vendor;
                    if (vendor == null)
                    {
                        VendorIdTextBox.Text = "null";
                        return;
                    }
                    var id = vendor.Id;
                    if (id == 0)
                    {
                        VendorIdTextBox.Text = "null";
                        return;
                    }
                    VendorIdTextBox.Text = id.ToString();

                    //var product = (Product)EditProductsDataGrid.SelectedItem.;
                    //NameTextBox.Text = product.Name;
                    //PriceTextBox.Text = product.Price.ToString();
                    //VendorIdTextBox.Text = product.Vendor.Id.ToString();


                }
            }catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}There was a problem with gettin Row index");
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            
            Name = NameTextBox.Text;
            VendorID = Int32.Parse(VendorIdTextBox.Text);
            Price = Double.Parse(PriceTextBox.Text);

            try
            {
                using (var context = new StoreContext() { })
                {
                    
                    var product = new Product
                    {
                        Name = Name,
                        Price = Price,
                        Vendor = context.Vendors.Where(v => v.Id == VendorID).SingleOrDefault()
                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }        

            updateDataGrid();
            clearTextBox();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Name = NameTextBox.Text;
            VendorID = Int32.Parse(VendorIdTextBox.Text);
            Price = Double.Parse(PriceTextBox.Text);
            var ProductID =Int32.Parse( ((DataRowView)EditProductsDataGrid.SelectedItem).Row["Id"].ToString());
            try
            {
                using (var context = new StoreContext() { })
                {
                    var products = context.Products;
                    var product = context.Products.Find(ProductID);
                    if (product == null)
                    {
                        MessageBox.Show("Product with given ProductID does not exists");
                        updateDataGrid();
                        clearTextBox();
                        return;
                    }
                    product.Price = Price;
                    product.Name = Name;
                    product.Vendor = context.Vendors.Where(v => v.Id == VendorID).SingleOrDefault();
                    context.SaveChanges();
                    
                    updateDataGrid();
                    clearTextBox();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }            
        }
        private void updateDataGrid() {
            using (var context = new StoreContext() { })
            {
                EditProductsDataGrid.ItemsSource = context.Products.ToList();
            }
        }
        private void clearTextBox()
        {
            this.PriceTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.VendorIdTextBox.Text = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Name = NameTextBox.Text;
            VendorID = Int32.Parse(VendorIdTextBox.Text);
            Price = Double.Parse(PriceTextBox.Text);
            var ProductID = Int32.Parse(((DataRowView)EditProductsDataGrid.SelectedItem).Row["Id"].ToString());
            try
            {
                using (var context = new StoreContext() { })
                {
                    var products = context.Products;
                    var product = context.Products.Find(ProductID);
                    if (product == null)
                    {
                        MessageBox.Show("Product with given ProductID does not exists");
                        updateDataGrid();
                        clearTextBox();
                        return;
                    }
                    context.Products.Remove(product);
                    context.SaveChanges();

                    updateDataGrid();
                    clearTextBox();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
        }
    }
}
