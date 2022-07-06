using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProdService.Models;

namespace ProdService.Controllers
{
    [Route("api/processings")]
    [ApiController]
    public class ProcessingValuesController : ControllerBase
    {
        private IProcessingRepository repository;

        public ProcessingValuesController(IProcessingRepository repo)
            => repository = repo;

        [HttpGet("{id}")]
        public object GetProcessing(long id)
        {
            return repository.GetProcessing(id) ?? NotFound();
        }

        [HttpGet]
        public object GetProcessings()
        {
            return repository.GetProcessings();
        }

        [HttpPost]
        public void AddProcessing([FromBody] Processing processing)
        {
            repository.AddProcessing(processing);
        }

        //[HttpPut]
        //public void UpdateProcessing([FromBody] Processing processing)
        //{
        //    repository.UpdateProcessing(processing);
        //}

        [HttpDelete("{id}")]
        public void DeleteProcessing(long id)
        {
            repository.DeleteProcessing(id);
        }
    }
}
