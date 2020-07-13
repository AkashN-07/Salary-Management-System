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
    /// Interaction logic for Attendance_Calculator.xaml
    /// </summary>
    public partial class Attendance_Calculator : Window
    {
        public Attendance_Calculator()
        {
            InitializeComponent();
        }

        private void txtAttended_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtAttended.Clear();
            txtEmpID.Clear();
            txtLeavesA.Clear();
            txtLeavesT.Clear();
            txtTotal.Clear();
        }

        private float Attendance_Calc()
        {
            float Attendance;
            Attendance = (float.Parse(txtAttended.Text) / ((float.Parse(txtTotal.Text)) - (float.Parse(txtLeavesA.Text) - float.Parse(txtLeavesT.Text)) - float.Parse(txtLeavesA.Text)))*100;
            return Attendance;           
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[AttendanceDetails] where EmpID= @EmpID", con);
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

                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[AttendanceDetails]([EmpID],[NoAttended],[NoLeaves],[NoLeavesAllowed],[TotalDays]) VALUES('" + txtEmpID.Text + "','" + txtAttended.Text + "','" + txtLeavesT.Text + "','" + txtLeavesA.Text + "','" + txtTotal.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("CalAttendance", con);
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlDataAdapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlDataAdapter.SelectCommand.Parameters.Add("@Attendance", System.Data.SqlDbType.Int).Value = Attendance_Calc();
                    sqlDataAdapter.SelectCommand.Parameters.Add("@EmpID", System.Data.SqlDbType.Int).Value = int.Parse(txtEmpID.Text);
                    sqlDataAdapter.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show(string.Format("Employee {0} Inserted Successfully ", this.txtEmpID.Text));
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            txtAttended.Clear();
            txtEmpID.Clear();
            txtLeavesA.Clear();
            txtLeavesT.Clear();
            txtTotal.Clear();
        }
    }
}
