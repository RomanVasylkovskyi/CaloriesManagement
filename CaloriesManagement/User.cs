using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Gender { get; set; }

        public User(string name, int age, double weight, double height, string gender)
        {
            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
            Gender = gender;
        }

        public double CalculateBMR()
        {
            if (Gender.ToLower() == "male")
            {
                return 88.36 + (13.4 * Weight) + (4.8 * Height) - (5.7 * Age);
            }
            else
            {
                return 447.6 + (9.2 * Weight) + (3.1 * Height) - (4.3 * Age);
            }
        }
    }
}
