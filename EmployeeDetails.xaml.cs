using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class EmployeeDetails : Window
    {
        public EmployeeDetails()
        {
            InitializeComponent();
        }

        private void ClearData()
        {
            txtEmpID.Clear();
            txtDept.Clear();
            txtEmail.Clear();
            txtName.Clear();
            txtPhone.Clear();
            cmb_dob.SelectedDate = DateTime.Now.Date;
        }

        public void displayall()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [EmpID],[EmpName],[BloodGroup],[Phone],[E-mail],[DOB] FROM [dbo].[EmployeePersonalDetails]";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                grid.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            con.Close();
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[EmployeePersonalDetails] where EmpID= @EmpID", con);
                cmd1.Parameters.AddWithValue("@EmpID", this.txtEmpID.Text);
                var result = cmd1.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(string.Format("Employee {0} already exist", this.txtEmpID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO [dbo].[EmployeePersonalDetails]([EmpID],[EmpName],[BloodGroup],[Phone],[E-mail],[DOB]) VALUES('" + txtEmpID.Text + "','" + txtName.Text + "','" + txtDept.Text + "','" + txtPhone.Text + "','" + txtEmail.Text + "','" + cmb_dob.SelectedDate + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(string.Format("Employee {0} Inserted Successfully ", this.txtEmpID.Text));
                        ClearData();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
            finally
            {
                con.Close();
            }
            displayall();
            con.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[EmployeePersonalDetails] where EmpID= @EmpID", con);
            cmd1.Parameters.AddWithValue("@EmpID", this.txtEmpID.Text);
            var result = cmd1.ExecuteScalar();
            if (result == null)
            {
                MessageBox.Show(string.Format("Employee {0} doesn't exist", this.txtEmpID.Text));
                ClearData();
                con.Close();
            }
            else
            {
                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE[dbo].[EmployeePersonalDetails] SET [EmpID] = '" + txtEmpID.Text + "',[EmpName] = '" + txtName.Text + "',[BloodGroup] = '" + txtDept.Text + "',[Phone] = '" + txtPhone.Text + "',[E-mail] = '" + txtEmail.Text + "',[DOB] = '" + cmb_dob.SelectedDate + "' WHERE [EmpID] ='" + int.Parse(txtEmpID.Text) + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Employee {0} Updated Successfully ", this.txtEmpID.Text));
                    ClearData();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            displayall();
            con.Close();
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            displayall();
        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[EmployeePersonalDetails] where EmpID= @EmpID", con);
            cmd1.Parameters.AddWithValue("@EmpID", this.txtEmpID.Text);
            var result = cmd1.ExecuteScalar();
            try
            {

                if (result == null)
                {
                    MessageBox.Show(string.Format("Employee {0} doesn't exist", this.txtEmpID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [dbo].[EmployeePersonalDetails] WHERE [EmpID] ='" + txtEmpID.Text + "'";
                    cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record deleted successfully");
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            displayall();
            con.Close();
        }

        private void Double_Click(object sender, MouseButtonEventArgs e)
        {
            displayall();
        }
    }
}
