using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProductDishCartJunctionRepository : IProductDishCartJunctionRepository
    {
        private ProdServiceContext context;
        public ProductDishCartJunctionRepository(ProdServiceContext ctx) => context = ctx;

        public object GetProductDishCartJunction(long id)
        {
            return context.ProductDishCartJunctions.FirstOrDefault(p => p.Id == id);
        }
        public object GetProductDishCartJunctions()
        {
            return context.ProductDishCartJunctions;
        }
        public void AddProductDishCartJunction(ProductDishCartJunction pdcj)
        {
            if (pdcj.Id > 0)
            {
                UpdateProductDishCartJunction(pdcj);
            }
            else
            {
                context.ProductDishCartJunctions.Add(pdcj);
                context.SaveChanges();
            }
        }
        public void UpdateProductDishCartJunction(ProductDishCartJunction pdcj)
        {
            context.ProductDishCartJunctions.Update(pdcj);
            context.SaveChanges();
        }

        public void DeleteProductDishCartJunction(long id)
        {
            context.ProductDishCartJunctions.Remove(new ProductDishCartJunction { Id = id });
            context.SaveChanges();
        }
    }
}
