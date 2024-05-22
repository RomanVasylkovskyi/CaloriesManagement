﻿using System;
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
    /// Interaction logic for InfoCal.xaml
    /// </summary>
    public partial class InfoCal : Window
    {
        public InfoCal()
        {
            InitializeComponent();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string EnterDishtName = EnterDish.Text.ToLower();
            if (string.IsNullOrWhiteSpace(EnterDish.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі текстові поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Вивести калорійність страви");
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
