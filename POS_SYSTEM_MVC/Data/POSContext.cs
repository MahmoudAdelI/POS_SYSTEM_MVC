using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Models;
using static POS_SYSTEM_MVC.Constants.Enums;

namespace POS_SYSTEM_MVC.Data
{
    public class POSContext(DbContextOptions<POSContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<VariantAttribute> VariantAttributes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleLine> SaleLines { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Rename tables
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            #endregion

            #region Composite Primary Keys
            builder.Entity<VariantAttribute>().HasKey(e => new { e.ProductVariantId, e.AttributeValueId });
            builder.Entity<SaleLine>().HasKey(e => new { e.SaleId, e.ProductVariantId });
            #endregion

            #region turning off cascade
            builder.Entity<VariantAttribute>()
                .HasOne(e => e.ProductVariant)
                .WithMany(e => e.VariantAttributes)
                .HasForeignKey(e => e.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VariantAttribute>()
                .HasOne(e => e.AttributeValue)
                .WithMany(e => e.VariantAttributes)
                .HasForeignKey(e => e.AttributeValueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SaleLine>()
                .HasOne(e => e.Sale)
                .WithMany(e => e.SaleLines)
                .HasForeignKey(e => e.SaleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SaleLine>()
                .HasOne(e => e.ProductVariant)
                .WithMany(e => e.SaleLines)
                .HasForeignKey(e => e.ProductVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sale>()
                .HasOne(s => s.Cashier)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.CashierId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Unique indexes
            builder.Entity<ProductVariant>().HasIndex(e => e.SKU).IsUnique();
            builder.Entity<ProductAttributeValue>()
                .HasIndex(e => new { e.SubCategoryId, e.AttributeId, e.Value })
                .IsUnique();
            #endregion

            #region Store enums as strings
            builder.Entity<Discount>().Property(e => e.Type).HasConversion<string>();
            builder.Entity<Sale>().Property(e => e.Status).HasConversion<string>();
            builder.Entity<Sale>().Property(e => e.DiscountType).HasConversion<string>();
            builder.Entity<SaleLine>().Property(e => e.DiscountType).HasConversion<string>();
            #endregion

            #region Data seeding
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Footwear" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Accessories" }
            );

            builder.Entity<SubCategory>().HasData(
                new SubCategory { Id = 1, Name = "Sneakers", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Sandals", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "T-Shirts", CategoryId = 2 },
                new SubCategory { Id = 4, Name = "Jackets", CategoryId = 2 },
                new SubCategory { Id = 5, Name = "Watches", CategoryId = 3 },
                new SubCategory { Id = 6, Name = "Bags", CategoryId = 3 }
            );

            builder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Nike" },
                new Brand { Id = 2, Name = "Adidas" },
                new Brand { Id = 3, Name = "Casio" }
            );

            builder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "Piece" },
                new Unit { Id = 2, Name = "Pair" }
            );

            builder.Entity<ProductAttribute>().HasData(
                new ProductAttribute { Id = 1, Name = "Color" },
                new ProductAttribute { Id = 2, Name = "Size" }
            );

            builder.Entity<ProductAttributeValue>().HasData(
                // Color ? Sneakers
                new ProductAttributeValue { Id = 1, AttributeId = 1, SubCategoryId = 1, Value = "Red" },
                new ProductAttributeValue { Id = 2, AttributeId = 1, SubCategoryId = 1, Value = "Blue" },
                new ProductAttributeValue { Id = 3, AttributeId = 1, SubCategoryId = 1, Value = "Black" },
                // Size ? Sneakers (EU)
                new ProductAttributeValue { Id = 4, AttributeId = 2, SubCategoryId = 1, Value = "40" },
                new ProductAttributeValue { Id = 5, AttributeId = 2, SubCategoryId = 1, Value = "41" },
                new ProductAttributeValue { Id = 6, AttributeId = 2, SubCategoryId = 1, Value = "42" },
                new ProductAttributeValue { Id = 7, AttributeId = 2, SubCategoryId = 1, Value = "43" },
                // Color ? T-Shirts
                new ProductAttributeValue { Id = 8, AttributeId = 1, SubCategoryId = 3, Value = "White" },
                new ProductAttributeValue { Id = 9, AttributeId = 1, SubCategoryId = 3, Value = "Black" },
                new ProductAttributeValue { Id = 10, AttributeId = 1, SubCategoryId = 3, Value = "Gray" },
                // Size ? T-Shirts (letter)
                new ProductAttributeValue { Id = 11, AttributeId = 2, SubCategoryId = 3, Value = "S" },
                new ProductAttributeValue { Id = 12, AttributeId = 2, SubCategoryId = 3, Value = "M" },
                new ProductAttributeValue { Id = 13, AttributeId = 2, SubCategoryId = 3, Value = "L" },
                new ProductAttributeValue { Id = 14, AttributeId = 2, SubCategoryId = 3, Value = "XL" },
                // Color ? Watches
                new ProductAttributeValue { Id = 15, AttributeId = 1, SubCategoryId = 5, Value = "Silver" },
                new ProductAttributeValue { Id = 16, AttributeId = 1, SubCategoryId = 5, Value = "Gold" },
                new ProductAttributeValue { Id = 17, AttributeId = 1, SubCategoryId = 5, Value = "Black" }
            );

            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Air Max 90", ImageUrl = "/images/air-max-90.webp", UnitId = 2, BrandId = 1, SubCategoryId = 1, BasePrice = 120.00m },
                new Product { Id = 2, Name = "Stan Smith", ImageUrl = "/images/stan-smith.webp", UnitId = 2, BrandId = 2, SubCategoryId = 1, BasePrice = 90.00m },
                new Product { Id = 3, Name = "Classic Tee", ImageUrl = "/images/classic-tee.webp", UnitId = 1, BrandId = 2, SubCategoryId = 3, BasePrice = 25.00m },
                new Product { Id = 4, Name = "G-Shock DW", ImageUrl = "/images/g-shock-dw.webp", UnitId = 1, BrandId = 3, SubCategoryId = 5, BasePrice = 75.00m }
            );

            builder.Entity<ProductVariant>().HasData(
                // Air Max 90 � Red/41, Red/42, Blue/41
                new ProductVariant { Id = 1, ProductId = 1, UnitPrice = 130.00m, StockQuantity = 10, SKU = "AM90-RED-41" },
                new ProductVariant { Id = 2, ProductId = 1, UnitPrice = 130.00m, StockQuantity = 8, SKU = "AM90-RED-42" },
                new ProductVariant { Id = 3, ProductId = 1, UnitPrice = 130.00m, StockQuantity = 5, SKU = "AM90-BLU-41" },
                // Classic Tee � White/M, Black/L
                new ProductVariant { Id = 4, ProductId = 3, UnitPrice = 28.00m, StockQuantity = 20, SKU = "TEE-WHT-M" },
                new ProductVariant { Id = 5, ProductId = 3, UnitPrice = 28.00m, StockQuantity = 15, SKU = "TEE-BLK-L" },
                // G-Shock � Black
                new ProductVariant { Id = 6, ProductId = 4, UnitPrice = 80.00m, StockQuantity = 7, SKU = "GSH-BLK" }
            );

            builder.Entity<VariantAttribute>().HasData(
                // AM90-RED-41 ? Color=Red(1), Size=41(5)
                new VariantAttribute { ProductVariantId = 1, AttributeValueId = 1 },
                new VariantAttribute { ProductVariantId = 1, AttributeValueId = 5 },
                // AM90-RED-42 ? Color=Red(1), Size=42(6)
                new VariantAttribute { ProductVariantId = 2, AttributeValueId = 1 },
                new VariantAttribute { ProductVariantId = 2, AttributeValueId = 6 },
                // AM90-BLU-41 ? Color=Blue(2), Size=41(5)
                new VariantAttribute { ProductVariantId = 3, AttributeValueId = 2 },
                new VariantAttribute { ProductVariantId = 3, AttributeValueId = 5 },
                // TEE-WHT-M ? Color=White(8), Size=M(12)
                new VariantAttribute { ProductVariantId = 4, AttributeValueId = 8 },
                new VariantAttribute { ProductVariantId = 4, AttributeValueId = 12 },
                // TEE-BLK-L ? Color=Black(9), Size=L(13)
                new VariantAttribute { ProductVariantId = 5, AttributeValueId = 9 },
                new VariantAttribute { ProductVariantId = 5, AttributeValueId = 13 },
                // GSH-BLK ? Color=Black(17)
                new VariantAttribute { ProductVariantId = 6, AttributeValueId = 17 }
            );

            builder.Entity<Discount>().HasData(
                // 10% off Air Max 90 variant 1
                new Discount { Id = 1, ProductVariantId = 1, Type = DiscountTypeENUM.Percentage, Value = 10, IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                // 5.00 fixed off Classic Tee variant 4
                new Discount { Id = 2, ProductVariantId = 4, Type = DiscountTypeENUM.Fixed, Value = 5, IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
                // Sale-level: 15% off when total >= 200
                new Discount { Id = 3, SaleTotalThreshold = 200, Type = DiscountTypeENUM.Percentage, Value = 15, IsActive = true, CreatedAt = new DateTime(2026, 1, 1) }
            );
            #endregion
        }
    }
}
