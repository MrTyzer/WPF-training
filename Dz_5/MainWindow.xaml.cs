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

namespace Dz_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employee> Workers = new ObservableCollection<Employee>();
        public ObservableCollection<Department> Departments = new ObservableCollection<Department>();

        public MainWindow()
        {
            InitializeComponent();
            FillList();
        }

        void FillList()
        {
            Departments.Add(new Department() {  Name = "Образования" });
            Departments.Add(new Department() {  Name = "Здравоохранения" });
            Departments.Add(new Department() {  Name = "Транспорта" });
            lvDepartments.ItemsSource = Departments;
            Workers.Add(new Employee() { Id = 1, Name = "Вася", Age = 22, Salary = 3000, Department = Departments[0] });
            Workers.Add(new Employee() { Id = 2, Name = "Петя", Age = 25, Salary = 6000, Department = Departments[1] });
            Workers.Add(new Employee() { Id = 3, Name = "Коля", Age = 23, Salary = 8000, Department = Departments[2] });
            lvEmployee.ItemsSource = Workers;
        }


        public class Employee : INotifyPropertyChanged
        {
            private int id;
            public int Id
            {
                get { return id; }
                set
                {
                    if (id != value)
                    {
                        id = value;
                        NotifyPropertyChanged("Id");
                    }
                }
            }
            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    if (name != value)
                    {
                        name = value;
                        NotifyPropertyChanged("Name");
                    }
                }
            }
            private int age;
            public int Age
            {
                get { return age; }
                set
                {
                    if (age != value)
                    {
                        age = value;
                        NotifyPropertyChanged("Age");
                    }
                }
            }
            private double salary;
            public double Salary
            {
                get { return salary; }
                set
                {
                    if (salary != value)
                    {
                        salary = value;
                        NotifyPropertyChanged("Salary");
                    }
                }
            }
            private Department department;
            public Department Department
            {
                get { return department; }
                set
                {
                    if (department != value)
                    {
                        department = value;
                        NotifyPropertyChanged("Department");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void NotifyPropertyChanged(string propName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

            public override string ToString()
            {
                return $"{Id}\t{Name}\t{Age}\t{Salary}\t{Department}";
            }
        }

        public class Department : INotifyPropertyChanged
        {
            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    if (name != value)
                    {
                        name = value;
                        NotifyPropertyChanged("Name");
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            public void NotifyPropertyChanged(string propName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
            public override string ToString()
            {
                return $"{Name}";
            }
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsEnabled = false;
            Employee worker;
            if (e.AddedItems[0] is Employee)
            {
                worker = e.AddedItems[0] as Employee;
                Editor childWindow = new Editor();
                childWindow.Owner = this;
                childWindow.Closing += Enable;
                childWindow.Show();
                childWindow.Activate();
                childWindow.Worker = worker;
                childWindow.lId.Content = worker.Id;
                childWindow.tbName.Text = worker.Name;
                childWindow.tbAge.Text = worker.Age.ToString();
                childWindow.tbSalary.Text = worker.Salary.ToString();
                childWindow.cbDepartment.ItemsSource = Departments;
                childWindow.cbDepartment.SelectedItem = worker.Department;
            }
        }

        private void lvDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsEnabled = false;
            Department department;
            if (e.AddedItems[0] is Department)
            {
                department = e.AddedItems[0] as Department;
                Editor2 childWindow = new Editor2();
                childWindow.Closing += Enable;
                childWindow.Owner = this;
                childWindow.Show();
                childWindow.Activate();
                childWindow.Department = department;
                childWindow.tbName.Text = department.Name;
            }
        }

        public void Enable(object sender, EventArgs e)
        {
            IsEnabled = true;
        }

    }
}
