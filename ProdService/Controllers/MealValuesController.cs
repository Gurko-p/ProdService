using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/meals")]
    [ApiController]
    public class MealValuesController : ControllerBase
    {
        private IMealRepository repository;

        public MealValuesController(IMealRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetMeal(long id)
        {
            return repository.GetMeal(id) ?? NotFound();
        }

        [HttpGet]
        public object GetMeals()
        {
            return repository.GetMeals();
        }

        [HttpPost]
        public void AddMeal([FromBody] Meal meal)
        {
            repository.AddMeal(meal);
        }

        //[HttpPut]
        //public void UpdateMeal([FromBody] Meal meal)
        //{
        //    repository.UpdateMeal(product);
        //}

        [HttpDelete("{id}")]
        public void DeleteMeal(long id)
        {
            repository.DeleteMeal(id);
        }
    }
}
