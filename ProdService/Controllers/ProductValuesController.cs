using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductValuesController : ControllerBase
    {
        private IProductRepository repository;

        public ProductValuesController(IProductRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetProduct(long id)
        {
            return repository.GetProduct(id) ?? NotFound();
        }

        [HttpGet]
        public object Products()
        {
            return repository.GetProducts();
        }

        [HttpPost]
        public void AddProduct([FromBody] Product product)
        {
            repository.AddProduct(product);
        }

        //[HttpPut]
        //public void UpdateProduct([FromBody] Product product)
        //{
        //    repository.UpdateProduct(product);
        //}

        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            repository.DeleteProduct(id);
        }
    }
}
