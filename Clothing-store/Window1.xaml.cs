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

namespace Clothing_store
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        int userID = 1;
        public Window1(int userid)
        {
            InitializeComponent();
            items.entity = new Entities1();
            ListView1.ItemsSource = FindMain();
            sortItems.SelectedIndex = 0;
            //пытаюсь получить idRole, userID == idRole
            //тест
            userID = userid;
            MessageBox.Show(userID.ToString());
            Button myButton = ListView1.FindName("btnBuy") as Button;

            //Visibility.Visible;
            switch (userID) {
                case 1:
                    myButton.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    myButton.Visibility = Visibility.Visible;
                    break;
            }
            
        }


        private void card_Click(object sender, RoutedEventArgs e)
        {

        }

        // переход в корзину
        private void card_Click_1(object sender, RoutedEventArgs e)
        {
            cart cart = new cart(userID);
            cart.Show();
            this.Close();
        }



        // функция для поиска
        main[] FindMain() {
            List<main> mains = AppConnect.model0db.main.ToList();

            if (String.IsNullOrEmpty(findItems.Text) || String.IsNullOrWhiteSpace(findItems.Text))
            {

            }
            else {
                mains = mains.Where(x => x.nameItem.ToLower().Contains(findItems.Text.ToLower())).ToList();
            }

            if (sortItems.SelectedIndex != 0) {
                mains = mains.Where(x => x.idCategor.ToString() == sortItems.SelectedIndex.ToString()).ToList();
            }

            var mainAll = mains;
            return mains.ToArray();
        }

        // поиск
        private void findItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }

       


        //фильтрация
        private void sortItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }

        //отправка товара в корзуну 
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag;
            int userId = (int)App.Current.Properties["userEmail"];

            try
            {
                card card = new card()
                {
                    idMain = (int?)id,
                    idAccount = userId,
                    caunt = 1,
                };
                AppConnect.model0db.card.Add(card);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("товар отправлен в корзину");
            }
            catch (Exception ex) { MessageBox.Show("товар не отправлен в корзину"); }
        }

    }
}
