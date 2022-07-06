using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class MenuItem
    {
        public long Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public IEnumerable<DishCartMenuItemJunction> DishCartMenuItemJunctions { get; set; }
    }
}
