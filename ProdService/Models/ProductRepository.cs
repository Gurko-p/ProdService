using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProductRepository : IProductRepository
    {
        private ProdServiceContext context;

        public ProductRepository(ProdServiceContext ctx) => context = ctx;

        public object GetProduct(long id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }
        public object GetProducts()
        {
            return context.Products
                .Select(p => new {
                        Id = p.Id,
                        Name = p.Name,
                        PeelRate = p.PeelRate,
                        HeatTreatmentRate = p.HeatTreatmentRate,
                        Squirrels = p.Squirrels,
                        Fats = p.Fats,
                        Сarbohydrates = p.Сarbohydrates,
                        NormPerDay = p.NormPerDay,
                        KCal = p.KCal,
                        ProductGroupId = p.ProductGroupId,
                        ProductGroup = new
                        {
                            Id = p.ProductGroup.Id,
                            Name = p.ProductGroup.Name,
                        }
                    })
                    .OrderBy(p => p.Name);
        }

        public void AddProduct(Product product)
        {
            if (product.Id > 0)
            {
                UpdateProduct(product);
            }
            else
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            context.Products.Remove(new Product { Id = id });
            context.SaveChanges();
        }
    }
}
