using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
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

using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Paragraph = iTextSharp.text.Paragraph;
using Aspose.BarCode.Generation;
using Image = iTextSharp.text.Image;
using Document = iTextSharp.text.Document;

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

        //Удаление
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

        //функция для создании и передачи pdf кода
        private void PDF() 
        {
            //попытка создать pdf файл
            //Document docPdf = new Document();
            // try
            //{
            //    PdfWriter.GetInstance(docPdf, new FileStream("..\\..\\Чек.pdf", FileMode.Create));

            //    docPdf.Open();
            //    BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //    Font font = new Font(baseFont, 12);
            //    Font font1 = new Font(baseFont, 23, 3, BaseColor.BLACK);

            //    Paragraph line = new Paragraph("--------------------------------------------------------------------", font1);
            //    line.Alignment = Element.ALIGN_CENTER;
            //    docPdf.Add(line);

            //    Paragraph title = new Paragraph("ЧЕК", font1);
            //    title.Alignment = Element.ALIGN_CENTER;
            //    docPdf.Add(title);

            //    docPdf.Add(line);
                
            //    int cnt = 0;
            //    decimal sumW = 0;
            //    decimal sumR = 0;

            //    foreach (var item in ListCart.Items)
            //    {
            //        if (item is card)
            //        {
            //            card data = (card)item;

            //            Image img = Image.GetInstance(@"S:\USERS\51-02\Хаитов Некруз Самиджонович\практика\Clothing-store\Clothing-store\img\" + data.main.photoItem);
            //            img.ScaleAbsolute(100f, 100f);
            //            docPdf.Add(img);
            //            docPdf.Add(new Paragraph($"Название: {data.main.nameItem}", font));
            //            docPdf.Add(new Paragraph($"Цена: {data.main.price}", font));
            //            docPdf.Add(new Paragraph($"О товаре: {data.main.infoItem}", font));
            //            docPdf.Add(new Paragraph($"Категория: {data.main.idCategor}", font));
            //            docPdf.Add(new Paragraph($"Цвет: {data.main.idColor}", font));
            //            docPdf.Add(new Paragraph($"Материал: {data.main.idMaterial}", font));
            //            docPdf.Add(line);

            //            cnt += data.order_quantity;
            //            sumW += data.Toys_ToyStore.toy_wholesalePrice * data.order_quantity;
            //            sumR += data.Toys_ToyStore.toy_retailPrice * data.order_quantity;
            //        }
            //    }

            //    Paragraph ordN = new Paragraph($"Номер заказа: {numOrder}", font);
            //    ordN.Alignment = Element.ALIGN_RIGHT;
            //    docPdf.Add(ordN);
                
            //    Paragraph TotalQuantity = new Paragraph($"Общее количество товаров: {cnt}", font);
            //    Paragraph TotalSumW = new Paragraph($"Оптовая сумма: {sumW}", font);
            //    Paragraph TotalSumR = new Paragraph($"Розничная сумма: {sumR}", font);
                
            //    TotalQuantity.Alignment = Element.ALIGN_RIGHT;
            //    TotalSumW.Alignment = Element.ALIGN_RIGHT;
            //    TotalSumR.Alignment = Element.ALIGN_RIGHT;

            //    docPdf.Add(TotalQuantity);
            //    docPdf.Add(TotalSumW);
            //    docPdf.Add(TotalSumR);
            //}
            //catch (DocumentException de)
            //{
            //    Console.Error.WriteLine(de.Message);
            //}
            //catch (IOException ioe)
            //{
            //    Console.Error.WriteLine(ioe.Message);
            //}
            //finally
            //{
            //    docPdf.Close();
            //}
        }

        //QR функция
        int a = 1;
        private void doQR()
        {
            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://yandex.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fcdn.nwmgroups.hu%2Fs%2Fimg%2Fi%2F2211%2F20221104bean-1997-uk-cinema-appareil.jpg&lr=2&pos=4&rpt=simage&text=%D1%84%D0%BE%D1%82%D0%BE%20%D0%BC%D0%B5%D0%BC%D0%BE%D0%B2");
            gen.Parameters.Barcode.XDimension.Pixels = 34;
            string dataDir = @"S:\USERS\51-02\Хаитов Некруз Самиджонович\практика\Clothing-store\Clothing-store\";
            gen.Save(dataDir + a.ToString() + "1.png", BarCodeImageFormat.Png);
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + a.ToString() + "1.png");
            bitmap.EndInit();
            QRimg.Source = bitmap;
            a++;
        }

        // при нажатии на оформить срабатывает PDF() и doQR()
        private void by_Click_1(object sender, RoutedEventArgs e)
        {
            doQR();
            PDF();
        }
    }
}
