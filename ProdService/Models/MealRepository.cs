using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class MealRepository : IMealRepository
    {
        private ProdServiceContext context;
        public MealRepository(ProdServiceContext ctx) => context = ctx;

        public object GetMeal(long id)
        {
            return context.Meals.FirstOrDefault(p => p.Id == id);
        }
        public object GetMeals()
        {
            return context.Meals.OrderBy(p => p.Name);
        }

        public void AddMeal(Meal meal)
        {
            if (meal.Id > 0)
            {
                UpdateMeal(meal);
            }
            else
            {
                context.Meals.Add(meal);
                context.SaveChanges();
            }
        }
        public void UpdateMeal(Meal meal)
        {
            context.Meals.Update(meal);
            context.SaveChanges();
        }

        public void DeleteMeal(long id)
        {
            context.Meals.Remove(new Meal { Id = id });
            context.SaveChanges();
        }
    }
}
