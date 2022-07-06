using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IProductRepository
    {
        //object GetProducts(int skip, int take);
        object GetProducts();
        object GetProduct(long id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(long id);
    }
}
