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

namespace Clothing_store
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        //переход на фрейм регистрации
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainframe.Navigate(new Page2());
        }

        // проверка есть ли пользователь в бд, если есть то преход на страницу с товаром
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userObj = AppConnect.model0db.account.FirstOrDefault(x => x.email == Email.Text && x.password == Password.Password);
            //проверка если у пользователя 1 уровень доступа то переход на стараницу админ
            if (userObj.idRole == 1) {
                App.Current.Properties["userEmail"] = userObj.id;
                Admin admin = new Admin();
                admin.Show();
                Application.Current.MainWindow.Close();
            }
            else if (userObj != null)
            {
                App.Current.Properties["userEmail"] = userObj.id;
                Window1 window = new Window1((int)userObj.idRole);
                window.Show();
                Application.Current.MainWindow.Close();
            }
            else 
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}
