using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    class Meal
    {
        public string Name { get; set; }
        public List<FoodItem> FoodItems { get; set; }

        public Meal(string name)
        {
            Name = name;
            FoodItems = new List<FoodItem>();
        }

        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
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
}
