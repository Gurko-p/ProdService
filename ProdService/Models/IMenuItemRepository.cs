using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IMenuItemRepository
    {
        object GetMenuItems();
        object GetMenuItem(long id);
        long AddMenuItem(MenuItem menuItem);
        void UpdateMenuItem(MenuItem menuItem);
        void DeleteMenuItem(long id);
    }
}
