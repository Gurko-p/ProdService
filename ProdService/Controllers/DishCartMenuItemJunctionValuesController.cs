using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/dishCartMenuItemJunctions")]
    [ApiController]
    public class DishCartMenuItemJunctionValuesController : ControllerBase
    {
        private IDishCartMenuItemJunctionRepository repository;

        public DishCartMenuItemJunctionValuesController(IDishCartMenuItemJunctionRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetDishCartMenuItemJunction(long id)
        {
            return repository.GetDishCartMenuItemJunction(id) ?? NotFound();
        }

        [HttpGet]
        public object GetDishCartMenuItemJunctions()
        {
            return repository.GetDishCartMenuItemJunctions();
        }

        [HttpPost]
        public void AddDishCartMenuItemJunction([FromBody] DishCartMenuItemJunction dcmij)
        {
            repository.AddDishCartMenuItemJunction(dcmij);
        }

        //[HttpPut]
        //public void UpdateDishCartMenuItemJunction([FromBody] DishCartMenuItemJunction dcmij)
        //{
        //    repository.UpdateDishCartMenuItemJunction(dcmij);
        //}

        [HttpDelete("{id}")]
        public void DeleteDishCartMenuItemJunction(long id)
        {
            repository.DeleteDishCartMenuItemJunction(id);
        }
    }
}
