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
    /// <summary>
    /// Interaction logic for DepartmentDetails.xaml
    /// </summary>
    public partial class DepartmentDetails : Window
    {
        public DepartmentDetails()
        {
            InitializeComponent();
        }

        private void ClearData()
        {
            txtDeptID.Clear();
            txtDeptName.Clear();
            txtDeptMgr.Clear();
        }
        private void ClearData1()
        {
            txtEmpID.Clear();
            txtEmpDeptID.Clear();
        }


        private void displayall()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [DeptID],[DeptName],[DeptMgrID] FROM [dbo].[DeptDetails]";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                grid1.ItemsSource = dt.DefaultView;
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

        private void displayall1()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [EmpID],[DeptID] FROM [dbo].[EmployeeWorkDetails]";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                grid2.ItemsSource = dt.DefaultView;
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

        //private void Triggerdelete()
        //{
            //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            //con.Open();
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = System.Data.CommandType.Text;         
            //cmd.CommandText = "CREATE TRIGGER trg_delete11 ON [dbo].[DeptDetails] AFTER DELETE AS BEGIN SET NOCOUNT ON; DELETE FROM [dbo].[EmployeeWorkDetails] WHERE [DeptID] = '" + txtDeptID.Text+ "'END ";
            //cmd.CommandText = "DROP TRIGGER [dbo].[trg_delete11] ";
            //cmd.ExecuteNonQuery();
        //}
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[DeptDetails] where DeptID= @DeptID", con);
                cmd1.Parameters.AddWithValue("@DeptID", this.txtDeptID.Text);
                var result = cmd1.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(string.Format("Department {0} already exist", this.txtDeptID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO [dbo].[DeptDetails]([DeptID],[DeptName],[DeptMgrID]) VALUES('" + txtDeptID.Text + "','" + txtDeptName.Text + "','" + txtDeptMgr.Text + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(string.Format("Department {0} Inserted Successfully ", this.txtDeptID.Text));
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
            try
            {
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[DeptDetails] where DeptID= @DeptID", con);
                cmd1.Parameters.AddWithValue("@DeptID", this.txtDeptID.Text);
                var result = cmd1.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show(string.Format("Department {0} doesn't exist", this.txtDeptID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "UPDATE [dbo].[DeptDetails] SET [DeptID] = '" + txtDeptID.Text + "',[DeptName] = '" + txtDeptName.Text + "',[DeptMgrID] = '" + txtDeptMgr.Text + "' WHERE [DeptID] ='" + int.Parse(txtDeptID.Text) + "'";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(string.Format("Department {0} Updated Successfully ", this.txtDeptID.Text));
                        cmd.CommandText = "";
                        ClearData();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please Fill all the fields !!");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                displayall();
                con.Close();
            }
            catch(Exception d)
            {
                MessageBox.Show(d.Message);
            }
            con.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[DeptDetails] where DeptID= @DeptID", con);
            cmd1.Parameters.AddWithValue("@DeptID", this.txtDeptID.Text);
            var result = cmd1.ExecuteScalar();
            try
            {

                if (result == null)
                {
                    MessageBox.Show(string.Format("Department {0} doesn't exist", this.txtDeptID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    //try
                    //{
                    //    Triggerdelete();
                    //}
                    //catch (Exception D)
                    //{
                    //    MessageBox.Show(D.Message);
                    //}
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [dbo].[DeptDetails] WHERE [DeptID] ='" + int.Parse(txtDeptID.Text) + "'";
                    cmd.ExecuteNonQuery();
                    ClearData();
                    displayall();
                    con.Close();
                    MessageBox.Show("Department deleted successfully");                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            displayall();
            con.Close();
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[EmployeeWorkDetails] where EmpID= @EmpID", con);
            cmd1.Parameters.AddWithValue("@EmpID", this.txtEmpID.Text);
            var result = cmd1.ExecuteScalar();
            if (result == null)          
            {
                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO [dbo].[EmployeeWorkDetails]([EmpID],[DeptID]) VALUES('" + txtEmpID.Text + "','" + txtEmpDeptID.Text + "')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Department for Employee {0} Updated Successfully ", this.txtEmpID.Text));
                    ClearData1();
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
            else
            {
                try
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE [dbo].[EmployeeWorkDetails] SET [DeptID] = '" + txtEmpDeptID.Text + "',[EmpID] = '" + txtEmpID.Text + "' WHERE [EmpID] ='" + int.Parse(txtEmpID.Text) + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Department for Employee {0} Updated Successfully ", this.txtEmpID.Text));
                    ClearData1();
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
            displayall1();
            con.Close();
        }

        private void grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            displayall();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData1();
        }

        private void Mouse_Dpuble_Display_Click(object sender, MouseButtonEventArgs e)
        {
            displayall();
        }

        private void Mouse_Double_Display1_Click(object sender, MouseButtonEventArgs e)
        {
            displayall1();
        }
    }
}

    

