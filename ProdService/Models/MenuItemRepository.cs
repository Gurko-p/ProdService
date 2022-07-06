using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private ProdServiceContext context;
        public MenuItemRepository(ProdServiceContext ctx) => context = ctx;

        public object GetMenuItem(long id)
        {
            return context.MenuItems.FirstOrDefault(p => p.Id == id);
        }
        public object GetMenuItems()
        {
            return context.MenuItems.OrderBy(p => p.Id);
        }

        public long AddMenuItem(MenuItem menuItem)
        {
            if (menuItem.Id > 0)
            {
                UpdateMenuItem(menuItem);
                return menuItem.Id;
            }
            else
            {
                MenuItem mi = context.MenuItems.Where(mi => mi.Date == menuItem.Date).FirstOrDefault();
                if (mi == null)
                {
                    context.MenuItems.Add(menuItem);
                    context.SaveChanges();
                    long id = context.MenuItems.OrderBy(i => i.Id).Last().Id;
                    return id;
                }
                return mi.Id;
            }
        }
        public void UpdateMenuItem(MenuItem menuItem)
        {
            context.MenuItems.Update(menuItem);
            context.SaveChanges();
        }

        public void DeleteMenuItem(long id)
        {
            context.MenuItems.Remove(new MenuItem { Id = id });
            context.SaveChanges();
        }
    }
}
