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
    /// Interaction logic for AddProd.xaml
    /// </summary>
    public partial class AddProd : Window
    {
        public AddProd()
        {
            InitializeComponent();
        }
        private void AddProdCalDBButton_Click(object sender, RoutedEventArgs e)
        {
            string ProductName = NameProd.Text.ToLower();
            string ProductWeight = WeightProd.Text.ToLower();
            string ProductCallorie = CalProd.Text.ToLower();
            if (string.IsNullOrWhiteSpace(NameProd.Text) || string.IsNullOrWhiteSpace(WeightProd.Text) || string.IsNullOrWhiteSpace(CalProd.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі текстові поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Продукт додано");
            }
        }

        private void backMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backMenu = new MainWindow();

            backMenu.Show();

            this.Close();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                string selectedCategory = radioButton.Content.ToString()!;
                // Виконати дії відповідно до вибраної категорії
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть категорію!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
