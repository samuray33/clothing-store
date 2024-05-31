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

namespace Clothing_store
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            ListView1.ItemsSource = FindMain();
            sortItems.SelectedIndex = 0;
        }

        main[] FindMain()
        {
            List<main> mains = AppConnect.model0db.main.ToList();

            if (String.IsNullOrEmpty(findItems.Text) || String.IsNullOrWhiteSpace(findItems.Text))
            {

            }
            else
            {
                mains = mains.Where(x => x.nameItem.ToLower().Contains(findItems.Text.ToLower())).ToList();
            }

            if (sortItems.SelectedIndex != 0)
            {
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

        //кнопка для удаление товара из таблицы
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag;
            var itemDel = AppConnect.model0db.main.Where(x => x.id == (int?)id);
            try
            {
                AppConnect.model0db.main.RemoveRange(itemDel);
                AppConnect.model0db.SaveChanges();
                AppConnect.model0db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ListView1.ItemsSource = AppConnect.model0db.main.ToList();
            }
            catch
            {
                MessageBox.Show("Обьект не не удален");
            }
        }
    }
}
