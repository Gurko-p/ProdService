using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/menuItems")]
    [ApiController]
    public class MenuItemValuesController : ControllerBase
    {
        private IMenuItemRepository repository;

        public MenuItemValuesController(IMenuItemRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetMenuItem(long id)
        {
            return repository.GetMenuItem(id) ?? NotFound();
        }

        [HttpGet]
        public object GetMenuItems()
        {
            return repository.GetMenuItems();
        }

        [HttpPost]
        public long AddMenuItem([FromBody] MenuItem menuItem)
        {
            return repository.AddMenuItem(menuItem);
        }

        //[HttpPut]
        //public void UpdateMenuItem([FromBody] MenuItem menuItem)
        //{
        //    repository.UpdateMenuItem(menuItem);
        //}

        [HttpDelete("{id}")]
        public void DeleteMenuItem(long id)
        {
            repository.DeleteMenuItem(id);
        }
    }
}
