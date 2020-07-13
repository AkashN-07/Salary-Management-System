using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DBMS_MiniProj_V1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            startClock();
        }

        private void btnDept_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            DepartmentDetails departmentDetails = new DepartmentDetails();
            departmentDetails.Show();
        }

        private void btnEmp1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Show();
                EmployeeDetails employeeDetails = new EmployeeDetails();
                employeeDetails.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSal_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            SalaryDetails salaryDetails = new SalaryDetails();
            salaryDetails.Show();
        }

        private void btnProject_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            ProjectDetails projectDetails = new ProjectDetails();
            projectDetails.Show();
        }

        private void btnAttend_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            EmployeeReport employeeReport = new EmployeeReport();
            employeeReport.Show();
        }

        private void btnEmp_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
            AdminRegistration adminRegistration = new AdminRegistration();
            adminRegistration.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
            Application.Current.Shutdown();
        }

        private void startClock()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += tickevent;
            dispatcherTimer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            datelbl.Text = DateTime.Now.ToString(@"dd/MM/yyyy");
            timelbl.Text = DateTime.Now.ToString(@"hh:mm:ss");
        }

        private void Main_Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Main_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
