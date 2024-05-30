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
            // получаем данные из поле Patronymic, если оно пустое то пишем "Нет отчества"
            var Patro = Patronymic.Text.ToString();
            if (Patro.Length < 1)
            {
                Patro = "Нет отчества";
            }

            //Проверка корректности данных при регистрации(получение данных в переменные)
            var EM = Email.Text.ToString();
            var PS = Password.Password.ToString();
            var GN = Gender.SelectedIndex;
            var NA = Name.Text.ToString();
            var SN = Surname.Text.ToString();
            //MessageBox.Show(EM + " " + PS + "" + GN + "" + NA + "" + SN);
            //Проверка корректности данных при регистрации(сама проверка)
            var cheData = 0;

            if (EM.Length < 1) 
            {
                cheData++;
            }
            if (PS.Length < 1)
            {
                cheData++;
            }
            if (GN==0)
            {
                cheData++;
            }
            if (NA.Length < 1)
            {
                cheData++;
            }
            if (SN.Length < 1)
            {
                cheData++;
            }

            // Если все данные введены корректно то отправляем данные в бд и перенаправляем в ВХОД
            if (cheData == 0)
            {
                try
                {
                    account personObj = new account()
                    {
                        name = Name.Text,
                        surname = Surname.Text,
                        patronymic = Patro,
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
            else 
            {
                MessageBox.Show("Обнаружены пустые данные"); 
            }
        }



    }
}
