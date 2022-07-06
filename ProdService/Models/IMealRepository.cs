using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public interface IMealRepository
    {
        object GetMeals();
        object GetMeal(long id);
        void AddMeal(Meal meal);
        void UpdateMeal(Meal meal);
        void DeleteMeal(long id);
    }
}
