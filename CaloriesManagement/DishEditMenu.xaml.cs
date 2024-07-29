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
                _dish = new Dish(_database.GetNewDishId(),"");  
            }
            LoadDish();
        }

        private void LoadDish()
        {
            NameText.Text = _dish.Name;
            TotalCaloriesText.Content = _dish.CalculateTotalCalories();
            List<Ingredient> ingredients = _database.GetAllIngredients();
            ListView.ItemsSource = ingredients;
            GridView gv = ListView.View as GridView;
            if (gv != null)
            {
                GridViewColumn idColumn = gv.Columns[0];
                GridViewColumn nameColumn = gv.Columns[1];
                GridViewColumn caloriesColumn = gv.Columns[2];
                GridViewColumn weightColumn = gv.Columns[3];
                idColumn.Header = "ID";
                idColumn.DisplayMemberBinding = new Binding("Id");
                nameColumn.Header = "Назва";
                nameColumn.DisplayMemberBinding = new Binding("Name");
                caloriesColumn.Header = "Калорії (на 100г)";
                caloriesColumn.DisplayMemberBinding = new Binding("CaloriesPer100g");
                weightColumn.Header = "Вага";
                weightColumn.DisplayMemberBinding = new Binding("Weight");
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem is Ingredient selectedIngredient)
            {
                //IngredientForm ingredient = new IngredientForm(selectedIngredient);
                //ingredient.Closed += (s, args) => LoadIngredients();
                //ingredient.ShowDialog();
            }

        }

        private void DeleteIngredientFromDish(object sender, RoutedEventArgs e)
        {

        }
        private void AddIngredientToDish(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteDish(object sender, RoutedEventArgs e)
        {

        }
        private void Save(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
