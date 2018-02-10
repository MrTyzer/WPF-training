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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;


namespace Dz_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection _connectionEmp;
        private SqlConnection _connectionDep;
        public SqlDataAdapter AdapterEmp;
        public SqlDataAdapter AdapterDep;
        public DataTable EmployeeTable;
        public DataTable DepartmentTable;
        public DataRowView NewRow;

        public MainWindow()
        {
            InitializeComponent();
            EmployeeInit();
            DepartmentInit();
        }
        
        private void DepartmentInit()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Dz_7;Integrated Security=True";
            _connectionDep = new SqlConnection(connectionString);
            AdapterDep = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(@"SELECT ID, Name FROM
                                                Department", _connectionDep);
            AdapterDep.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Department (Name)
                                     VALUES (@Name); SET @ID = @@IDENTITY;", _connectionDep);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            AdapterDep.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Department SET Name = @Name, 
                                     WHERE ID = @ID", _connectionDep);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterDep.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Department WHERE ID = @ID", _connectionDep);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterDep.DeleteCommand = command;
            DepartmentTable = new DataTable();
            AdapterDep.Fill(DepartmentTable);
            dgDepartment.DataContext = DepartmentTable.DefaultView;
        }

        private void EmployeeInit()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Dz_7;Integrated Security=True";
            _connectionEmp = new SqlConnection(connectionString);
            AdapterEmp = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(@"SELECT ID, Name, Age, Salary, Department FROM
                                                Employee", _connectionEmp);
            AdapterEmp.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Employee (Name, Age, Salary, Department)
                                     VALUES (@Name, @Age, @Salary, @Department); SET @ID = @@IDENTITY;", _connectionEmp);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            AdapterEmp.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Employee SET Name = @Name, Age = @Age,
                                     Salary = @Salary, Department = @Department WHERE ID = @ID", _connectionEmp);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.NVarChar, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.NVarChar, -1, "Salary");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, -1, "Department");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterEmp.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Employee WHERE ID = @ID", _connectionEmp);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            AdapterEmp.DeleteCommand = command;
            EmployeeTable = new DataTable();
            AdapterEmp.Fill(EmployeeTable);
            dgEmployee.DataContext = EmployeeTable.DefaultView;
        }

        
        private void dgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsEnabled = false;
            NewRow = (DataRowView)dgEmployee.SelectedItem;
            NewRow.BeginEdit();
            Editor childWindow = new Editor(NewRow.Row, this);
            childWindow.Closing += Enable;
            childWindow.Show();
            childWindow.Activate();
        }

        private void dgDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsEnabled = false;
            NewRow = (DataRowView)dgDepartment.SelectedItem;
            NewRow.BeginEdit();
            Editor2 childWindow = new Editor2(NewRow.Row, this);
            childWindow.Closing += Enable;
            childWindow.Show();
            childWindow.Activate();
        }

        public void Enable(object sender, EventArgs e)
        {
            IsEnabled = true;
        }
    }
}
