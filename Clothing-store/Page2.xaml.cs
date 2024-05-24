using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            fillGender();
        }

        void fillGender() 
        {

        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainframe.Navigate(new Page1());
        }

        private void Regis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                account personObj = new account()
                {
                    name = Name.Text,
                    surname = Surname.Text,
                    patronymic = Patronymic.Text,
                    email = Email.Text,
                    password = Password.Password,
                    idGender = Gender.SelectedIndex + 1,
                    idRole = 2,
                };

                AppConnect.model0db.account.Add(personObj);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Пользователь добавлен");
                AppFrame.mainframe.Navigate(new Page1());
            }
            catch (Exception ex) { MessageBox.Show(Gender.SelectedIndex.ToString()); }
        }



    }
}
