using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class DishCartMenuItemJunctionRepository : IDishCartMenuItemJunctionRepository
    {
        private ProdServiceContext context;
        public DishCartMenuItemJunctionRepository(ProdServiceContext ctx) => context = ctx;

        public object GetDishCartMenuItemJunction(long id)
        {
            return context.DishCartMenuItemJunctions.FirstOrDefault(p => p.Id == id);
        }
        public object GetDishCartMenuItemJunctions()
        {
            return context.DishCartMenuItemJunctions.OrderBy(p => p.Id);
        }

        public void AddDishCartMenuItemJunction(DishCartMenuItemJunction dcmij)
        {
            if (dcmij.Id > 0)
            {
                UpdateDishCartMenuItemJunction(dcmij);
            }
            else
            {
                context.DishCartMenuItemJunctions.Add(dcmij);
                context.SaveChanges();
            }
        }
        public void UpdateDishCartMenuItemJunction(DishCartMenuItemJunction dcmij)
        {
            context.DishCartMenuItemJunctions.Update(dcmij);
            context.SaveChanges();
        }

        public void DeleteDishCartMenuItemJunction(long id)
        {
            context.DishCartMenuItemJunctions.Remove(new DishCartMenuItemJunction { Id = id });
            context.SaveChanges();
        }
    }
}
