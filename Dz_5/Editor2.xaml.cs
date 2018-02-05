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
    /// Логика взаимодействия для Editor2.xaml
    /// </summary>
    public partial class Editor2 : Window
    {
        public MainWindow.Department Department { get; set; }
        public Editor2()
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
                Department.Name = tbName.Text;
            }

            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow)
            {
                MainWindow Mwin = Owner as MainWindow;
                MainWindow.Department department = new MainWindow.Department();
                department.Name = tbName.Text;
                Mwin.Departments.Add(department);
            }

            Close();
        }
    }
}
