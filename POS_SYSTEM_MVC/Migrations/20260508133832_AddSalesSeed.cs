using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POS_SYSTEM_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "CashierId", "CreatedAt", "DiscountAmount", "DiscountType", "DiscountValue", "Status" },
                values: new object[,]
                {
                    { 1, "c51c4f1e-4ab2-41bc-bf77-266ebdbd88b2", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Completed" },
                    { 2, "c51c4f1e-4ab2-41bc-bf77-266ebdbd88b2", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Completed" },
                    { 3, "c51c4f1e-4ab2-41bc-bf77-266ebdbd88b2", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.50m, "Percentage", 15m, "Completed" },
                    { 4, "c51c4f1e-4ab2-41bc-bf77-266ebdbd88b2", new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Completed" },
                    { 5, "c51c4f1e-4ab2-41bc-bf77-266ebdbd88b2", new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Canceled" }
                });

            migrationBuilder.InsertData(
                table: "SaleLines",
                columns: new[] { "ProductVariantId", "SaleId", "DiscountAmount", "DiscountType", "DiscountValue", "OriginalUnitPrice", "Quantity" },
                values: new object[,]
                {
                    { 4, 1, 5.00m, "Fixed", 5m, 28.00m, 2 },
                    { 1, 2, 13.00m, "Percentage", 10m, 130.00m, 1 },
                    { 3, 3, null, null, null, 130.00m, 1 },
                    { 5, 3, null, null, null, 28.00m, 1 },
                    { 6, 4, null, null, null, 80.00m, 2 },
                    { 2, 5, null, null, null, 130.00m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SaleLines",
                keyColumns: new[] { "ProductVariantId", "SaleId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "SaleLines",
                keyColumns: new[] { "ProductVariantId", "SaleId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SaleLines",
                keyColumns: new[] { "ProductVariantId", "SaleId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "SaleLines",
                keyColumns: new[] { "ProductVariantId", "SaleId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "SaleLines",
                keyColumns: new[] { "ProductVariantId", "SaleId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "SaleLines",
                keyColumns: new[] { "ProductVariantId", "SaleId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
