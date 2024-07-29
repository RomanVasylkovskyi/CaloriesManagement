using System;
using System.Collections.Generic;
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

    public partial class IngredientForm : Window
    {
        private Database _database;
        private Ingredient Ingredient;
        
        public IngredientForm(Ingredient ingredient =null)
        {
            _database = new Database(Database.DBPath);
            InitializeComponent();
            if (ingredient != null)
            {
                Ingredient = ingredient;
            }
            else {
                Ingredient = new Ingredient(_database.GetNewIngredientId(), "", -1);
            }
            refresh_info();
        }

        private void refresh_info() { 
            IngredientNameText.Text = Ingredient.Name;
            IngredientCaloriesText.Text = Ingredient.CaloriesPer100g.ToString();
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(IngredientNameText.Text))
            {
                isValid = false;
                errorMessage += "Ім'я не може бути порожнім.\n";
            }
            if (!int.TryParse(IngredientCaloriesText.Text, out int caloriesPer100g) || caloriesPer100g < 0)
            {
                isValid = false;
                errorMessage += "Введіть коректне значення калорій (невід'ємне число).\n";
            }
            if (isValid)
            {
                Ingredient tmp = new Ingredient(Ingredient.Id,IngredientNameText.Text, caloriesPer100g);
                _database.AddOrUpdateIngredient(Ingredient, tmp);
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            _database.DeleteIngredientById(Ingredient.Id);
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
