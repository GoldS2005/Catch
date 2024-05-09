using Catch.Connection;
using Catch.Connection.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private DataBaseConnection _db;
        public string Email { get; set; }
        public AuthorizationWindow()
        {
            InitializeComponent();
            _db = new DataBaseConnection("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");

            roleComboBox.ItemsSource = new string[] { "Менеджер", "Покупатель", "Администратор" };
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordBox.Password;
            string role = roleComboBox.SelectedItem as string;

           
            string tableName = "";
            if (role == "Менеджер") tableName = "managers";
            else if (role == "Покупатель") tableName = "customers";
            else if (role == "Администратор") tableName = "admins";

           
            try
            {
               
                _db.Open();

                
                string query = $"SELECT * FROM {tableName} WHERE email = @email AND password = @password";
                using (var command = _db.CreateCommand(query))
                {
                    command.Parameters.AddWithValue("@email", email);

                    
                    
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var mainWindow = new MainWindow();
                            if (role == "Менеждер")
                            {
                                mainWindow.ViewOrderButton.IsEnabled = false;
                                mainWindow.addButton.IsEnabled = false;
                                mainWindow.redacButton.IsEnabled = false;
                                mainWindow.deleteButton.IsEnabled = false;
                                mainWindow.ShowDialog();
                            }
                            else if (role == "Покупатель")
                            {
                                mainWindow.Email = email;
                                mainWindow.addButton.IsEnabled = false;
                                mainWindow.redacButton.IsEnabled = false;
                                mainWindow.deleteButton.IsEnabled = false;
                                mainWindow.ShowDialog();
                            }
                            else if (role == "Администратор")
                            {
                                mainWindow.ViewOrderButton.IsEnabled = false;
                                mainWindow.ShowDialog();
                            }
                                
                           
                            this.Close();
                        }
                        else
                        {
                            errorTextBlock.Text = "Неправильный email или пароль.";
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                
                errorTextBlock.Text = "Ошибка базы данных: " + ex.Message;
            }
            catch (Exception ex)
            {
                
                errorTextBlock.Text = "Ошибка: " + ex.Message;
            }
            finally
            {
                
                _db.Close();
            }

        }
            
        }
    }

