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
    /// Interaction logic for CalcCal.xaml
    /// </summary>
    public partial class CalcCal : Window
    {
        public CalcCal()
        {
            InitializeComponent();
        }
        private void CalcCalButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вивести потрібну змінну");
        }
        private void RadioCalButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                string selectedGoal = radioButton.Content.ToString()!;
                // Виконати дії відповідно до вибраної категорії
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть категорію!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
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
