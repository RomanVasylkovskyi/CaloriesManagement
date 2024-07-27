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
using System.Xml;

namespace CaloriesManagement
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private User _user = new User();
        private Database _database;
        public Settings()
        {
            InitializeComponent();
            _database = new Database(Database.DBPath);
            refreshInfo();
        }

        public void refreshInfo()
        {
            _user = _database.GetUser(1);

            NameLabel.Text = _user.Name;
            AgeLabel.Text = _user.Age.ToString();
            WeightLabel.Text = _user.Weight.ToString();
            HeightLabel.Text = _user.Height.ToString();
        }

        private void SetWomen(object sender, RoutedEventArgs e)
        {
            _user.Gender = 0;
        }

        private void SetMan(object sender, RoutedEventArgs e)
        {
            _user.Gender = 1;
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
