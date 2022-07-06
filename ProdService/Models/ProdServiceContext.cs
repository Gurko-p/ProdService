using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService.Models
{
    public class ProdServiceContext : DbContext
    {
        public ProdServiceContext(DbContextOptions<ProdServiceContext> opts) : base(opts) { }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductDishCartJunction> ProductDishCartJunctions { get; set; }
        public DbSet<DishCartMenuItemJunction> DishCartMenuItemJunctions { get; set; }
        public DbSet<DishCart> DishCarts { get; set; }
        public DbSet<Processing> Processings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().HasData(
                new Meal[]
                {
                new Meal { Id=1, Name="Завтрак"},
                new Meal { Id=2, Name="Обед"},
                new Meal { Id=3, Name="Ужин"}
                });

            modelBuilder.Entity<ProductGroup>().HasData(
                new ProductGroup[]
                {
                    new ProductGroup{ Id = 1, Name = "Мясная продукция"},
                    new ProductGroup{ Id = 2, Name = "Рыбная продукция"},
                    new ProductGroup{ Id = 3, Name = "Колбасные изделия"},
                    new ProductGroup{ Id = 4, Name = "Жиры"},
                    new ProductGroup{ Id = 5, Name = "Крупа"},
                    new ProductGroup{ Id = 6, Name = "Овощи"},
                    new ProductGroup{ Id = 7, Name = "Хлебобулочные изделия"},
                    new ProductGroup{ Id = 8, Name = "Консервные изделия"},
                    new ProductGroup{ Id = 9, Name = "Прочие"},
                });

            modelBuilder.Entity<Processing>().HasData(
                new Processing[]
                {
                    new Processing { Id=1, Name = "Без обработки", ReduceSquirrels = 0, ReduceFats = 0, ReduceСarbohydrates = 0 },
                    new Processing { Id=2, Name = "Очистка", ReduceSquirrels = 0, ReduceFats = 0, ReduceСarbohydrates = 0 },
                    new Processing { Id=3, Name = "Тепловая обработка", ReduceSquirrels = 0.06, ReduceFats = 0.12, ReduceСarbohydrates = 0.09 },
                    new Processing { Id=4, Name = "Очистка + Тепловая обработка", ReduceSquirrels = 0.06, ReduceFats = 0.12, ReduceСarbohydrates = 0.09 }
                });

            modelBuilder.Entity<Product>().HasData(
                new Product[]
                {
                    new Product { Id=1, Name = "Свинина", PeelRate = 0.245, HeatTreatmentRate = 0.32, Squirrels = 11.7, Fats = 49.3, Сarbohydrates = 0, NormPerDay = 150.0, KCal = 491, ProductGroupId = 1},
                    new Product { Id=2, Name = "Пшено", PeelRate = 0, HeatTreatmentRate = 4, Squirrels = 11.5, Fats = 3.3, Сarbohydrates = 66.5, NormPerDay = 100, KCal = 348, ProductGroupId = 5},
                    new Product { Id=3, Name = "Xлеб ржаной", PeelRate = 0, HeatTreatmentRate = 0, Squirrels = 7.7, Fats = 1.4, Сarbohydrates = 37.6, NormPerDay = 100, KCal = 210, ProductGroupId = 7},
                    new Product { Id=4, Name = "Лук", PeelRate = 0.16, HeatTreatmentRate = 0.5, Squirrels = 1.4, Fats = 0.2, Сarbohydrates = 8.2, NormPerDay = 30, KCal = 41, ProductGroupId = 6},
                    new Product { Id=5, Name = "Сосиски", PeelRate = 0.03, HeatTreatmentRate = 0.03, Squirrels = 10.1, Fats = 31.6, Сarbohydrates = 1.9, NormPerDay = 100, KCal = 270, ProductGroupId = 3},
                    new Product { Id=6, Name = "Масло сливочное", PeelRate = 0, HeatTreatmentRate = 0, Squirrels = 0.7, Fats = 78.0, Сarbohydrates = 1.0, NormPerDay = 30, KCal = 709, ProductGroupId = 4},
                });

            modelBuilder.Entity<DishCart>().HasData(
                new DishCart[]
                {
                    new DishCart { Id=1, Name = "Сосиска отварная, каша пшеная вязкая"},
                    new DishCart { Id=2, Name = "Лук, хлеб ржаной"}
                });
            modelBuilder.Entity<ProductDishCartJunction>().HasData(
                new ProductDishCartJunction[]
                {
                    new ProductDishCartJunction { Id=1, ProductId = 2, DishCartId = 1, ProcessingId = 2, WeightBrutto = 80.0},
                    new ProductDishCartJunction { Id=2, ProductId = 5, DishCartId = 1, ProcessingId = 2, WeightBrutto = 80.0},
                    new ProductDishCartJunction { Id=3, ProductId = 6, DishCartId = 1, ProcessingId = 1, WeightBrutto = 5.0},
                    new ProductDishCartJunction { Id=4, ProductId = 4, DishCartId = 2, ProcessingId = 2, WeightBrutto = 20.0}, // лук 20 г белки - 0.2688 г, жиры - , углеводы - 1.45, ккал - 7.38 
                    new ProductDishCartJunction { Id=5, ProductId = 3, DishCartId = 2, ProcessingId = 1, WeightBrutto = 100.0} // хлеб 100 г белки - 7.7, жиры - , углеводы - 37.6, ккал - 210
                });

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem[]
                {
                    new MenuItem { Id = 1, Date = new DateTime(2022, 4, 21)},
                    new MenuItem { Id = 2, Date = new DateTime(2022, 4, 22)}

                });

            modelBuilder.Entity<DishCartMenuItemJunction>().HasData(
                new DishCartMenuItemJunction[]
                {
                    new DishCartMenuItemJunction { Id = 1, DishCartId = 1, MenuItemId = 1, MealId = 1}, // 21.04.2022 завтрак Сосиска отварная, каша пшеная вязкая
                    new DishCartMenuItemJunction { Id = 2, DishCartId = 1, MenuItemId = 1, MealId = 1}, // 21.04.2022 завтрак Сосиска отварная, каша пшеная вязкая
                    new DishCartMenuItemJunction { Id = 3, DishCartId = 2, MenuItemId = 1, MealId = 2}, // 21.04.2022 обед Лук, хлеб ржаной
                    new DishCartMenuItemJunction { Id = 4, DishCartId = 2, MenuItemId = 1, MealId = 2}, // 21.04.2022 обед Лук, хлеб ржаной
                    new DishCartMenuItemJunction { Id = 5, DishCartId = 2, MenuItemId = 1, MealId = 2}, // 21.04.2022 обед Лук, хлеб ржаной
                    new DishCartMenuItemJunction { Id = 6, DishCartId = 2, MenuItemId = 1, MealId = 3}, // 21.04.2022 ужин Лук, хлеб ржаной
                    new DishCartMenuItemJunction { Id = 7, DishCartId = 1, MenuItemId = 1, MealId = 1}, // 21.04.2022 завтрак Сосиска отварная, каша пшеная вязкая
                    new DishCartMenuItemJunction { Id = 8, DishCartId = 1, MenuItemId = 2, MealId = 1} //  22.04.2022 завтрак Сосиска отварная, каша пшеная вязкая
                });

        }
    }
}
