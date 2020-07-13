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
    /// Interaction logic for SalaryDetails.xaml
    /// </summary>
    public partial class SalaryDetails : Window
    {
        public SalaryDetails()
        {
            InitializeComponent();
        }

        private void ClearData()
        {
            txtEmpID.Clear();
            txtbasic.Clear();
            txthra.Clear();
            txtta.Clear();
            txtit.Clear();
            txtattend.Clear();            
        }

        public void displayall()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [EmpID],[BasicSal],[HRA],[TA],[IT],[Attendance],[TotalSal] FROM [dbo].[SalaryDetails]";
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

        private float calTotal()
        {
            float totSal = 0;
            totSal = (float.Parse(txtbasic.Text) + float.Parse(txthra.Text) + float.Parse(txtta.Text) - float.Parse(txtit.Text)) * (float.Parse(txtattend.Text) / 100);
            return totSal;
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[SalaryDetails] where EmpID= @EmpID", con);
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
                        cmd.CommandText = "INSERT INTO [dbo].[SalaryDetails]([EmpID],[BasicSal],[HRA],[TA],[IT],[Attendance]) VALUES ('" + txtEmpID.Text + "','" + txtbasic.Text + "','" + txthra.Text + "','" + txtta.Text + "','" + txtit.Text + "','" + txtattend.Text + "')";
                        cmd.ExecuteNonQuery();                       
                        MessageBox.Show(string.Format("Employee {0} Inserted Successfully ", this.txtEmpID.Text));
                        displayall();
                        ClearData();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please Register Employee First !!");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[SalaryDetails] where EmpID= @EmpID", con);
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
                    cmd.CommandText = "UPDATE[dbo].[SalaryDetails] SET [EmpID] = '" + txtEmpID.Text + "',[BasicSal] = '" + txtbasic.Text + "',[HRA] = '" + txthra.Text + "',[TA] = '" + txtta.Text + "',[IT] = '" + txtit.Text + "',[Attendance] = '" + txtattend.Text + "' WHERE [EmpID] ='" + txtEmpID.Text + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Employee {0} Updated Successfully ", this.txtEmpID.Text));
                    ClearData();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please fill all the details");
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

        private void btnDisplay_Double_Click(object sender, MouseButtonEventArgs e)
        {
            displayall();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                string selectQuery = "SELECT [dbo].[AttendanceDetails].[Attendance] FROM [dbo].[AttendanceDetails] WHERE [dbo].[AttendanceDetails].[EmpID] = '" + txtEmpID.Text + "'";
                SqlCommand sqlCommand = new SqlCommand(selectQuery, con);
                SqlDataReader read = sqlCommand.ExecuteReader();
                while (read.Read())
                    txtattend.Text = (read["Attendance"].ToString());               
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }

        private void btnAttendance_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            Attendance_Calculator attendance_Calculator = new Attendance_Calculator();
            attendance_Calculator.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
