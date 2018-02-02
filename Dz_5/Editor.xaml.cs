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
using System.Windows.Shapes;

namespace Dz_5
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public MainWindow.Employee Worker { get; set; }
        public Editor()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow)
            {
                MainWindow Mwin = Owner as MainWindow;
                GetInformation(Worker);
                Mwin.lvEmployee.Items.Refresh();
            }

            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow)
            {
                MainWindow Mwin = Owner as MainWindow;
                MainWindow.Employee worker = new MainWindow.Employee();
                GetInformation(worker);
                worker.Id = Mwin.Workers.Count + 1;
                Mwin.Workers.Add(worker);
                Mwin.lvEmployee.Items.Refresh();
            }

            Close();
        }

        private void GetInformation(MainWindow.Employee worker)
        {
            worker.Name = textBox1.Text;
            worker.Age = Convert.ToInt32(textBox2.Text);
            worker.Salary = Convert.ToInt32(textBox3.Text);
            worker.Department = comboBox.SelectedItem as MainWindow.Department;
        }
    }
}
