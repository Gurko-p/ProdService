using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IDishCartRepository
    {
        object GetDishCarts();
        object GetDishCart(long id);
        long AddDishCart(DishCart dishCart);
        void UpdateDishCart(DishCart dishCart);
        void DeleteDishCart(long id);
    }
}
