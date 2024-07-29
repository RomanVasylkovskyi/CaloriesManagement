using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesManagement
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishIngredient> Ingredients { get; set; }

        public Dish(int id, string name)
        {
            Id = id;
            Name = name;
            Ingredients = new List<DishIngredient>();
        }

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(di => di.Ingredient.CaloriesPer100g * di.Quantity / 100);
        }
    }

    public class DishIngredient
    {
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }

        public DishIngredient(Ingredient ingredient, double quantity)
        {
            Ingredient = ingredient;
            Quantity = quantity;
        }
    }
}
