using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POS_SYSTEM_MVC.Migrations
{
    /// <inheritdoc />
    public partial class dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_ProductVariant_ProductVariantId",
                table: "Discount");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Product_ProductId",
                table: "Discount");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubCategory_SubCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Unit_UnitId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValue_ProductAttribute_AttributeId",
                table: "ProductAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValue_SubCategory_SubCategoryId",
                table: "ProductAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Product_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_users_CashierId",
                table: "Sale");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleLine_ProductVariant_ProductVariantId",
                table: "SaleLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleLine_Sale_SaleId",
                table: "SaleLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryAttribute_ProductAttribute_AttributeId",
                table: "SubCategoryAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryAttribute_SubCategory_SubCategoryId",
                table: "SubCategoryAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantAttribute_ProductAttributeValue_AttributeValueId",
                table: "VariantAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantAttribute_ProductVariant_ProductVariantId",
                table: "VariantAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantAttribute",
                table: "VariantAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategoryAttribute",
                table: "SubCategoryAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleLine",
                table: "SaleLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeValue",
                table: "ProductAttributeValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttribute",
                table: "ProductAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "VariantAttribute",
                newName: "VariantAttributes");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "SubCategoryAttribute",
                newName: "SubCategoryAttributes");

            migrationBuilder.RenameTable(
                name: "SubCategory",
                newName: "SubCategories");

            migrationBuilder.RenameTable(
                name: "SaleLine",
                newName: "SaleLines");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sales");

            migrationBuilder.RenameTable(
                name: "ProductVariant",
                newName: "ProductVariants");

            migrationBuilder.RenameTable(
                name: "ProductAttributeValue",
                newName: "ProductAttributeValues");

            migrationBuilder.RenameTable(
                name: "ProductAttribute",
                newName: "ProductAttributes");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_VariantAttribute_AttributeValueId",
                table: "VariantAttributes",
                newName: "IX_VariantAttributes_AttributeValueId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategoryAttribute_AttributeId",
                table: "SubCategoryAttributes",
                newName: "IX_SubCategoryAttributes_AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategories",
                newName: "IX_SubCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleLine_ProductVariantId",
                table: "SaleLines",
                newName: "IX_SaleLines_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Sale_CashierId",
                table: "Sales",
                newName: "IX_Sales_CashierId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_SKU",
                table: "ProductVariants",
                newName: "IX_ProductVariants_SKU");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeValue_SubCategoryId_AttributeId_Value",
                table: "ProductAttributeValues",
                newName: "IX_ProductAttributeValues_SubCategoryId_AttributeId_Value");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeValue_AttributeId",
                table: "ProductAttributeValues",
                newName: "IX_ProductAttributeValues_AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UnitId",
                table: "Products",
                newName: "IX_Products_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_SubCategoryId",
                table: "Products",
                newName: "IX_Products_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Discount_ProductVariantId",
                table: "Discounts",
                newName: "IX_Discounts_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Discount_ProductId",
                table: "Discounts",
                newName: "IX_Discounts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantAttributes",
                table: "VariantAttributes",
                columns: new[] { "ProductVariantId", "AttributeValueId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategoryAttributes",
                table: "SubCategoryAttributes",
                columns: new[] { "SubCategoryId", "AttributeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleLines",
                table: "SaleLines",
                columns: new[] { "SaleId", "ProductVariantId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nike" },
                    { 2, "Adidas" },
                    { 3, "Casio" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Footwear" },
                    { 2, "Clothing" },
                    { 3, "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "ExpiresAt", "IsActive", "ProductId", "ProductVariantId", "SaleTotalThreshold", "Type", "Value" },
                values: new object[] { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, null, 200m, "Percentage", 15m });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Color" },
                    { 2, "Size" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Piece" },
                    { 2, "Pair" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Sneakers" },
                    { 2, 1, "Sandals" },
                    { 3, 2, "T-Shirts" },
                    { 4, 2, "Jackets" },
                    { 5, 3, "Watches" },
                    { 6, 3, "Bags" }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributeValues",
                columns: new[] { "Id", "AttributeId", "SubCategoryId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "Red" },
                    { 2, 1, 1, "Blue" },
                    { 3, 1, 1, "Black" },
                    { 4, 2, 1, "40" },
                    { 5, 2, 1, "41" },
                    { 6, 2, 1, "42" },
                    { 7, 2, 1, "43" },
                    { 8, 1, 3, "White" },
                    { 9, 1, 3, "Black" },
                    { 10, 1, 3, "Gray" },
                    { 11, 2, 3, "S" },
                    { 12, 2, 3, "M" },
                    { 13, 2, 3, "L" },
                    { 14, 2, 3, "XL" },
                    { 15, 1, 5, "Silver" },
                    { 16, 1, 5, "Gold" },
                    { 17, 1, 5, "Black" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "BrandId", "Name", "SubCategoryId", "UnitId" },
                values: new object[,]
                {
                    { 1, 120.00m, 1, "Air Max 90", 1, 2 },
                    { 2, 90.00m, 2, "Stan Smith", 1, 2 },
                    { 3, 25.00m, 2, "Classic Tee", 3, 1 },
                    { 4, 75.00m, 3, "G-Shock DW", 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "SubCategoryAttributes",
                columns: new[] { "AttributeId", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 1, 5 },
                    { 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "ProductId", "SKU", "StockQuantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, "AM90-RED-41", 10, 130.00m },
                    { 2, 1, "AM90-RED-42", 8, 130.00m },
                    { 3, 1, "AM90-BLU-41", 5, 130.00m },
                    { 4, 3, "TEE-WHT-M", 20, 28.00m },
                    { 5, 3, "TEE-BLK-L", 15, 28.00m },
                    { 6, 4, "GSH-BLK", 7, 80.00m }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedAt", "ExpiresAt", "IsActive", "ProductId", "ProductVariantId", "SaleTotalThreshold", "Type", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, 1, null, "Percentage", 10m },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, null, 4, null, "Fixed", 5m }
                });

            migrationBuilder.InsertData(
                table: "VariantAttributes",
                columns: new[] { "AttributeValueId", "ProductVariantId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 5, 1 },
                    { 1, 2 },
                    { 6, 2 },
                    { 2, 3 },
                    { 5, 3 },
                    { 8, 4 },
                    { 12, 4 },
                    { 9, 5 },
                    { 13, 5 },
                    { 17, 6 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_ProductVariants_ProductVariantId",
                table: "Discounts",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Products_ProductId",
                table: "Discounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributes_AttributeId",
                table: "ProductAttributeValues",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_SubCategories_SubCategoryId",
                table: "ProductAttributeValues",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLines_ProductVariants_ProductVariantId",
                table: "SaleLines",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLines_Sales_SaleId",
                table: "SaleLines",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_users_CashierId",
                table: "Sales",
                column: "CashierId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryAttributes_ProductAttributes_AttributeId",
                table: "SubCategoryAttributes",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryAttributes_SubCategories_SubCategoryId",
                table: "SubCategoryAttributes",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantAttributes_ProductAttributeValues_AttributeValueId",
                table: "VariantAttributes",
                column: "AttributeValueId",
                principalTable: "ProductAttributeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantAttributes_ProductVariants_ProductVariantId",
                table: "VariantAttributes",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_ProductVariants_ProductVariantId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Products_ProductId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributes_AttributeId",
                table: "ProductAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_SubCategories_SubCategoryId",
                table: "ProductAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleLines_ProductVariants_ProductVariantId",
                table: "SaleLines");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleLines_Sales_SaleId",
                table: "SaleLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_users_CashierId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryAttributes_ProductAttributes_AttributeId",
                table: "SubCategoryAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryAttributes_SubCategories_SubCategoryId",
                table: "SubCategoryAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantAttributes_ProductAttributeValues_AttributeValueId",
                table: "VariantAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantAttributes_ProductVariants_ProductVariantId",
                table: "VariantAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantAttributes",
                table: "VariantAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategoryAttributes",
                table: "SubCategoryAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleLines",
                table: "SaleLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributeValues",
                table: "ProductAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttributes",
                table: "ProductAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "SubCategoryAttributes",
                keyColumns: new[] { "AttributeId", "SubCategoryId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 13, 5 });

            migrationBuilder.DeleteData(
                table: "VariantAttributes",
                keyColumns: new[] { "AttributeValueId", "ProductVariantId" },
                keyValues: new object[] { 17, 6 });

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "VariantAttributes",
                newName: "VariantAttribute");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "SubCategoryAttributes",
                newName: "SubCategoryAttribute");

            migrationBuilder.RenameTable(
                name: "SubCategories",
                newName: "SubCategory");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Sale");

            migrationBuilder.RenameTable(
                name: "SaleLines",
                newName: "SaleLine");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductAttributeValues",
                newName: "ProductAttributeValue");

            migrationBuilder.RenameTable(
                name: "ProductAttributes",
                newName: "ProductAttribute");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_VariantAttributes_AttributeValueId",
                table: "VariantAttribute",
                newName: "IX_VariantAttribute_AttributeValueId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategoryAttributes_AttributeId",
                table: "SubCategoryAttribute",
                newName: "IX_SubCategoryAttribute_AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategory",
                newName: "IX_SubCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CashierId",
                table: "Sale",
                newName: "IX_Sale_CashierId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleLines_ProductVariantId",
                table: "SaleLine",
                newName: "IX_SaleLine_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_SKU",
                table: "ProductVariant",
                newName: "IX_ProductVariant_SKU");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UnitId",
                table: "Product",
                newName: "IX_Product_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubCategoryId",
                table: "Product",
                newName: "IX_Product_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeValues_SubCategoryId_AttributeId_Value",
                table: "ProductAttributeValue",
                newName: "IX_ProductAttributeValue_SubCategoryId_AttributeId_Value");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeValues_AttributeId",
                table: "ProductAttributeValue",
                newName: "IX_ProductAttributeValue_AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_ProductVariantId",
                table: "Discount",
                newName: "IX_Discount_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Discounts_ProductId",
                table: "Discount",
                newName: "IX_Discount_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantAttribute",
                table: "VariantAttribute",
                columns: new[] { "ProductVariantId", "AttributeValueId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategoryAttribute",
                table: "SubCategoryAttribute",
                columns: new[] { "SubCategoryId", "AttributeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleLine",
                table: "SaleLine",
                columns: new[] { "SaleId", "ProductVariantId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttributeValue",
                table: "ProductAttributeValue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttribute",
                table: "ProductAttribute",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_ProductVariant_ProductVariantId",
                table: "Discount",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Product_ProductId",
                table: "Discount",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubCategory_SubCategoryId",
                table: "Product",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Unit_UnitId",
                table: "Product",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValue_ProductAttribute_AttributeId",
                table: "ProductAttributeValue",
                column: "AttributeId",
                principalTable: "ProductAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValue_SubCategory_SubCategoryId",
                table: "ProductAttributeValue",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Product_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_users_CashierId",
                table: "Sale",
                column: "CashierId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLine_ProductVariant_ProductVariantId",
                table: "SaleLine",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLine_Sale_SaleId",
                table: "SaleLine",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryAttribute_ProductAttribute_AttributeId",
                table: "SubCategoryAttribute",
                column: "AttributeId",
                principalTable: "ProductAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryAttribute_SubCategory_SubCategoryId",
                table: "SubCategoryAttribute",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantAttribute_ProductAttributeValue_AttributeValueId",
                table: "VariantAttribute",
                column: "AttributeValueId",
                principalTable: "ProductAttributeValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantAttribute_ProductVariant_ProductVariantId",
                table: "VariantAttribute",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
