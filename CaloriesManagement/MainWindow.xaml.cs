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
using static System.Net.Mime.MediaTypeNames;


namespace CaloriesManagement
{
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
            try
            {
                try
                {
                    string imagePath = "";
                    if (_user.Gender == 0)
                    {
                        imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "woman.png");
                    }
                    else { 
                        imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "man.png");
                    }
                    if (System.IO.File.Exists(imagePath))
                    {
                        UserImg.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex){ }
            //MessageBox.Show(_user.ToString(), "User Info");
        }

        private void SettingsMenu(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Closed += (s, args) => refreshInfo();
            settings.ShowDialog();
        }
    }
}