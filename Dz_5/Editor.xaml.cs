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
    {
        public DataRow resultRow { get; set; }
        public MainWindow Mwin { get; set; }

        public Editor(DataRow row, MainWindow mwin)
        {
            InitializeComponent();
            Mwin = mwin;
            lId.Content = row["Id"].ToString();
            tbName.Text = row["Name"].ToString();
            tbAge.Text = row["Age"].ToString();
            tbSalary.Text = row["Salary"].ToString();
            tbDepartment.Text = row["Department"].ToString();
            resultRow = row;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Id"] = lId.Content;
            resultRow["Name"] = tbName.Text;
            resultRow["Age"] = tbAge.Text;
            resultRow["Salary"] = tbSalary.Text;
            resultRow["Department"] = tbDepartment.Text;
            Mwin.NewRow.EndEdit();
            DbInteractionNamespace.DataTables packet = new DbInteractionNamespace.DataTables();
            Mwin.InteractionService.Update(Mwin.Tables);
            Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Mwin.NewRow.EndEdit();
            DataRow newRow = Mwin.Tables.EmpTable.NewRow();
            newRow["Id"] = Mwin.Tables.EmpTable.Rows.Count + 1;
            newRow["Name"] = tbName.Text;
            newRow["Age"] = tbAge.Text;
            newRow["Salary"] = tbSalary.Text;
            newRow["Department"] = tbDepartment.Text;
            Mwin.Tables.EmpTable.Rows.Add(newRow);
            Mwin.InteractionService.Update(Mwin.Tables);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Mwin.NewRow.EndEdit();
            Mwin.NewRow.Row.Delete();
            Mwin.InteractionService.Update(Mwin.Tables);
            Close();
        }
    }
}
