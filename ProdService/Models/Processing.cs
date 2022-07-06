using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class Processing
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double ReduceSquirrels { get; set; } // процент уменьшения количества белков
        public double ReduceFats { get; set; } // процент уменьшения количества жиров
        public double ReduceСarbohydrates { get; set; } // процент уменьшения количества углеводов
    }
}
