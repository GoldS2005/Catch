using Catch.Connection;
using Catch.Connection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Catch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBaseConnection _db;
        private ObservableCollection<Product> _products;

        private List<Product> selectedProducts = new List<Product>(); 
        private decimal orderTotal; 
        public string Email { get; set; }
        public int Order { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");
            _db.Open();
            LoadPickupPoints();

            _products = new ObservableCollection<Product>();
            using (var reader = _db.ExecuteReader("SELECT * FROM products"))
            {
                while (reader.Read())
                {
                    _products.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),

                        Price = reader.GetDecimal(3),
                        CategoryId = reader.GetInt32(4),

                        Image = (byte[])reader.GetValue(5)

                    });
                }


            }


            ProductsListView.ItemsSource = _products;


        }

        private void LoadPickupPoints()
        {
            try
            {
               
                string query = "SELECT * FROM pickup_points";
                using (var reader = _db.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        cbPickupPoints.Items.Add(new PickupPoint { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки пунктов выдачи: " + ex.Message);
            }
           
        }

        private void ProductsListView_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedProduct = (sender as FrameworkElement).DataContext as Product;
            if (selectedProduct != null)
            {
                // Добавление выбранного товара в список выбранных товаров
                selectedProducts.Add(selectedProduct);

                // Обновление общей суммы заказа
                orderTotal += selectedProduct.Price;

                // Обновление интерфейса
                UpdateInterface();
            }
           
        }

        private int CreateOrder(List<Product> selectedProducts, string email)
        {
            int orderId = 0;
            try
            {

                int customerId;

                using (var command = _db.CreateCommand("SELECT id FROM customers WHERE email = @email"))
                {
                    command.Parameters.AddWithValue("@email", email);
                    
                    customerId = (int)command.ExecuteScalar();
                }

               

                PickupPoint selectedPickupPoint = cbPickupPoints.SelectedItem as PickupPoint;
                int managerId = GetRandomManagerId();

                using (var command = _db.CreateCommand("INSERT INTO orders (customer_id, pickup_point_id, manager_id, status) VALUES (@customerId, @pickuppointId, @managerId, 'Новый') RETURNING id"))
                {
                    command.Parameters.AddWithValue("@customerId", customerId); 
                    command.Parameters.AddWithValue("@pickuppointId", selectedPickupPoint.Id);
                    command.Parameters.AddWithValue("@managerId", managerId);
                    orderId = (int)command.ExecuteScalar() ;

                    foreach (var product in selectedProducts)
                    {
                        using (var command2 = _db.CreateCommand("INSERT INTO order_items (order_id, product_id, quantity, price) VALUES (@orderId, @productId, 1, @price)"))
                        {
                            command2.Parameters.AddWithValue("@orderId", orderId);
                            command2.Parameters.AddWithValue("@productId", product.Id);
                            command2.Parameters.AddWithValue("@price", product.Price);
                            command2.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании заказа: " + ex.Message);
            }
            return orderId;
        }
    
        private void UpdateInterface()
        {
            
            ViewOrderButton.Visibility = (selectedProducts.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
            cbPickupPoints.Visibility = (selectedProducts.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        private int GetRandomManagerId()
        {
            try
            {

                using (var command = _db.CreateCommand("SELECT id FROM managers ORDER BY RANDOM() LIMIT 1"))
                {
                    return (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выборе рандомного менеджера: " + ex.Message);
                return 0;
            }
           
        }
        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string email = Email;
            
           
            if (selectedProducts.Count > 0)
            {
                
              int orderId = CreateOrder(selectedProducts, email);

                if (orderId > 0)
                { 
                    PickupPoint selectedPickupPoint = cbPickupPoints.SelectedItem as PickupPoint;


                    this.Close();
                    var orderWindow = new OrderWindow(selectedProducts, orderTotal, selectedPickupPoint.Name, orderId);
                    orderWindow.ShowDialog();
                    
                    if (orderWindow.OrderCreated)
                    {
                       selectedProducts.Clear();
                       orderTotal = 0;
                       UpdateInterface();
                    }

                }

               

                
            }
        }

        
    
       

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var addWindow = new AddWindow();
            addWindow.ShowDialog();
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var productToDelete = (Product)ProductsListView.SelectedItem;
            if (productToDelete == null)
            {
                MessageBox.Show("Выберите товар для удаления.");
                return;
            }

            try
            {
                
                string query = "DELETE FROM products WHERE id = @id";
                using (var command = _db.CreateCommand(query))
                {
                    command.Parameters.AddWithValue("@id", productToDelete.Id);
                    command.ExecuteNonQuery();
                }

                // Удалить товар из ObservableCollection
                _products.Remove(productToDelete);

                MessageBox.Show("Товар успешно удален.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления товара: " + ex.Message);
            }
            
        }

        private void redacButton_Click(object sender, RoutedEventArgs e)
        {
            var productToEdit = (Product)ProductsListView.SelectedItem;
            if (productToEdit == null)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }
            
            this.Close();
            var editWindow = new EditWindow(productToEdit.Id);
            editWindow.ShowDialog();
        }


       
    }
}

