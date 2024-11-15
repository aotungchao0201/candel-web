using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Model.Models
{
    public partial class candleContext : DbContext
    {
        public candleContext()
        {
        }

        public candleContext(DbContextOptions<candleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Candle> Candles { get; set; } = null!;
        public virtual DbSet<CandlesImg> CandlesImgs { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Zalopay> Zalopays { get; set; } = null!;

        //         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //         {
        //             if (!optionsBuilder.IsConfigured)
        //             {
        // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                 optionsBuilder.UseSqlServer(GetConnectionString());
        //             }
        //         }
        //         private string GetConnectionString()
        //         {
        //             IConfiguration config = new ConfigurationBuilder()
        //              .SetBasePath(Directory.GetCurrentDirectory())
        //             .AddJsonFile("appsettings.json", true, true)
        //             .Build();
        //             var strConn = config["ConnectionStrings:Candle"];

        //             return strConn;
        //         }

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
                    .HasColumnName("created_at");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasColumnName("postal_code");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__addresses__user___29572725");
            });

            modelBuilder.Entity<Candle>(entity =>
            {
                entity.ToTable("candles");

                entity.Property(e => e.CandleId).HasColumnName("candle_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImgUrl)
                    .HasColumnType("text")
                    .HasColumnName("imgURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Candles)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__candles__categor__30F848ED");
            });

            modelBuilder.Entity<CandlesImg>(entity =>
            {
                entity.HasKey(e => e.CandleImgId);

                entity.ToTable("candles_img");

                entity.Property(e => e.CandleImgId)
                    .ValueGeneratedNever()
                    .HasColumnName("candle_img_id");

                entity.Property(e => e.CandleId).HasColumnName("candle_id");

                entity.Property(e => e.ImgUrl)
                    .HasColumnType("text")
                    .HasColumnName("imgURL");

                entity.HasOne(d => d.Candle)
                    .WithMany(p => p.CandlesImgs)
                    .HasForeignKey(d => d.CandleId)
                    .HasConstraintName("FK_candles_img_candles");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("logs");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__logs__user_id__33D4B598");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.IsPay)
                    .HasMaxLength(50)
                    .HasColumnName("isPay");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("note");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_price");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__orders__user_id__2E1BDC42");
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
                    .HasConstraintName("FK__order_ite__candl__3A81B327");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__3B75D760");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .HasColumnName("payment_method");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(255)
                    .HasColumnName("transaction_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__payments__order___3E52440B");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.CandleId).HasColumnName("candle_id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Candle)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CandleId)
                    .HasConstraintName("FK__reviews__candle___36B12243");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__reviews__user_id__37A5467C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user__role_id__267ABA7A");
            });

            modelBuilder.Entity<Zalopay>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("zalopay");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Appid).HasColumnName("appid");

                entity.Property(e => e.Apptime).HasColumnName("apptime");

                entity.Property(e => e.Apptransid)
                    .HasMaxLength(255)
                    .HasColumnName("apptransid");

                entity.Property(e => e.Appuser)
                    .HasMaxLength(255)
                    .HasColumnName("appuser");

                entity.Property(e => e.Bankcode)
                    .HasMaxLength(255)
                    .HasColumnName("bankcode");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Embeddata).HasColumnName("embeddata");

                entity.Property(e => e.Mac)
                    .HasMaxLength(255)
                    .HasColumnName("mac");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Paymentcode)
                    .HasMaxLength(255)
                    .HasColumnName("paymentcode");

                entity.Property(e => e.Returnurl)
                    .HasMaxLength(255)
                    .HasColumnName("returnurl");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_zalopay_orders1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
