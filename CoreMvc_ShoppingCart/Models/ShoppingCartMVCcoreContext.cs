using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreMvc_ShoppingCart.Models
{
    public partial class ShoppingCartMVCcoreContext : DbContext
    {
        public ShoppingCartMVCcoreContext()
        {
        }

        public ShoppingCartMVCcoreContext(DbContextOptions<ShoppingCartMVCcoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-T8ACSIP;Initial Catalog=ShoppingCartMVCcore;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("PK__Cart__CF0FBF2639786347");

                entity.ToTable("Cart");

                entity.Property(e => e.CardId).HasColumnName("CARD_ID");

                entity.Property(e => e.ProductCount).HasColumnName("PRODUCT_COUNT");

                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.ProductTotalprice)
                    .HasColumnType("money")
                    .HasColumnName("PRODUCT_TOTALPRICE");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Cart__PRODUCT_ID__29572725");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.ProductCount).HasColumnName("PRODUCT_COUNT");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_DESCRIPTION");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PRODUCT_PRICE");

                entity.Property(e => e.ProductReview)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_REVIEW");

                entity.Property(e => e.ProductSold).HasColumnName("PRODUCT_SOLD");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__CATEGO__267ABA7A");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Product___E7DA297C687FA3E0");

                entity.ToTable("Product_Category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.Firstmodified)
                    .HasColumnType("datetime")
                    .HasColumnName("FIRSTMODIFIED");

                entity.Property(e => e.Lastmodified)
                    .HasColumnType("datetime")
                    .HasColumnName("LASTMODIFIED");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserDeta__206A9DF8C21C587A");

                entity.Property(e => e.UserId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("User_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Phone_no");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
