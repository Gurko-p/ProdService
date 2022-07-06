using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class DishCartRepository : IDishCartRepository
    {
        private ProdServiceContext context;
        public DishCartRepository(ProdServiceContext ctx) => context = ctx;

        public object GetDishCart(long id)
        {
            return context.DishCarts.FirstOrDefault(p => p.Id == id);
        }
        public object GetDishCarts()
        {
            return context.DishCarts.OrderBy(p => p.Name);
        }

        public long AddDishCart(DishCart dishCart)
        {
            if (dishCart.Id > 0)
            {
                UpdateDishCart(dishCart);
                return dishCart.Id;
            }
            else
            {
                context.DishCarts.Add(dishCart);
                context.SaveChanges();
                long id = context.DishCarts.OrderBy(i => i.Id).Last().Id;
                return id;
            }
        }
        public void UpdateDishCart(DishCart dishCart)
        {
            context.DishCarts.Update(dishCart);
            context.SaveChanges();
        }

        public void DeleteDishCart(long id)
        {
            context.DishCarts.Remove(new DishCart { Id = id });
            context.SaveChanges();
        }
    }
}
