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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private DataBaseConnection _db;
        private byte[] _imageData;

        public AddWindow()
        {
            InitializeComponent();
            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");
            LoadCategories();
            

            
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
            Category selectedCategory = cbCategory.SelectedItem as Category;

            try
            {
                _db.Open();
                string query = "INSERT INTO products (name, description, price, category_id, image) VALUES (@name, @description, @price, @category_id, @image)";
                using (var command = _db.CreateCommand(query))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@category_id", selectedCategory.Id);
                    command.Parameters.AddWithValue("@image", _imageData);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Товар успешно добавлен.");
                this.Close();
                var mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара: " + ex.Message);
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
