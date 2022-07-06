using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IProcessingRepository
    {
        object GetProcessings();
        object GetProcessing(long id);
        void AddProcessing(Processing processing);
        void UpdateProcessing(Processing processing);
        void DeleteProcessing(long id);
    }
}
