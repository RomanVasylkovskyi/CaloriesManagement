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
        public int Calories { get; set; }
        public string Description { get; set; }

        public Dish(int id, string name,int caolries,string description)
        {
            Id = id;
            Name = name;
            Calories = caolries;
            Description = description;
        }
    }
}
