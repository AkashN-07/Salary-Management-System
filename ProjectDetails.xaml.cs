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
    /// Interaction logic for ProjectDetails.xaml
    /// </summary>
    public partial class ProjectDetails : Window
    {
        public ProjectDetails()
        {
            InitializeComponent();
        }

        private void ClearData()
        {
            txtProjID.Clear();
            txtProjName.Clear();
            txtProjHead.Clear();
            txtProjDuration.Clear();
        }
        private void ClearData1()
        {
            txtEmpID.Clear();
            txtEmpProjID.Clear();
        }


        private void displayall()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [ProjID],[ProjName],[ProjHeadID],[ProjDuration] FROM [dbo].[ProjectDetails]";
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
                cmd.CommandText = "SELECT [EmpID],[ProjID] FROM [dbo].[EmployeeWorkDetails]";
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

        //private void Triggerdelete1()
        //{
        //    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
        //    con.Open();
        //    SqlCommand cmd = con.CreateCommand();
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandText = "CREATE TRIGGER trg_delete_dl ON [dbo].[ProjectDetails] FOR DELETE AS DELETE FROM [dbo].[EmployeeWorkDetails] WHERE [ProjID] ='" + txtProjID.Text + "'";
        //    cmd.ExecuteNonQuery();
        //}

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[ProjectDetails] where ProjID= @ProjID", con);
                cmd1.Parameters.AddWithValue("@ProjID", this.txtProjID.Text);
                var result = cmd1.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(string.Format("Project {0} already exist", this.txtProjID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    try
                    {
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO [dbo].[ProjectDetails]([ProjID],[ProjName],[ProjHeadID],[ProjDuration]) VALUES('" + txtProjID.Text + "','" + txtProjName.Text + "','" + txtProjHead.Text + "','"+txtProjDuration.Text+"')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(string.Format("Project {0} Inserted Successfully ", this.txtProjID.Text));
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
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[ProjectDetails] where ProjID= @ProjID", con);
                cmd1.Parameters.AddWithValue("@ProjID", this.txtProjID.Text);
                var result = cmd1.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show(string.Format("Project {0} doesn't exist", this.txtProjID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {                
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "UPDATE [dbo].[ProjectDetails] SET [ProjID] = '" + txtProjID.Text + "',[ProjName] = '" + txtProjName.Text + "',[ProjHeadID] = '" + txtProjHead.Text + "',[ProjDuration] = '" + txtProjDuration.Text + "' WHERE [ProjID] ='" + int.Parse(txtProjID.Text) + "'";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(string.Format("Project {0} Updated Successfully ", this.txtProjID.Text));
                        ClearData();
                        con.Close();                                        
                }
                displayall();
                con.Close();
            }
            catch (Exception d)
            {
                MessageBox.Show(d.Message);
            }
            con.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[ProjectDetails] where ProjID= @DeptID", con);
            cmd1.Parameters.AddWithValue("@DeptID", this.txtProjID.Text);
            var result = cmd1.ExecuteScalar();
            try
            {
                if (result == null)
                {
                    MessageBox.Show(string.Format("Project {0} doesn't exist", this.txtProjID.Text));
                    ClearData();
                    con.Close();
                }
                else
                {
                    //try
                    //{
                    //    Triggerdelete1();
                    //}
                    //catch (Exception D)
                    //{
                    //    MessageBox.Show(D.Message);
                    //}
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM [dbo].[ProjectDetails] WHERE [ProjID] ='" + txtProjID.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Project deleted successfully");                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please assign Employees with different project then delete the project");
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
                    cmd.CommandText = "INSERT INTO [dbo].[EmployeeWorkDetails]([EmpID],[ProjID]) VALUES('" + txtEmpID.Text + "','" + txtEmpProjID.Text + "')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Project for Employee {0} Assigned Successfully ", this.txtEmpID.Text));
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
                    cmd.CommandText = "UPDATE [dbo].[EmployeeWorkDetails] SET [ProjID] = '" + txtEmpProjID.Text + "',[EmpID] = '" + txtEmpID.Text + "' WHERE [EmpID] ='" + int.Parse(txtEmpID.Text) + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Project for Employee {0} Updated Successfully ", this.txtEmpID.Text));
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

        private void Mouse_Dpuble_Display_Click(object sender, MouseButtonEventArgs e)
        {
            displayall();
        }

        private void Mouse_Double_Display1_Click(object sender, MouseButtonEventArgs e)
        {
            displayall1();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData1();
        }
    }

    
   
}
