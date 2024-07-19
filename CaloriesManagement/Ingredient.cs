using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    public class Ingredient
    {
        public string Photo { get; set; }
        public string Name { get; set; }
        public int CaloriesPer100g { get; set; }

        public Ingredient(string photo, string name, int caloriesPer100g)
        {
            Photo = photo;
            Name = name;
            CaloriesPer100g = caloriesPer100g;
        }
    }
}
