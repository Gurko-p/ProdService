using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class DishCart
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ProductDishCartJunction> ProductDishCarts { get; set; }
        public IEnumerable<DishCartMenuItemJunction> DishCartMenuItemJunctions { get; set; }

        public Dictionary<long, double> GetBruttoProductInDish(long dishCartId)
        {
            Dictionary<long, double> dict = new Dictionary<long, double>();
            foreach (var p in ProductDishCarts.Where(p => p.DishCartId == dishCartId).Distinct())
            {
                if (dict.ContainsKey(Convert.ToInt32(p.ProductId)))
                {
                    double d;
                    dict.Remove(p.ProductId, out d);
                    dict.Add(p.ProductId, d + p.WeightBrutto);
                }
                else
                {
                    dict.Add(p.ProductId, p.WeightBrutto);
                }
            }
            return dict;
        }

        public Dictionary<long, double> GetBruttoCountPerProductOnADay(long menuItemId)
        {
            Dictionary<long, double> dict = new Dictionary<long, double>();
            foreach (var p in DishCartMenuItemJunctions.Where(d => d.MenuItemId == menuItemId).Distinct()
                                                        .Join(ProductDishCarts,
                                                        d => d.DishCartId, p => p.DishCartId,
                                                        (d, p) => new { miId = d.MenuItemId, prodId = p.ProductId, prodBrutto = p.WeightBrutto }))
            {
                if (dict.ContainsKey(Convert.ToInt32(p.prodId)))
                {
                    double d;
                    dict.Remove(p.prodId, out d);
                    dict.Add(p.prodId, d + p.prodBrutto);
                }
                else
                {
                    dict.Add(p.prodId, p.prodBrutto);
                }
            }
            return dict;
        }
        public Dictionary<string, double> getUsefulPropOfTheDish(long id)
        {
            double squirrels = 0; double fats = 0; double carbohydrates = 0; double kCal = 0;
            Dictionary<string, double> dict = new Dictionary<string, double> { ["Squirrels"] = 0, ["Fats"] = 0, ["Сarbohydrates"] = 0, ["KCal"] = 0 };
            foreach (var p in ProductDishCarts.Where(p => p.DishCartId == id).Distinct())
            {
                
                double s = p.Product.Squirrels / 100 * p.WeightBrutto;
                double f = p.Product.Fats / 100 * p.WeightBrutto;
                double c = p.Product.Сarbohydrates / 100 * p.WeightBrutto;
                switch (p.ProcessingId)
                {
                    case 3:
                    case 4:
                        squirrels = s - (s * p.Processing.ReduceSquirrels);
                        fats = f - (f * p.Processing.ReduceFats);
                        carbohydrates = c - (c * p.Processing.ReduceСarbohydrates);
                        break;
                    default:
                        squirrels = s;
                        fats = f;
                        carbohydrates = c;
                        break;
                }
                double sv;
                dict.Remove("Squirrels", out sv);
                dict.Add("Squirrels", sv + squirrels);

                double fv;
                dict.Remove("Fats", out fv);
                dict.Add("Fats", fv + fats);

                double cv;
                dict.Remove("Сarbohydrates", out cv);
                dict.Add("Сarbohydrates", cv + carbohydrates);

                double kv;
                dict.Remove("KCal", out kv);
                dict.Add("KCal", kv + (p.Product.KCal / 100 * p.WeightBrutto));
            }
            dict.Remove("KCal", out kCal);
            dict.Add("KCal", kCal * 0.90);
            return dict;
        }
    }
}
