using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class MenuHelperClass
    {
        public DateTime MenuItemDate { get; set; }
        public long MenuItemId { get; set; }
        public List<MealDishProd> MealDishProd { get; set; }
        public Dictionary<long, double> KKalPerDish { get; set; }
        public Dictionary<long, double> ProductsPerDayCount { get; set; }
    }
    public class MealDishProd
    {
        public string MealName { get; set; }
        public long MealId { get; set; }
        public List<Prod> Prods { get; set; }
    }
    public class Prod
    {
        public string DishName { get; set; }
        public Dictionary<long, double> ProdCount { get; set; }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ProdService.Models
//{
//    public class MenuHelperClass
//    {
//        public DateTime MenuItemDate { get; set; }
//        public long MenuItemId { get; set; }
//        public Dictionary<string, IList<Dictionary<string, Dictionary<long, double>>>> MealsDishProdWeight { get; set; }
//        public Dictionary<long, double> KKalPerDish { get; set; }
//        public Dictionary<long, double> ProductsPerDayCount { get; set; }
//    }
//}