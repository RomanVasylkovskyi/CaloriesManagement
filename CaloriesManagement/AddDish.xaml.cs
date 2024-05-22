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
    /// <summary>
    /// Interaction logic for AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        public AddDish()
        {
            InitializeComponent();
        }
        private void AddDishCalDBButton_Click(object sender, RoutedEventArgs e)
        {
            string DishName = NameDish.Text.ToLower();
            string DishWeight = WeightDish.Text.ToLower();
            string DishCallorie = CalDish.Text.ToLower();
            if (string.IsNullOrWhiteSpace(NameDish.Text) || string.IsNullOrWhiteSpace(WeightDish.Text) || string.IsNullOrWhiteSpace(CalDish.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі текстові поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Страву додано");
            }
        }

        private void backMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backMenu = new MainWindow();

            backMenu.Show();

            this.Close();
        }
    }
}
