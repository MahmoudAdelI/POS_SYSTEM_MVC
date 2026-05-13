using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Models;
using System;
using static POS_SYSTEM_MVC.Constants.Enums;

namespace POS_SYSTEM_MVC.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<POSContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
            await SeedSalesAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = [Role.Admin, Role.Cashier];

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByIdAsync(SeedConstants.AdminUserId) is null)
            {
                var admin = new ApplicationUser
                {
                    Id = SeedConstants.AdminUserId,
                    UserName = "admin",
                    Email = "admin@pos.com",
                    FirstName = "System",
                    LastName = "Admin",
                    HireDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Salary = 5000
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, Role.Admin);
                else
                    foreach (var error in result.Errors)
                        Console.WriteLine($"Admin seed error: {error.Description}");

            }

            if (await userManager.FindByIdAsync(SeedConstants.CashierUserId) is null)
            {
                var cashier = new ApplicationUser
                {
                    Id = SeedConstants.CashierUserId,
                    UserName = "cashier",
                    Email = "cashier@pos.com",
                    FirstName = "John",
                    LastName = "Doe",
                    HireDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Salary = 2000
                };

                var result = await userManager.CreateAsync(cashier, "Cashier123!");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(cashier, Role.Cashier);
                else
                    foreach (var error in result.Errors)
                        Console.WriteLine($"Cashier seed error: {error.Description}");
            }
        }

        private static async Task SeedSalesAsync(POSContext context)
        {
            if (await context.Sales.AnyAsync()) return;

            context.Sales.AddRange(
                // Sale 1 — completed, no discount
                new Sale
                {
                    //Id = 1,
                    CashierId = SeedConstants.CashierUserId,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    Status = SaleStatus.Completed,
                    DiscountType = null,
                    DiscountValue = null,
                    DiscountAmount = null,
                    SaleLines =
                    [
                        // AM90 Red/41 x2 = 260
                        new SaleLine
                        {
                            SaleId              = 1,
                            ProductVariantId    = 1,
                            Quantity            = 2,
                            OriginalUnitPrice   = 130.00m,
                            DiscountType        = null,
                            DiscountValue       = null,
                            DiscountAmount      = null,
                        },
                        // Classic Tee White/M x1 = 28
                        new SaleLine
                        {
                            SaleId              = 1,
                            ProductVariantId    = 4,
                            Quantity            = 1,
                            OriginalUnitPrice   = 28.00m,
                            DiscountType        = null,
                            DiscountValue       = null,
                            DiscountAmount      = null,
                        },
                    ]
                },

                // Sale 2 — canceled, no discount
                new Sale
                {
                    //Id = 2,
                    CashierId = SeedConstants.CashierUserId,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    Status = SaleStatus.Canceled,
                    DiscountType = null,
                    DiscountValue = null,
                    DiscountAmount = null,
                    SaleLines =
                    [
                        // G-Shock Black x1 = 80
                        new SaleLine
                        {
                            SaleId              = 2,
                            ProductVariantId    = 6,
                            Quantity            = 1,
                            OriginalUnitPrice   = 80.00m,
                            DiscountType        = null,
                            DiscountValue       = null,
                            DiscountAmount      = null,
                        },
                    ]
                },

                // Sale 3 — completed, 10% discount on a line item
                new Sale
                {
                    //Id = 3,
                    CashierId = SeedConstants.CashierUserId,
                    CreatedAt = DateTime.UtcNow,
                    Status = SaleStatus.Completed,
                    DiscountType = null,      // no sale-level discount
                    DiscountValue = null,
                    DiscountAmount = null,
                    SaleLines =
                    [
                        // AM90 Blue/41 x1 = 130, 10% off → discount 13, line total 117
                        new SaleLine
                        {
                            SaleId              = 3,
                            ProductVariantId    = 3,
                            Quantity            = 1,
                            OriginalUnitPrice   = 130.00m,
                            DiscountType        = DiscountTypeENUM.Percentage,
                            DiscountValue       = 10,
                            DiscountAmount      = 13.00m,
                        },
                        // Classic Tee Black/L x1 = 28, no discount
                        new SaleLine
                        {
                            SaleId              = 3,
                            ProductVariantId    = 5,
                            Quantity            = 1,
                            OriginalUnitPrice   = 28.00m,
                            DiscountType        = null,
                            DiscountValue       = null,
                            DiscountAmount      = null,
                        },
                        // G-Shock Black x1 = 80, no discount
                        new SaleLine
                        {
                            SaleId              = 3,
                            ProductVariantId    = 6,
                            Quantity            = 1,
                            OriginalUnitPrice   = 80.00m,
                            DiscountType        = null,
                            DiscountValue       = null,
                            DiscountAmount      = null,
                        },
                    ]
                }
            );

            await context.SaveChangesAsync();
        }
    }

}
