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

         

            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";

                options.AccessDeniedPath = "/Account/AccessDenied";

                options.ExpireTimeSpan = TimeSpan.FromDays(7);

                options.SlidingExpiration = true;
            });
            var app = builder.Build();



            #region seed users + roles
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // seed roles
                string[] roles = { Role.Admin, Role.Cashier };

                foreach (var role in roles)
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));

                // seed admin user
                if (await userManager.FindByNameAsync("admin") == null)
                {
                    var admin = new ApplicationUser
                    {
                        UserName = "admin",
                        FirstName = "Admin",
                        LastName = "User"
                    };
                    var result = await userManager.CreateAsync(admin, "Admin123!");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(admin, Role.Admin);
                    else
                        foreach (var error in result.Errors)
                            Console.WriteLine($" {error.Description}");
                }

                // seed cashier user
                if (await userManager.FindByNameAsync("cashier") == null)
                {
                    var cashier = new ApplicationUser
                    {
                        UserName = "cashier",
                        FirstName = "Cashier",
                        LastName = "User"
                    };
                    var result = await userManager.CreateAsync(cashier, "Cashier123!");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(cashier, Role.Cashier);
                    else
                        foreach (var error in result.Errors)
                            Console.WriteLine($" {error.Description}");
                }
            }
            #endregion


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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
