using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IDishCartMenuItemJunctionRepository
    {
        object GetDishCartMenuItemJunctions();
        object GetDishCartMenuItemJunction(long id);
        void AddDishCartMenuItemJunction(DishCartMenuItemJunction dcmij);
        void UpdateDishCartMenuItemJunction(DishCartMenuItemJunction dcmij);
        void DeleteDishCartMenuItemJunction(long id);
    }
}
