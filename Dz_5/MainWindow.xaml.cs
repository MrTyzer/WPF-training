﻿using System;
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

namespace Dz_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> Workers = new ObservableCollection<Employee>();
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

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public double Salary { get; set; }
            public Department Department { get; set; }
            public override string ToString()
            {
                return $"{Id}\t{Name}\t{Age}\t{Salary}\t{Department}";
            }
        }
        public class Department
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return $"{Name}";
            }
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Window win = new Window();
        }
    }
}