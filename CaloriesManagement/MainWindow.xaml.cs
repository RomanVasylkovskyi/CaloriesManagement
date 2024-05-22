using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaloriesManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddDishCalButton_Click(object sender, RoutedEventArgs e)
        {
            AddDish dishWindow = new AddDish();

            dishWindow.Show();

            this.Close();
        }

        private void CalckCalButton_Click(object sender, RoutedEventArgs e)
        {
            CalcCal CalcWindow = new CalcCal();

            CalcWindow.Show();

            this.Close();
        }

        private void AddProdCalButton_Click(object sender, RoutedEventArgs e)
        {
            AddProd prodWindow = new AddProd();

            prodWindow.Show();

            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoCal InfoCalWindow = new InfoCal();

            InfoCalWindow.Show();

            this.Close();
        }
    }
}