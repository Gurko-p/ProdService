using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProductDishUsefulProperty
    {
        public long DishCartId { get; set; }
        public string DishCartName { get; set; }
        public Dictionary<long, ProductNettoInDish> productNettoInDishes { get; set; }
        public double Squirrels { get; set; } // белки, 100 г (нетто)
        public double Fats { get; set; } // жиры, 100 г (нетто)
        public double Сarbohydrates { get; set; } // углеводы, 100 г (нетто)
        public double KCal { get; set; } // энергетическая ценность (кКал), 100 гр (Брутто)
        
    }

    public class ProductNettoInDish
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public double ProductWeightBrutto { get; set; }
        public double ProductWeightNetto { get; set; }
        public string ProcessingName { get; set; }
    }
}
