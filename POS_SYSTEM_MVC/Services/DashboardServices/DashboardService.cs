using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.DTOs.Dashboard;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Services.UnitServices;
using POS_SYSTEM_MVC.UnitOfWork;
using System;

namespace POS_SYSTEM_MVC.Services.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _uow;

        public DashboardService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            // ---- جيبي كل الـ Sales مع SaleLines والـ ProductVariant ----
            var allSales = await _uow.Sales.GetAllAsync(
                s => s.SaleLines
            );

            var completedSales = allSales
                .Where(s => s.Status == SaleStatus.Completed)
                .ToList();

            
            // 1. Total Sales (مجموع المبيعات)
           
            var totalSales = completedSales
                .Sum(s =>
                    s.SaleLines.Sum(sl => sl.OriginalUnitPrice * sl.Quantity
                    - (sl.DiscountAmount ?? 0))
                    - (s.DiscountAmount ?? 0)
                );

            
            // 2. Total Orders (عدد الفواتير)
           
            var totalOrders = completedSales.Count;

            
            // 3. Total Products
            
            var totalProducts = _uow.Products.Count();

            
            // 4. Growth (نسب النمو)
            //    بنقارن الشهر الحالي بالشهر اللي فاته
            
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfLastMonth = startOfMonth.AddMonths(-1);

            // مبيعات الشهر الحالي
            var thisMonthSales = completedSales
                .Where(s => s.CreatedAt >= startOfMonth)
                .Sum(s => s.SaleLines.Sum(sl =>
                    sl.OriginalUnitPrice * sl.Quantity
                    - (sl.DiscountAmount ?? 0))
                    - (s.DiscountAmount ?? 0)
                );

            // مبيعات الشهر اللي فات
            var lastMonthSales = completedSales
                .Where(s => s.CreatedAt >= startOfLastMonth
                         && s.CreatedAt < startOfMonth)
                .Sum(s => s.SaleLines.Sum(sl =>
                    sl.OriginalUnitPrice * sl.Quantity
                    - (sl.DiscountAmount ?? 0))
                    - (s.DiscountAmount ?? 0)
                );

            // أوردرز الشهر الحالي
            var thisMonthOrders = completedSales
                .Count(s => s.CreatedAt >= startOfMonth);

            // أوردرز الشهر اللي فات
            var lastMonthOrders = completedSales
                .Count(s => s.CreatedAt >= startOfLastMonth
                         && s.CreatedAt < startOfMonth);

            // حساب نسبة النمو
            double salesGrowth = lastMonthSales == 0 ? 100 :
                (double)((thisMonthSales - lastMonthSales) / lastMonthSales * 100);

            double ordersGrowth = lastMonthOrders == 0 ? 100 :
                (double)(thisMonthOrders - lastMonthOrders) / lastMonthOrders * 100;

            
            // 5. Recent Sales (آخر 5 فواتير)
            
            var recentSales = completedSales
                .OrderByDescending(s => s.CreatedAt)
                .Take(5)
                .Select(s => new RecentSaleDto
                {
                    // أول منتج في الفاتورة
                    ProductName = s.SaleLines
                        .FirstOrDefault()?.ProductVariant?.Product?.Name
                        ?? "Unknown",

                    // مجموع الفاتورة
                    Amount = s.SaleLines.Sum(sl =>
                        sl.OriginalUnitPrice * sl.Quantity
                        - (sl.DiscountAmount ?? 0))
                        - (s.DiscountAmount ?? 0),

                    Status = s.Status.ToString(),
                    Date = s.CreatedAt
                })
                .ToList();

           
            // 6. Top Products (أكتر 5 منتجات)
            
            var topProducts = completedSales
                .SelectMany(s => s.SaleLines)
                .GroupBy(sl => sl.ProductVariant?.Product?.Name ?? "Unknown")
                .Select((g, index) => new TopProductDto
                {
                    ProductName = g.Key,
                    UnitsSold = g.Sum(sl => sl.Quantity),
                    TotalRevenue = g.Sum(sl =>
                        sl.OriginalUnitPrice * sl.Quantity
                        - (sl.DiscountAmount ?? 0))
                })
                .OrderByDescending(x => x.UnitsSold)
                .Take(5)
                .ToList();

            // إضافة الـ Rank
            for (int i = 0; i < topProducts.Count; i++)
                topProducts[i].Rank = i + 1;

            return new DashboardDto
            {
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                TotalProducts = totalProducts,
                SalesGrowth = Math.Round(salesGrowth, 1),
                OrdersGrowth = Math.Round(ordersGrowth, 1),
                RecentSales = recentSales,
                TopProducts = topProducts
            };
        }
    }
}
