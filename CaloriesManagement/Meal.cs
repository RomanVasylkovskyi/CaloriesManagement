using System;
using System.Collections.Generic;

namespace CaloriesManagement
{
    class Meal
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } 

        public Meal(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>(); 
        }

        public void AddFoodItem(Ingredient foodItem)
        {
            Ingredients.Add(foodItem);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var item in Ingredients)
            {
                totalCalories += item.CaloriesPer100g;
            }
            return totalCalories;
        }
    }

}
