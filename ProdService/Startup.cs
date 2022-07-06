using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProdService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdService
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            string conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ProdServiceContext>(options => options.UseSqlServer(conString));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductGroupRepository, ProductGroupRepository>(); 
            services.AddTransient<IMealRepository, MealRepository>();
            services.AddTransient<IProcessingRepository, ProcessingRepository>();
            services.AddTransient<IDishCartRepository, DishCartRepository>();
            services.AddTransient<IProductDishCartJunctionRepository, ProductDishCartJunctionRepository>();
            services.AddTransient<IDishCartMenuItemJunctionRepository, DishCartMenuItemJunctionRepository>();
            services.AddTransient<IMenuItemRepository, MenuItemRepository>();    
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
