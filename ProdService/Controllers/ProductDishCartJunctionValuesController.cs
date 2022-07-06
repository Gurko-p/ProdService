using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/productDishCarts")]
    [ApiController]
    public class ProductDishCartJunctionValuesController : ControllerBase
    {
        private IProductDishCartJunctionRepository repository;

        public ProductDishCartJunctionValuesController(IProductDishCartJunctionRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetProductDishCartJunction(long id)
        {
            return repository.GetProductDishCartJunction(id) ?? NotFound();
        }

        [HttpGet]
        public object GetProductDishCartJunctions()
        {
            return repository.GetProductDishCartJunctions();
        }

        [HttpPost]
        public void AddProductDishCartJunction([FromBody] ProductDishCartJunction pdcj)
        {
            repository.AddProductDishCartJunction(pdcj);
        }

        //[HttpPut]
        //public void UpdateProductDishCartJunction([FromBody] ProductDishCartJunction pdcj)
        //{
        //    repository.UpdateProductDishCartJunction(pdcj);
        //}

        [HttpDelete("{id}")]
        public void DeleteProductDishCartJunction(long id)
        {
            repository.DeleteProductDishCartJunction(id);
        }
    }
}
