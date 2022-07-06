using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProductDishCartJunction
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long DishCartId { get; set; }
        public DishCart DishCart { get; set; }

        public long? ProcessingId { get; set; } = null;
        public Processing Processing { get; set; }
        public double WeightBrutto { get; set; }

        public static double getNetto(ProductDishCartJunction p)
        {
            double val = 0;
            switch (p.ProcessingId)
            {
                case 2:
                    val = p.WeightBrutto - (p.WeightBrutto * p.Product.PeelRate);
                    break;
                case 3:
                    if (p.Product.HeatTreatmentRate < 1)
                    {
                        val = p.WeightBrutto - (p.WeightBrutto * p.Product.HeatTreatmentRate);
                    }
                    else
                    {
                        val = p.WeightBrutto * p.Product.HeatTreatmentRate;
                    }
                    break;
                case 4:
                    double peal = p.WeightBrutto - (p.WeightBrutto * p.Product.PeelRate);
                    val = peal - (peal * p.Product.HeatTreatmentRate);
                    break;
                default:
                    val = p.WeightBrutto;
                    break;
            }
            return val;
        }
    }
}
