using Catch.Connection;
using Catch.Connection.Model;
using iTextSharp.text.pdf;
using SelectPdf;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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


namespace Catch
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private DataBaseConnection _db;
        private ObservableCollection<OrderItem> _orderItems;
        private ObservableCollection<Order> _orders;
        private ObservableCollection<PickupPoint> _pickuppoints;
        private int _nextOrderId;
        private int _currentOrderId;
        private Order currentOrder;

        private bool orderCreated;

        public bool OrderCreated => orderCreated;

        public OrderWindow()
        {
            InitializeComponent();

            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");
            _db.Open();
        



           
            


        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FinishOrderButton_Click(object sender, RoutedEventArgs e)
        {
            
           
        }
    


    private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            
            var random = new Random();
            var orderCode = random.Next(100, 999).ToString();

            
            var pdfDocument = new PdfDocument();
            var pdfPage = pdfDocument.AddPage();
            
            var graphics = pdfPage.Graphics;
            object value = graphics.DrawString("Дата заказа: " + DateTime.Now.ToString("dd.MM.yyyy"), new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, 10));
            graphics.DrawString("Номер заказа: " + orderCode, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, 30));
            graphics.DrawString("Состав заказа:", new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, 50));

       
            var yPosition = 70;
            foreach (var orderItem in _orderItems)
            {
                graphics.DrawString(orderItem.ProductId, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition));
                yPosition += 20;
            }

            PickupPoint pickupPoint = new PickupPoint();
            

           
            graphics.DrawString("Пункт выдачи:", new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition));
            yPosition += 20;
            graphics.DrawString(pickupPoint.Name, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition));
            yPosition += 20;
            graphics.DrawString(pickupPoint.Address, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition));
            yPosition += 20;
            graphics.DrawString(pickupPoint.PhoneNumber, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition));
            yPosition += 20;

            
            graphics.DrawString("Код получения:", new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition));
            graphics.DrawString(orderCode, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold), PdfBrushes.Black, new PointF(100, yPosition));

           
            var deliveryTime = "Срок доставки: ";
            if (_orderItems.Any(x => x.Quantity < 3))
            {
                deliveryTime += "6 дней";
            }
            else
            {
                deliveryTime += "3 дня";
            }
            graphics.DrawString(deliveryTime, new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new PointF(10, yPosition + 20));

            
            using (var fileStream = new FileStream("order.pdf", FileMode.Create))
            {
                pdfDocument.Save(fileStream);
            }

            
            MessageBox.Show("Талон заказа сохранен в файл order.pdf");
            */
        }

      

      
    }

}

        
 
