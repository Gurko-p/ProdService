using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/productgroups")]
    [ApiController]
    public class ProductGroupValuesController : ControllerBase
    {
        private IProductGroupRepository repository;

        public ProductGroupValuesController(IProductGroupRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetProductGroup(long id)
        {
            return repository.GetProductGroup(id) ?? NotFound();
        }

        [HttpGet]
        public object GetProductGroups()
        {
            return repository.GetProductGroups();
        }

        [HttpPost]
        public void AddProductGroup([FromBody] ProductGroup productGroup)
        {
            repository.AddProductGroup(productGroup);
        }

        //[HttpPut]
        //public void UpdateProduct([FromBody] Product product)
        //{
        //    repository.UpdateProduct(product);
        //}

        [HttpDelete("{id}")]
        public void DeleteProductGroup(long id)
        {
            repository.DeleteProductGroup(id);
        }
    }
}
