using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class EditUsers : Window
    {

        readonly SqlConnection SqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private int ProductID;
        private String Name;
        private String Surname;
        private String DisplayUsername;
        private String Username;
        private String Passwort;

        int VendorID;
        public EditUsers()
        {
            InitializeComponent();
            this.RoleComboBox.Items.Add("admin");
            this.RoleComboBox.Items.Add("deparment leader");
            this.RoleComboBox.Items.Add("worker");
            try
            {
                SqlConnection = new SqlConnection(@"Data Source=DESKTOP-Q6NTATU; Initial Catalog=loginData; Integrated Security=True;");
                SqlConnection.Open();
                if (SqlConnection.State == System.Data.ConnectionState.Closed || !(SqlConnection.State == System.Data.ConnectionState.Open))
                {
                    SqlConnection.Open();
                    MessageBox.Show("Cannot connect");
                }
                sqlDataAdapter = new SqlDataAdapter("SELECT * FROM login JOIN users ON login.userId=users.id", SqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                editUsersDataGrid.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                //sqlCon.Close();
            }
        }
        private void updateDataGrid()
        {
            sqlDataAdapter = new SqlDataAdapter("SELECT * FROM login JOIN users ON login.userId=users.id", SqlConnection);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            editUsersDataGrid.ItemsSource = dataTable.DefaultView;
        }
        private void clearTextBox()
        {
            this.NameTextBox.Text = "";
            this.PasswortTextBox.Text = "";
            this.SurnameTextBox.Text = "";
            this.UsernameTextBox.Text = "";
            this.DisplayUsernametextBox.Text = "";
        }
        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            Name = NameTextBox.Text;
            Surname = SurnameTextBox.Text;
            DisplayUsername = DisplayUsernametextBox.Text;
            Username = UsernameTextBox.Text;
            Passwort = PasswortTextBox.Text;
            //To Do: Transaktion with 2 inserts
            String query = "SELECT MAX(id) FROM USERS";
            SqlCommand sqlCmd = new SqlCommand(query, SqlConnection);
            sqlCmd.Parameters.AddWithValue("@ProductID", ProductID);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            int newID = Int32.Parse(sqlCmd.ExecuteScalar().ToString());
            if (newID == 0)
            {
                newID = 1;
            }

            query = "INSERT INTO Users (id,name,surname,displayUsername) Values(@newID,@Name,@Surname,@Username)";
            sqlCmd = new SqlCommand(query, SqlConnection);
            sqlCmd.CommandType = System.Data.CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@newID", newID);
            sqlCmd.Parameters.AddWithValue("@Name", Name);
            sqlCmd.Parameters.AddWithValue("@Surname", Surname);
            sqlCmd.Parameters.AddWithValue("@Username", Username);
            int result = sqlCmd.ExecuteNonQuery();
            if (result == null)
            {
                MessageBox.Show("There was a problem with insert");
            }
            updateDataGrid();
            clearTextBox();
        }
    }
}
