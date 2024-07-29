using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Interaction logic for IngredientsMenu.xaml
    /// </summary>
    public partial class IngredientsMenu : Window
    {
        private Database database;
        public IngredientsMenu()
        {
            database = new Database(Database.DBPath);
            InitializeComponent();
            LoadIngredients();
        }
        private void LoadIngredients()
        {
            List<Ingredient> ingredients =  database.GetAllIngredients();

            ListView.ItemsSource = ingredients;
        }

        private void Add_Ingredient(object sender, RoutedEventArgs e)
        {

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Метод для обробки події SelectionChanged
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem is Ingredient selectedIngredient)
            {
                MessageBox.Show(selectedIngredient.ToString());
            }
            
        }

    }
}
