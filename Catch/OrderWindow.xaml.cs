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
        private Order _currentOrder;
        private int _orderId;
        private bool _orderCreated;

        public bool OrderCreated => _orderCreated;

        public OrderWindow()
        {
            InitializeComponent();

            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");



            LoadOrderData();






        }

        private void LoadOrderData()
        {
            try
            {
                _db.Open();

                Order order = new Order();

                // Получение информации о заказе
                var orderQuery = "SELECT * FROM orders WHERE id = @orderId";
                using (var command = _db.CreateCommand(orderQuery))
                {
                    command.Parameters.AddWithValue("@orderId", order.Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _currentOrder = new Order
                            {
                                Id = (int)reader["id"],
                                CustomerId = (int)reader["customer_id"],
                                ManagerId = (int)reader["manager_id"],
                                PickupPointId = (int)reader["pickup_point_id"],
                                Status = (string)reader["status"],
                                CreatedAt = (DateTime)reader["created_at"],
                                UpdatedAt = (DateTime)reader["updated_at"],
                            };

                            // Отображение информации о заказе
                            OrderIdLabel.Text = _currentOrder.Id.ToString();
                            OrderStatusLabel.Text = _currentOrder.Status;
                            CreatedAtLabel.Text = _currentOrder.CreatedAt.ToString();
                            UpdatedAtLabel.Text = _currentOrder.UpdatedAt.ToString();
                        }
                    }
                }

                // Получение позиций заказа
                _orderItems = new ObservableCollection<OrderItem>();
                var orderItemsQuery = "SELECT * FROM order_items WHERE order_id = @orderId";
                using (var command = _db.CreateCommand(orderItemsQuery))
                {
                    command.Parameters.AddWithValue("@orderId", order.Id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderItem = new OrderItem
                            {
                                Id = (int)reader["id"],
                                OrderId = (int)reader["order_id"],
                                ProductId = (int)reader["product_id"],
                                Quantity = (int)reader["quantity"],
                                Price = (decimal)reader["price"]
                            };
                            _orderItems.Add(orderItem);
                        }
                    }
                }

                OrderItemsGrid.ItemsSource = _orderItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных заказа: " + ex.Message);
            }
            finally
            {
                _db.Close();
            }
        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик события удаления позиции заказа
            if (OrderItemsGrid.SelectedItem != null)
            {
                var selectedOrderItem = (OrderItem)OrderItemsGrid.SelectedItem;
                _orderItems.Remove(selectedOrderItem);

                try
                {
                    _db.Open();
                    var deleteQuery = "DELETE FROM order_items WHERE id = @orderItemId";
                    using (var deleteCommand = _db.CreateCommand(deleteQuery))
                    {
                        deleteCommand.Parameters.AddWithValue("@orderItemId", selectedOrderItem.Id);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении позиции заказа: " + ex.Message);
                }
                finally
                {
                    _db.Close();
                }
            }
        }



        private void FinishOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Завершение заказа

            // Генерация кода получения

            // Создание талона заказа в PDF

            // Сохранение талона заказа в PDF

            // Обновление статуса заказа в базе данных PostgreSQL

            try
            {
                _db.Open();

                Order order = new Order();
                var updateQuery = "UPDATE orders SET status = 'Completed' WHERE id = @orderId";
                using (var updateCommand = _db.CreateCommand(updateQuery))
                {
                    updateCommand.Parameters.AddWithValue("@orderId", order.Id);
                    updateCommand.ExecuteNonQuery();
                }

                _orderCreated = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при завершении заказа: " + ex.Message);
            }
            finally
            {
                _db.Close();
            }

        }



    }

}

        
 
