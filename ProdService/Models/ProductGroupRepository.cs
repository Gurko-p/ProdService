using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private ProdServiceContext context;
        public ProductGroupRepository(ProdServiceContext ctx) => context = ctx;

        public object GetProductGroup(long id)
        {
            return context.ProductGroups.FirstOrDefault(p => p.Id == id);
        }
        public object GetProductGroups()
        {
            return context.ProductGroups.OrderBy(p => p.Name);
        }

        public void AddProductGroup(ProductGroup productGroup)
        {
            if (productGroup.Id > 0)
            {
                UpdateProductGroup(productGroup);
            }
            else
            {
                context.ProductGroups.Add(productGroup);
                context.SaveChanges();
            }
        }
        public void UpdateProductGroup(ProductGroup productGroup)
        {
            context.ProductGroups.Update(productGroup);
            context.SaveChanges();
        }

        public void DeleteProductGroup(long id)
        {
            context.ProductGroups.Remove(new ProductGroup { Id = id });
            context.SaveChanges();
        }
    }
}
