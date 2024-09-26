using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Model.Models
{
    public partial class CandleContext : DbContext
    {
        public CandleContext()
        {
        }

        public CandleContext(DbContextOptions<CandleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Candle> Candles { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<VnpayTransaction> VnpayTransactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config["ConnectionStrings:Candle"];

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.AddressLine)
                    .HasMaxLength(255)
                    .HasColumnName("address_line");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasColumnName("postal_code");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__addresses__user___4D94879B");
            });

            modelBuilder.Entity<Candle>(entity =>
            {
                entity.ToTable("candles");

                entity.Property(e => e.CandleId).HasColumnName("candle_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.StockQuantity)
                    .HasColumnName("stock_quantity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Candles)
                    .UsingEntity<Dictionary<string, object>>(
                        "CandleCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__candle_ca__categ__49C3F6B7"),
                        r => r.HasOne<Candle>().WithMany().HasForeignKey("CandleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__candle_ca__candl__48CFD27E"),
                        j =>
                        {
                            j.HasKey("CandleId", "CategoryId").HasName("PK__candle_c__2B591B041DB98774");

                            j.ToTable("candle_categories");

                            j.IndexerProperty<int>("CandleId").HasColumnName("candle_id");

                            j.IndexerProperty<int>("CategoryId").HasColumnName("category_id");
                        });
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.Name, "UQ__categori__72E12F1B0256AB7F")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("logs");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__logs__user_id__5165187F");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__user_id__32E0915F");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.CandleId).HasColumnName("candle_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Candle)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.CandleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_ite__candl__36B12243");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__35BCFE0A");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.CandleId).HasColumnName("candle_id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Candle)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CandleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reviews__candle___4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reviews__user_id__4222D4EF");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "UQ__users__AB6E616493B607C4")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC572FA668BC4")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .HasColumnName("role")
                    .HasDefaultValueSql("('user')");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<VnpayTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__vnpay_tr__85C600AFAF4F0A85");

                entity.ToTable("vnpay_transactions");

                entity.HasIndex(e => e.VnpTransactionId, "UQ__vnpay_tr__EE5D997C0508652C")
                    .IsUnique();

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(10)
                    .HasColumnName("payment_status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.VnpTransactionId)
                    .HasMaxLength(255)
                    .HasColumnName("vnp_transaction_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.VnpayTransactions)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__vnpay_tra__order__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
