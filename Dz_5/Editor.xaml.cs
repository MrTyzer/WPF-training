using System;
using System.Collections.Generic;
using System.Data;
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

namespace Dz_5
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {        public DataRow resultRow { get; set; }        public Editor(DataRow row)
        {
            InitializeComponent();
            lId.Content = row["Id"].ToString();
            tbName.Text = row["Name"].ToString();
            tbAge.Text = row["Age"].ToString();
            tbSalary.Text = row["Salary"].ToString();
            tbDepartment.Text = row["Department"].ToString();
            resultRow = row;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Id"] = lId.Content;
            resultRow["Name"] = tbName.Text;
            resultRow["Age"] = tbAge.Text;
            resultRow["Salary"] = tbSalary.Text;
            resultRow["Department"] = tbDepartment.Text;
            MainWindow Mwin = Owner as MainWindow;
            Mwin.NewRow.EndEdit();
            Mwin.AdapterEmp.Update(Mwin.EmployeeTable);
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
