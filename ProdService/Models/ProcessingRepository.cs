using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProcessingRepository : IProcessingRepository
    {
        private ProdServiceContext context;
        public ProcessingRepository(ProdServiceContext ctx) => context = ctx;

        public object GetProcessing(long id)
        {
            return context.Processings.FirstOrDefault(p => p.Id == id);
        }
        public object GetProcessings()
        {
            return context.Processings.OrderBy(p => p.Name);
        }

        public void AddProcessing(Processing processing)
        {
            if (processing.Id > 0)
            {
                UpdateProcessing(processing);
            }
            else
            {
                context.Processings.Add(processing);
                context.SaveChanges();
            }
        }
        public void UpdateProcessing(Processing processing)
        {
            context.Processings.Update(processing);
            context.SaveChanges();
        }

        public void DeleteProcessing(long id)
        {
            context.Processings.Remove(new Processing { Id = id });
            context.SaveChanges();
        }
    }
}
