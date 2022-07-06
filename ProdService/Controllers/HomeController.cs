using Microsoft.AspNetCore.Mvc;
using ProdService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProdService.Controllers
{
    public class HomeController : Controller
    {
        public ProdServiceContext db;
        public HomeController(ProdServiceContext ctx)
        {
            db = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public long AddNewDish([FromBody] DishCart dishCart)
        {
            if (dishCart.Id == 0)
            {
                db.DishCarts.Add(dishCart);
                db.SaveChanges();
                long id = db.DishCarts.OrderBy(i => i.Id).Last().Id;
                return id;
            }
            return dishCart.Id;
        }

        [HttpGet]
        public async Task<JsonResult> GetTechnologicCarts(long dishCartId)
        {
            return Json(await db.ProductDishCartJunctions.Where(p => p.DishCartId == dishCartId).Select(p => new { Id = p.Id, ProductId = p.ProductId, ProcessingId = p.ProcessingId, WeightBrutto = p.WeightBrutto }).ToListAsync());
        }


        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            return Json(await db.Products.Select(p => new { Id = p.Id, Name = p.Name }).ToListAsync());
        }

        [HttpGet]
        public async Task<JsonResult> GetProcessings()
        {
            return Json(await db.Processings.ToListAsync());
        }

        [HttpGet]
        public async Task<JsonResult> GetDishCart(long id)
        {
            List<ProductDishCartJunction> productDishCarts = await db.ProductDishCartJunctions
                .Include(p => p.DishCart)
                .Include(p => p.Processing)
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductDishCarts)
                .Where(p => p.DishCartId == id)
                .OrderBy(p => p.ProductId)
                .ToListAsync();

            Dictionary<string, double> dict = new Dictionary<string, double>();

            ProductDishUsefulProperty obj = new ProductDishUsefulProperty();
            obj.DishCartId = productDishCarts.FirstOrDefault().DishCartId;
            obj.DishCartName = productDishCarts.FirstOrDefault().DishCart.Name;
            Dictionary<long, ProductNettoInDish> dictionary = new Dictionary<long, ProductNettoInDish>();
            for (int i = 0; i < productDishCarts.Count(); i++)
            {
                dictionary[i] = new ProductNettoInDish
                {
                    Id = productDishCarts[i].ProductId,
                    ProductName = productDishCarts[i].Product.Name,
                    ProcessingName = productDishCarts[i].Processing.Name,
                    ProductWeightBrutto = productDishCarts[i].WeightBrutto,
                    ProductWeightNetto = Math.Round(ProductDishCartJunction.getNetto(productDishCarts[i]), 1)
                };
                dict = productDishCarts[i].DishCart.getUsefulPropOfTheDish(productDishCarts[i].DishCartId);
            }
            obj.productNettoInDishes = dictionary;
            obj.Squirrels = Math.Round(dict["Squirrels"], 1);
            obj.Fats = Math.Round(dict["Fats"], 1);
            obj.Сarbohydrates = Math.Round(dict["Сarbohydrates"], 1);
            obj.KCal = Math.Round(dict["KCal"], 1);

            return Json(obj);
        }


        [HttpGet]
        public async Task<JsonResult> GetMenus(DateTime? date1, DateTime? date2)
        {
            List<MenuItem> menu = await db.MenuItems
                .Where(d => d.Date >= date1 && d.Date <= date2)
                .Include(p => p.DishCartMenuItemJunctions)
                    .ThenInclude(m => m.Meal)
                .Include(p => p.DishCartMenuItemJunctions)
                    .ThenInclude(p => p.DishCart)
                        .ThenInclude(p => p.ProductDishCarts)
                .OrderBy(p => p.Date)
                .ToListAsync();

            List<MenuHelperClass> listOfDict = new List<MenuHelperClass>();

            foreach (var m in menu)
            {
                MenuHelperClass menuHelper = new MenuHelperClass();
                Dictionary<long, double> dictCountProdPerDay = new Dictionary<long, double>();
                List<MealDishProd> mealDishProds = new List<MealDishProd>();
                menuHelper.MenuItemDate = m.Date;
                menuHelper.MenuItemId = m.Id;

                foreach (var mn in m.DishCartMenuItemJunctions.Where(p => p.MenuItemId == m.Id))
                {
                    Prod prod = new Prod { DishName = mn.DishCart.Name, ProdCount = mn.DishCart.GetBruttoProductInDish(mn.DishCartId) };
                    List<Prod> prods = new List<Prod>();
                    prods.Add(prod);
                    MealDishProd mealDishProd = new MealDishProd();
                    mealDishProd.MealId = mn.MealId;
                    mealDishProd.MealName = mn.Meal.Name;
                    mealDishProd.Prods = prods;
                    mealDishProds.Add(mealDishProd);
                    foreach (var a in mn.DishCart.GetBruttoCountPerProductOnADay(m.Id))
                    {
                        dictCountProdPerDay[a.Key] = a.Value;
                    }
                }

                menuHelper.MealDishProd = mealDishProds.OrderBy(p => p.MealId).ToList();
                menuHelper.ProductsPerDayCount = dictCountProdPerDay;
                listOfDict.Add(menuHelper);
            };

            return Json(listOfDict);
        }

       




        [HttpGet]
        public async Task<JsonResult> GetDishCartMenuItems(long menuItemId)
        {
            return Json(await db.DishCartMenuItemJunctions.Where(m => m.MenuItemId == menuItemId).OrderBy(m => m.MealId).ToListAsync());
        }
    }
}


