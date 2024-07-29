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
    /// Interaction logic for DishListMenu.xaml
    /// </summary>
    public partial class DishListMenu : Window
    {

        private Database database;
        public DishListMenu()
        {
            database = new Database(Database.DBPath);
            InitializeComponent();
            LoadDishes();
        }

        private void LoadDishes()
        {
            List<Dish> dishes = database.GetAllDishes();
            ListView.ItemsSource = dishes;
        }

        private void Add_Dish(object sender, RoutedEventArgs e)
        {
            DishEditMenu dish = new DishEditMenu();
            dish.Closed += (s, args) => LoadDishes();
            dish.ShowDialog();
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

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
