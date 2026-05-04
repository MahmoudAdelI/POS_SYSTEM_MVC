using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.Data
{
    public class POSContext(DbContextOptions<POSContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");

            // Composite Primary Keys
            builder.Entity<VariantAttribute>().HasKey(e => new { e.ProductVariantId, e.AttributeValueId});
            builder.Entity<SubCategoryAttribute>().HasKey(e => new { e.SubCategoryId, e.AttributeId });
            builder.Entity<SaleLine>().HasKey(e => new { e.SaleId, e.ProductVariantId});


            // turning off cascade
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

            builder.Entity<SubCategoryAttribute>()
                .HasOne(e => e.SubCategory)
                .WithMany(e => e.SubCategoryAttributes)
                .HasForeignKey(e => e.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SubCategoryAttribute>()
                .HasOne(e => e.Attribute)
                .WithMany(e => e.SubCategoryAttributes)
                .HasForeignKey(e => e.AttributeId)
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

            // Unique indexes
            builder.Entity<ProductVariant>().HasIndex(e => e.SKU).IsUnique();
            builder.Entity<ProductAttributeValue>()
                .HasIndex(e => new { e.SubCategoryId, e.AttributeId, e.Value })
                .IsUnique();

            // Enums stored as strings
            builder.Entity<Discount>().Property(e => e.Type).HasConversion<string>();
            builder.Entity<Sale>().Property(e => e.Status).HasConversion<string>();
            builder.Entity<Sale>().Property(e => e.DiscountType).HasConversion<string>();
            builder.Entity<SaleLine>().Property(e => e.DiscountType).HasConversion<string>();

        }
    }
}
