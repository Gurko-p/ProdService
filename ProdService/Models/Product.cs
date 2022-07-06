using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double PeelRate { get; set; } // коэффициент при очистке, %
        public double HeatTreatmentRate { get; set; } // коэффициент при тепловой обработке, %
        public double Squirrels { get; set; } // белки, 100 г (нетто)
        public double Fats { get; set; } // жиры, 100 г (нетто)
        public double Сarbohydrates { get; set; } // углеводы, 100 г (нетто)
        public double NormPerDay { get; set; } // норма в день
        public double KCal { get; set; } // энергетическая ценность (кКал), 100 гр (Брутто)
        public long ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public IEnumerable<ProductDishCartJunction> ProductDishCarts { get; set; }

    }
}
