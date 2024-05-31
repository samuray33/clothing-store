using System;
using System.CodeDom;
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
        int userID = 1;
        // подключение к бд для вывода данных из таблицы card
        public cart(int userid)
        {
            InitializeComponent();
            items.entity = new Entities1();
            ListCart.ItemsSource = AppConnect.model0db.card.ToList();
            
            //общая стоимость товаров в корзине 
            var a = AppConnect.model0db.card.ToList();
            int sumPrice = 0;

            for (int i = 0; i < a.Count; i++)
            {
                int gooid = (int)a[i].idMain;
                main b = AppConnect.model0db.main.FirstOrDefault(x => x.id == gooid);
                sumPrice += (int)b.price;
            }
            coast.Text = sumPrice.ToString();
            
            userID = userid;
        }

        // обратно на главную страницу
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1(userID);
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
