using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IProductDishCartJunctionRepository
    {
        object GetProductDishCartJunctions();
        object GetProductDishCartJunction(long id);
        void AddProductDishCartJunction(ProductDishCartJunction pdcj);
        void UpdateProductDishCartJunction(ProductDishCartJunction pdcj);
        void DeleteProductDishCartJunction(long id);
    }
}
