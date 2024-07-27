using System;
using System.Collections.Generic;
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
using System.Xml;

namespace CaloriesManagement
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private User _user = new User();
        private Database _database;
        public Settings()
        {
            InitializeComponent();
            _database = new Database(Database.DBPath);
            refreshInfo();
        }

        public void refreshInfo()
        {
            _user = _database.GetUser(1);

            NameText.Text = _user.Name;
            AgeText.Text = _user.Age.ToString();
            WeightText.Text = _user.Weight.ToString();
            HeightText.Text = _user.Height.ToString();
        }

        private void SetWomen(object sender, RoutedEventArgs e)
        {
            _user.Gender = 0;
            LoadImg();
        }

        private void SetMan(object sender, RoutedEventArgs e)
        {
            _user.Gender = 1;
            LoadImg();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(NameText.Text))
            {
                isValid = false;
                errorMessage += "Ім'я не може бути порожнім.\n";
            }
            if (!int.TryParse(AgeText.Text, out int age) || age <= 0 || age > 150)
            {
                isValid = false;
                errorMessage += "Введіть коректний вік (від 1 до 150).\n";
            }
            if (!double.TryParse(HeightText.Text, out double height) || height <= 0 || height > 300)
            {
                isValid = false;
                errorMessage += "Введіть коректний зріст (від 1 до 300 см).\n";
            }
            if (!double.TryParse(WeightText.Text, out double weight) || weight <= 0 || weight > 700)
            {
                isValid = false;
                errorMessage += "Введіть коректну вагу (від 1 до 700 кг).\n";
            }
            if (isValid)
            {
                _user.Name = NameText.Text;
                _user.Age = age;
                _user.Height = height;
                _user.Weight = weight;
                _database.SaveUser(_user);
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadImg() {
            try
            {
                try
                {
                    string imagePath = "";
                    if (_user.Gender == 0)
                    {
                        imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "woman.png");
                    }
                    else
                    {
                        imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "man.png");
                    }
                    if (System.IO.File.Exists(imagePath))
                    {
                        UserImg.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex) { }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
