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
    /// Interaction logic for EmployeeReport.xaml
    /// </summary>
    public partial class EmployeeReport : Window
    {
        public EmployeeReport()
        {
            InitializeComponent();
        }

        private void txtAttend_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void ClearData()
        {
            txtAttend.Clear();
            txtBlood.Clear();
            txtDOB.Clear();
            txtEmail.Clear();
            txtEmpID.Clear();
            txtBasic.Clear();
            txthra.Clear();
            txtit.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtta.Clear();
            txtTotSal.Clear();
            txtDeptID.Clear();
            txtDeptMgrID.Clear();
            txtDeptName.Clear();
            txtProjID.Clear();
            txtProjName.Clear();
            txtProjHeadID.Clear();
            txtProjDuration.Clear();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\akash\Documents\DBMS_DB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from [dbo].[EmployeePersonalDetails] where EmpID= @EmpID", con);
            cmd1.Parameters.AddWithValue("@EmpID", this.txtEmpID.Text);
            var result = cmd1.ExecuteScalar();
            if (result == null)
            {
                if (txtEmpID.Text == null)
                {
                    MessageBox.Show("Please Enter Emp-ID !!");
                    con.Close();
                }
                else
                {
                    MessageBox.Show(string.Format("Employee {0} doesn't exist", this.txtEmpID.Text));
                    ClearData();
                    con.Close();
                }
            }
            else
            {
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    string selectQuery = "SELECT dbo.SalaryDetails.BasicSal, dbo.SalaryDetails.HRA, dbo.SalaryDetails.TA, dbo.SalaryDetails.IT, dbo.SalaryDetails.Attendance, dbo.SalaryDetails.TotalSal, dbo.EmployeePersonalDetails.EmpName,  dbo.EmployeePersonalDetails.BloodGroup, dbo.EmployeePersonalDetails.Phone, dbo.EmployeePersonalDetails.[E-Mail],dbo.EmployeePersonalDetails.DOB, dbo.DeptDetails.DeptID, dbo.DeptDetails.DeptMgrID, dbo.DeptDetails.DeptName, dbo.ProjectDetails.ProjID, dbo.ProjectDetails.ProjName,  dbo.ProjectDetails.ProjHeadID, dbo.ProjectDetails.ProjDuration FROM   dbo.EmployeePersonalDetails INNER JOIN dbo.EmployeeWorkDetails ON dbo.EmployeePersonalDetails.EmpID = dbo.EmployeeWorkDetails.EmpID INNER JOIN dbo.DeptDetails ON dbo.EmployeeWorkDetails.DeptID = dbo.DeptDetails.DeptID INNER JOIN dbo.ProjectDetails ON dbo.EmployeeWorkDetails.ProjID = dbo.ProjectDetails.ProjID INNER JOIN  dbo.SalaryDetails ON dbo.EmployeePersonalDetails.EmpID = dbo.SalaryDetails.EmpID WHERE dbo.EmployeePersonalDetails.EmpID = '" + txtEmpID.Text+"'";
                    SqlCommand sqlCommand = new SqlCommand(selectQuery, con);
                    SqlDataReader read = sqlCommand.ExecuteReader();
                    while (read.Read())
                    {
                        txtName.Text = (read["EmpName"].ToString());
                        txtBlood.Text = (read["BloodGroup"].ToString());
                        txtPhone.Text = (read["Phone"].ToString());
                        txtEmail.Text = (read["E-Mail"].ToString());
                        txtDOB.Text = (read["DOB"].ToString());
                        txtBasic.Text = (read["BasicSal"].ToString());
                        txthra.Text = (read["HRA"].ToString());
                        txtta.Text = (read["TA"].ToString());
                        txtit.Text = (read["IT"].ToString());
                        txtAttend.Text = (read["Attendance"].ToString());
                        txtTotSal.Text = (read["TotalSal"].ToString());
                        txtDeptID.Text = (read["DeptID"].ToString());
                        txtDeptName.Text = (read["DeptName"].ToString());
                        txtDeptMgrID.Text = (read["DeptMgrID"].ToString());
                        txtProjID.Text = (read["ProjID"].ToString());
                        txtProjName.Text = (read["ProjName"].ToString());
                        txtProjHeadID.Text = (read["ProjHeadID"].ToString());
                        txtProjDuration.Text = (read["ProjDuration"].ToString());
                    }
                    con.Close();
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }
}
