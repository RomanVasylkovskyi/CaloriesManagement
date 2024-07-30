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
            TotalCaloriesText.Content = _dish.Calories;
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
