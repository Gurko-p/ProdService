using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/dishcarts")]
    [ApiController]
    public class DishCartValuesController : ControllerBase
    {
        private IDishCartRepository repository;

        public DishCartValuesController(IDishCartRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetDishCart(long id)
        {
            return repository.GetDishCart(id) ?? NotFound();
        }

        [HttpGet]
        public object GetDishCarts()
        {
            return repository.GetDishCarts();
        }

        [HttpPost]
        public long AddDishCart([FromBody] DishCart dishCart)
        {
            return repository.AddDishCart(dishCart);
        }

        //[HttpPut]
        //public void UpdateDishCart([FromBody] DishCart dishCart)
        //{
        //    repository.UpdateDishCart(dishCart);
        //}

        [HttpDelete("{id}")]
        public void DeleteDishCart(long id)
        {
            repository.DeleteDishCart(id);
        }
    }
}
