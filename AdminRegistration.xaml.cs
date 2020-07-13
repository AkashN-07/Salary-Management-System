using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace DBMS_MiniProj_V1
{
    /// <summary>
    /// Interaction logic for AdminRegistration.xaml
    /// </summary>
    public partial class AdminRegistration : Window
    {
        public AdminRegistration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Please Enter User Name");
            }
            else if (string.IsNullOrEmpty(pswPass.Password))
            {
                MessageBox.Show("Password Required");
            }
            else if (pswPass.Password != pswRePass.Password)
            {
                MessageBox.Show("Password Doesn't Match !!");
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
                SqlCommand cmd = new SqlCommand("Select * from [AdminLogin] where Username= @Username", con);
                cmd.Parameters.AddWithValue("@Username", this.txtUser.Text);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(string.Format("Username {0} already exist", this.txtUser.Text));
                }
                else
                {
                    try
                    {
                        SqlCommand cmd1 = con.CreateCommand();
                        cmd1.CommandType = System.Data.CommandType.Text;
                        cmd1.CommandText = "insert into [AdminLogin] values ('" + txtUser.Text + "','" + pswPass.Password + "')";
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record inserted successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                con.Close();
            }
        }
    }
}
