using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IProductGroupRepository
    {
        object GetProductGroups();
        object GetProductGroup(long id);
        void AddProductGroup(ProductGroup productGroup);
        void UpdateProductGroup(ProductGroup productGroup);
        void DeleteProductGroup(long id);
    }
}
