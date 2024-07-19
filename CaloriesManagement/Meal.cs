using System;
using System.Collections.Generic;

namespace CaloriesManagement
{
    class Meal
    {
        public string Name { get; set; }
        public string Photo { get; set; } 
        public List<string> Ingredients { get; set; } 
        public List<FoodItem> FoodItems { get; set; }

        public Meal(string name)
        {
            Name = name;
            Photo = string.Empty; 
            Ingredients = new List<string>(); 
            FoodItems = new List<FoodItem>();
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }

        public void AddIngredient(string ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (var item in FoodItems)
            {
                totalCalories += item.Calories;
            }
            return totalCalories;
        }
    }

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
