using System.Diagnostics;
using System.IO;
using System.Reflection;
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
using System.Xml;
using static System.Net.Mime.MediaTypeNames;


namespace CaloriesManagement
{
    public class Imgloader
    {
        string Path { get; set; }
        public Imgloader(string path) { Path = path; }

        public BitmapImage Load()
        {
            try
            {
                string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", Path);
                return new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
    }

    public partial class MainWindow : Window
    {
        private User _user = new User();
        private Database _database;
        public MainWindow()
        {
            InitializeComponent();
            _database = new Database(Database.DBPath);
            refreshInfo();
        }

        public void refreshInfo()
        {
            _user = _database.GetUser(1);

            NameLabel.Content = _user.Name;
            Age.Content = _user.Age;
            WeightLabel.Content = _user.Weight;
            HeightLabel.Content = _user.Height;
            UpdateBMI();
            try
            {
                if (_user.Gender == 0)
                {
                    Imgloader imgl = new Imgloader("woman.png");
                    UserImg.Source = imgl.Load();
                }
                else {
                    Imgloader imgl = new Imgloader("man.png");
                    UserImg.Source = imgl.Load();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            //MessageBox.Show(_user.ToString(), "User Info");
        }

        private void UpdateBMI()
        {
            double bmi = _user.CalculateBMI();
            ProgressBarElement.Value = _user.ScaleBMI();
            ProgressBarText.Content = _user.GetBMICategory();
            //ProgressText.Text = $"{bmi:F2}";
            //BmiText.Text = $"Your BMI is {bmi:F2} ({bmiCategory})";

            ProgressBarElement.Foreground = new SolidColorBrush(_user.GetColorForBMI());
        }

        private void SettingsMenu(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Closed += (s, args) => refreshInfo();
            settings.ShowDialog();
        }
    }
}