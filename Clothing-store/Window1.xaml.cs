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
using System.Windows.Shapes;

namespace Clothing_store
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            items.entity = new Entities1();
            ListView1.ItemsSource = items.entity.main.ToList();
        }

        private void card_Click(object sender, RoutedEventArgs e)
        {

        }

        private void card_Click_1(object sender, RoutedEventArgs e)
        {
            card card = new card();
            card.Show();
            this.Close();
        }
    }
}
