using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CaloriesManagement
{
    /// <summary>
    /// Interaction logic for DishEditMenu.xaml
    /// </summary>
    public partial class DishEditMenu : Window
    {
        private Dish _dish;
        private Database _database;
        public DishEditMenu(Dish dish=null)
        {
            _database = new Database(Database.DBPath);
            InitializeComponent();
            if (dish != null) { 
                _dish = dish;
            }
            else
            {
                _dish = new Dish(_database.GetNewDishId(),"",-1,"");  
            }
            LoadDish();
        }

        private void LoadDish()
        {
            NameText.Text = _dish.Name;
            CaloriesText.Text = _dish.Calories.ToString();
            DescriptionText.Text = _dish.Description;
        }
       
        private void DeleteDish(object sender, RoutedEventArgs e)
        {
            if (_dish.Id != -1) { 
                _database.DeleteDish(_dish.Id);
            }
            this.Close();
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

            if (!int.TryParse(CaloriesText.Text, out int caloriesPer100g) || caloriesPer100g < 0)
            {
                isValid = false;
                errorMessage += "Введіть коректне значення калорій (невід'ємне число).\n";
            }
            if (isValid)
            {
                    Dish dish = new Dish(_dish.Id, NameText.Text, caloriesPer100g, DescriptionText.Text);
                if (_dish.Id != _database.GetNewDishId())
                {
                    _database.UpdateDish(dish);
                }
                else {
                    _database.AddDish(dish);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
