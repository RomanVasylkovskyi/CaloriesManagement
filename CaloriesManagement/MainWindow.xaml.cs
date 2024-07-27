﻿using System.IO;
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

            string[] files = Directory.GetFiles("../img/");
            string text = "";
            foreach (string file in files)
            {
               text += $"{file}\n";
            }
            MessageBox.Show(text);

            try
            {
                string imagePath = _user.Gender == 0 ? "../img/man.png" : "../img/man.png";
                Uri imageUri = new Uri(imagePath, UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.UriSource = imageUri;
                bitmapImage.EndInit();

                UserImg.Source = bitmapImage;
            }
            catch (Exception ex)
            {
              
            }
            //MessageBox.Show(_user.ToString(), "User Info");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }
}