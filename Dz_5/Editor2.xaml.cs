﻿using System;
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
    /// Логика взаимодействия для Editor2.xaml
    /// </summary>
    public partial class Editor2 : Window
    {
        public DataRow resultRow { get; set; }
        public MainWindow Mwin { get; set; }

        public Editor2(DataRow row, MainWindow mwin)
        {
            InitializeComponent();
            Mwin = mwin;
            tbName.Text = row["Name"].ToString();
            resultRow = row;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Name"] = tbName.Text;
            Mwin.NewRow.EndEdit();
            Mwin.InteractionService.Update(Mwin.Tables);
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Mwin.NewRow.EndEdit();
            DataRow newRow = Mwin.Tables.DepTable.NewRow();
            newRow["Id"] = Mwin.Tables.DepTable.Rows.Count + 1;
            newRow["Name"] = tbName.Text;
            Mwin.Tables.DepTable.Rows.Add(newRow);
            Mwin.InteractionService.Update(Mwin.Tables);
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Mwin.NewRow.EndEdit();
            Mwin.NewRow.Row.Delete();
            Mwin.InteractionService.Update(Mwin.Tables);
            Close();
        }
    }
}
