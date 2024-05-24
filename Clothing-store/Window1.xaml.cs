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
            ListView1.ItemsSource = FindMain();
        }

        private void card_Click(object sender, RoutedEventArgs e)
        {

        }

        // переход в корзину
        private void card_Click_1(object sender, RoutedEventArgs e)
        {
            cart cart = new cart();
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

            var mainAll = mains;
            return mains.ToArray();
        }

        // поиск
        private void findItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }

        //отправка товара в корзуну (не получается)
        private void buy_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag;
            int userId = (int)App.Current.Properties["userEmail"];

            try
            {
                card card = new card()
                {
                    idAccount = userId,
                    idMain = (int?)id,
                    caunt = 1,

                };
                AppConnect.model0db.card.Add(card);
                MessageBox.Show("товар отправлен в корзину + " + userId + " + " + (int?)id);
            }
            catch (Exception ex) { MessageBox.Show("товар не отправлен в корзину"); }

        }

        //фильтрация
        private void sortItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem coAll = (ComboBoxItem)sortItems.SelectedItem;
            string value = coAll.Content.ToString();
            int id = 0;
            switch (value)
            {
                case "все":
                    ListView1.ItemsSource = AppConnect.model0db.main.ToList();
                    return;
                case "мужская":
                    id = 1;
                    break;
                case "женская":
                    id = 2;
                    break;
                case "детская":
                    id = 3;
                    break;
            }
            ListView1.ItemsSource = AppConnect.model0db.main.Where(x => x.idCategor == id).ToList();
        }

    }
}
