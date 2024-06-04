using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private main editMain;

        public AddItem(main editMain) {
            InitializeComponent();
            this.editMain = editMain;

            //получение данных из admin для изменение
            var nameItemCh = editMain.nameItem.ToString();
            var priceCh = Convert.ToInt32(editMain.price.ToString());
            var infoItemCh = editMain.infoItem.ToString();
            var photoItemCh = editMain.photoItem.ToString();
            var idCategorCh = editMain.idCategor;
            var idColorCh = editMain.idColor;
            var idMaterialCh = editMain.idMaterial;


            //ввод этих данных на их места
            nameItem.Text = nameItemCh;
            price.Text = Convert.ToString(priceCh);
            infoItem.Text = infoItemCh;
            photoItem.Text = photoItemCh;
            idCategor.SelectedIndex = Convert.ToInt32(idCategorCh) - 1;
            idColor.SelectedIndex = Convert.ToInt32(idColorCh) - 1;
            idMaterial.SelectedIndex = Convert.ToInt32(idMaterialCh) - 1;;
        }


        //Добавление товара в main
        private void Additem_Click(object sender, RoutedEventArgs e)
        {
            
            //получение данных в переменную
            var NA = nameItem.Text.ToString();
            var PR = price.Text.ToString();
            var INF = infoItem.Text.ToString();
            var PHOTOITEM = photoItem.Text.ToString();
            var CA = idCategor.SelectedIndex + 1;
            var COL = idColor.SelectedIndex + 1;
            var MA = idMaterial.SelectedIndex + 1;


            //проверка данных на корректность
            int error = 0;
            if (NA.Length < 1) 
            {
                error++;
            }
            if (PR.Length < 1)
            {
                error++;
            }
            if (INF.Length < 1)
            {
                error++;
            }
            if (PHOTOITEM.Length < 1)
            {
                error++;
            }
            if (CA == 0)
            {
                error++;
            }
            if (COL == 0)
            {
                error++;
            }
            if (MA == 0)
            {
                error++;
            }

            //если нету ошибок то добавляем товар
            if (error == 0)
            {
                try
                {
                    //проверка на изменение или добавлнеие
                    var id = 0;
                    if (editMain != null)
                    {
                        id = editMain.id;
                    }
                    main mainItems = new main()
                    {
                        id = id,
                        nameItem = nameItem.Text,
                        price = Convert.ToInt32(price.Text),
                        infoItem = infoItem.Text,
                        photoItem = photoItem.Text,
                        idCategor = idCategor.SelectedIndex + 1,
                        idColor = idColor.SelectedIndex + 1,
                        idMaterial = idMaterial.SelectedIndex + 1
                    };

                    //если данные уже есть тоесть мы изменяем, мы это ловим с помошью переменной и меняем данные
                    AppConnect.model0db.main.AddOrUpdate(mainItems);
                    AppConnect.model0db.SaveChanges();
                    MessageBox.Show("товар добавлен");

                    //переход на страницу с товаром
                    Admin admin = new Admin();
                    admin.Show();
                    Close();
                }
                catch (Exception ex) { MessageBox.Show("что то пошло не так"); }
                

            }
            //если есть ошибки то предупреждаем
            else 
            {
                MessageBox.Show("Обнаружены пустые данные");
            }
        }

        //вернутся на страницу с товарами 
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            Close();
        }
    }
}
