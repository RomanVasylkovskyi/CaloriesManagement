using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    public class Ingredient
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int CaloriesPer100g { get; set; }

        public Ingredient(int id, string name, int caloriesPer100g)
        {
            Id = id;
            Name = name;
            CaloriesPer100g = caloriesPer100g;
        }

        public override string ToString()
        {
            return $"ID: {Id}  | Name: {Name} ({CaloriesPer100g} cal/100g)";
        }
    }
}
