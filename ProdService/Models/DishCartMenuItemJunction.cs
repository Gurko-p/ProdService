using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class DishCartMenuItemJunction
    {
        public long Id { get; set; }

        public long DishCartId { get; set; }
        public DishCart DishCart { get; set; }

        public long MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public long MealId { get; set; }
        public Meal Meal { get; set; }
        
    }
}
