using Catch.Connection;
using Catch.Connection.Model;
using iTextSharp.text;
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
        private Order _currentOrder;
        private int _orderId;
        private bool _orderCreated;
        public List<Product> SelectedProducts { get; set; }
        public decimal OrderTotal { get; set; }

        public string PickupPoint { get; set; }

        public bool OrderCreated => _orderCreated;

        public OrderWindow(List<Product> selectedProducts, decimal orderTotal, string pickupPoint, int orderId)
        {
            InitializeComponent();

            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");
            _db.Open();
            SelectedProducts = selectedProducts;
            OrderTotal = orderTotal;

            PickupPoint = pickupPoint;
            _orderId = orderId;

            SelectedProductsListView.ItemsSource = SelectedProducts;
            TotalAmountTextBlock.Text = $"Общая сумма: {OrderTotal:C}";

            PickupPointTextBlock.Text = PickupPoint;

            CodeTextBlock.Text = $"Код получения: {GenerateRandomCode(3)}";
            

            int deliveryDays = int.Parse(GenerateDelivery(1));
            string dayWord;

            switch (deliveryDays)
            {
                case 1:
                    dayWord = "день";
                    break;
                case 2:
                case 3:
                case 4:
                    dayWord = "дня";
                    break;
                default:
                    dayWord = "дней";
                    break;
            }

            DeliveryTextBlock.Text = $"Срок доставки: {deliveryDays} {dayWord}";



        }

        private static Random random = new Random();
        private static string GenerateRandomCode(int length)
        {

            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GenerateDelivery(int length)
        {

            const string chars = "123456";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var command = _db.CreateCommand("UPDATE orders SET status = 'Отправлен' WHERE id = @orderId"))
                {
                    command.Parameters.AddWithValue("@orderId", _orderId); 
                    int result = command.ExecuteNonQuery();

                    if (result == 1)
                    {
                        MessageBox.Show("Заказ успешно завершен.");
                        

                        SaveWindowContentToPdf(this);
                        this.Close();
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при завершении заказа.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении статуса заказа: " + ex.Message);
            }
        }

        

        public void SaveWindowContentToPdf(Window window)
        {
            try
            {
                
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)window.Width, (int)window.Height, 96, 96, PixelFormats.Pbgra32);
                VisualBrush visualBrush = new VisualBrush(window);
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(0, 0), new System.Windows.Point(window.Width, window.Height)));
                }
                renderTargetBitmap.Render(drawingVisual);

                
                PngBitmapEncoder pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                byte[] imageArray;

                using (MemoryStream outputStream = new MemoryStream())
                {
                    pngImage.Save(outputStream);
                    imageArray = outputStream.ToArray();
                }

                
                Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                string pdfPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\WindowContent.pdf";
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                iTextSharp.text.Image windowImage = iTextSharp.text.Image.GetInstance(imageArray);
                windowImage.ScaleToFit(document.PageSize.Width - 50, document.PageSize.Height - 50);
                document.Add(windowImage);

                document.Close();
                writer.Close();

                MessageBox.Show($"Талон сохранен в PDF: {pdfPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }
    }


}


    



        
 
