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
        public DbInteractionNamespace.DbInteractionClient InteractionService { get; private set; }
        public DbInteractionNamespace.DataTables Tables { get; private set; }
        public DataRowView NewRow;

        public MainWindow()
        {
            InitializeComponent();
            InteractionService = new DbInteractionNamespace.DbInteractionClient();
            Tables = InteractionService.Init();
            dgEmployee.DataContext = Tables.EmpTable.DefaultView;
            dgDepartment.DataContext = Tables.DepTable.DefaultView;
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
