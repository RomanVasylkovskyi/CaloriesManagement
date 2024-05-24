using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    class FoodItem
    {
        public string Name { get; set; }
        public double Calories { get; set; }

        public FoodItem(string name, double calories)
        {
            Name = name;
            Calories = calories;
        }

    }
}
