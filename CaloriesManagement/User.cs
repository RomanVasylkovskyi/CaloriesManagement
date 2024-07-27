using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Gender { get; set; }

        public User() { }

        public User(string name, int age, double weight, double height, int gender)
        {
            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
            Gender = gender;
        }

        public double CalculateBMR()
        {
            if (Gender==1)
            {
                return 88.36 + (13.4 * Weight) + (4.8 * Height) - (5.7 * Age);
            }
            else
            {
                return 447.6 + (9.2 * Weight) + (3.1 * Height) - (4.3 * Age);
            }
        }

        public override string ToString()
        {
            return $"Ім'я: {Name}\n" +
                   $"Вік: {Age} років\n" +
                   $"Вага: {Weight:F1} кг\n" +
                   $"Зріст: {Height:F1} см\n" +
                   $"Стать: {(Gender == 1 ? "Чоловіча" : "Жіноча")}\n" +
                   $"BMR: {CalculateBMR():F2} калорій";
        }
    }
}
