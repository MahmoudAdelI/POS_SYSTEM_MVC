using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Servicies;
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

            builder.Services.AddScoped<IUnitOfWork, POS_SYSTEM_MVC.UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped<IUnitService, UnitService>();


            // Register Identity with roles
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<POSContext>()
                .AddDefaultTokenProviders();
            //------------

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
                    var result = await userManager.CreateAsync(admin, "password");
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
                    var result = await userManager.CreateAsync(cashier, "password");
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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
