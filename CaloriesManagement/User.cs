using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CaloriesManagement
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Gender { get; set; }

        public User() { }

        public User(int id, string name, int age, double weight, double height, int gender)
        {
            Id = id;
            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
            Gender = gender;
        }

        public double CalculateBMI()
        {
            return Weight / (Height * Height);
        }

        public double ScaleBMI()
        {
            double bmi = CalculateBMI();
            return 10000 * ((bmi - 0) / (40 - 0) * (100 - 0) + 0);
        }

        public string GetBMICategory()
        {
            double bmi = ScaleBMI();
            if (bmi < 35)
                return "Underweight";
            if (bmi < 60)
                return "Normal weight";
            if (bmi < 75)
                return "Overweight";
            return "Obese";
        }


        public Color GetColorForBMI()
        {
            double bmi = ScaleBMI();
            if (bmi < 35)
                return Colors.Blue;
            if (bmi < 60)
                return Colors.Green;
            if (bmi < 75)
                return Colors.Orange;
            return Colors.Red;
        }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Ім'я: {Name}\n" +
                   $"Вік: {Age} років\n" +
                   $"Вага: {Weight:F1} кг\n" +
                   $"Зріст: {Height:F1} см\n" +
                   $"Стать: {(Gender == 1 ? "Чоловіча" : "Жіноча")}\n" +
                   $"BMR: {CalculateBMI():F2} калорій";
        }
    }
}
