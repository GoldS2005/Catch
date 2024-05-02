using Catch.Connection;
using Catch.Connection.Model;
using Microsoft.Win32;
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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private int _productId;
        private DataBaseConnection _db;
        private byte[] _imageData;
        private Category _selectedCategory;
        public EditWindow(int productId)
        {
            InitializeComponent();
            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");
            _productId = productId;


           
            LoadCategories();
            LoadProductData();



        }

        private void LoadProductData()
        {
            try
            {
                _db.Open();
                string query = "SELECT name, description, price, category_id, image FROM products WHERE id = @id";
                using (var command = _db.CreateCommand(query))
                {
                    command.Parameters.AddWithValue("@id", _productId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nameTextBox.Text = reader.GetString(0);
                            descriptionTextBox.Text = reader.GetString(1);
                            priceTextBox.Text = reader.GetDecimal(2).ToString();
                            int categoryId = reader.GetInt32(3);

                            _selectedCategory = cbCategory.Items.Cast<Category>().FirstOrDefault(c => c.Id == categoryId);

                            if (!reader.IsDBNull(4))
                            {
                                _imageData = (byte[])reader[4];
                                BitmapImage image = new BitmapImage();
                                image.BeginInit();
                                image.StreamSource = new MemoryStream(_imageData);
                                image.EndInit();
                                ImagePreview.Source = image;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных товара: " + ex.Message);
            }
            finally
            {
                _db.Close();
            }
        }
        private void LoadCategories()
        {
            try
            {
                _db.Open();
                string query = "SELECT * FROM categories";
                using (var reader = _db.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        cbCategory.Items.Add(new Category { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message);
            }
            finally
            {
                _db.Close();
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string description = descriptionTextBox.Text;
            decimal price;
            if (!decimal.TryParse(priceTextBox.Text, out price))
            {
                errorTextBlock.Text = "Некорректная цена.";
                return;
            }
            

            try
            {
                _db.Open();
                string query = "UPDATE products SET name = @name, description = @description, price = @price, category_id = @category_id, image = @image WHERE id = @id";
                using (var command = _db.CreateCommand(query))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@category_id", _selectedCategory.Id);
                    command.Parameters.AddWithValue("@image", _imageData);
                    command.Parameters.AddWithValue("@id", _productId);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Товар успешно обновлен.");
                this.Close();
                var mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления товара: " + ex.Message);
            }
            finally
            {
                _db.Close();
            }
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true)
            {
                _imageData = File.ReadAllBytes(openFileDialog.FileName);
                ImagePreview.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
