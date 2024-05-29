using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для cart.xaml
    /// </summary>
    public partial class cart : Window
    {
        // подключение к бд для вывода данных из таблицы card
        public cart()
        {
            InitializeComponent();
            items.entity = new Entities1();
            ListCart.ItemsSource = AppConnect.model0db.card.ToList();
        }

        // обратно на главную страницу
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1();
            Window1.Show();
            Close();
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag;
            var itemDel = AppConnect.model0db.card.Where(x => x.id == (int?)id);
            try
            {
                AppConnect.model0db.card.RemoveRange(itemDel);
                AppConnect.model0db.SaveChanges();
                AppConnect.model0db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ListCart.ItemsSource = AppConnect.model0db.card.ToList();
            }
            catch 
            {
                MessageBox.Show("Обьект не не удален");
            }
        }
    }
}
