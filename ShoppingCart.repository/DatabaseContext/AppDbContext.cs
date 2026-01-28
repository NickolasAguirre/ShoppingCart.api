using Microsoft.EntityFrameworkCore;
using ShoppingCart.domain.Entities.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.infrastructure.DatabaseContext
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartModel>(entity =>
            {
                entity.HasKey(e => e.CartId);

                entity.HasMany(e => e.ListProduct)
                      .WithOne(p => p.Cart)
                      .HasForeignKey(p => p.CartId);
            });

            // CartProductModel → CartProductGroupModel
            modelBuilder.Entity<CartProductModel>(entity =>
            {
                entity.HasKey(e => e.CartProductId);

                entity.HasMany(e => e.ProductGroup)
                      .WithOne(g => g.CartProduct)
                      .HasForeignKey(g => g.CartProductId);
            });

            // CartProductGroupModel → CartAttributeModel
            modelBuilder.Entity<CartProductGroupModel>(entity =>
            {
                entity.HasKey(e => e.CartProductGroupId);

                entity.HasMany(e => e.Attributes)
                      .WithOne(a => a.CartProductGroup)
                      .HasForeignKey(a => a.CartProductGroupId);
            });

            modelBuilder.Entity<CartAttributeModel>(entity =>
            {
                entity.HasKey(e => e.CartAttributeId);
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CartModel> Cart { get;set; }
        public DbSet<CartProductModel> CartProduct { get; set; }
        public DbSet<CartProductGroupModel> CartProductGroup { get; set; }
        public DbSet<CartAttributeModel> CartAttribute { get; set; }
    }
}
