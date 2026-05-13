using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Services.Brands;
using POS_SYSTEM_MVC.Services.CategoryServices;
using POS_SYSTEM_MVC.Services.ProductServices;
using POS_SYSTEM_MVC.Services.SubCategoriesServices;
using POS_SYSTEM_MVC.Services.Unitservices;
using POS_SYSTEM_MVC.Services.UnitServices;
using POS_SYSTEM_MVC.UnitOfWork;
using POS_SYSTEM_MVC.Services.DashboardServices;
using POS_SYSTEM_MVC.Services.InventoryServices;
using POS_SYSTEM_MVC.Services.SalesHistoryServices;
using POS_SYSTEM_MVC.Services.AttributeServices;
using POS_SYSTEM_MVC.Services.DiscountServices;

namespace POS_SYSTEM_MVC
{
    public class Program
    {
        public static async Task Main(string[] args) // Change to async Task Main
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register DbContext
            builder.Services.AddDbContext<POSContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //------------

            // Register Identity with roles
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<POSContext>()
                .AddDefaultTokenProviders();
            //------------

            // To read RequestVerificationToken from header
            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "RequestVerificationToken";
            });
            //------------

            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<ISalesHistoryService, SalesHistoryService>();
            builder.Services.AddScoped<IAttributeService, AttributeService>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";

                options.AccessDeniedPath = "/Account/AccessDenied";

                options.ExpireTimeSpan = TimeSpan.FromDays(7);

                options.SlidingExpiration = true;
            });
            var app = builder.Build();

            await DatabaseSeeder.SeedAsync(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "cashier",
                pattern: "{controller=Cashier}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();
            app.Run();

        }
    }
}
