using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaaS.Models;

public partial class SqlContext : DbContext
{
    public SqlContext()
    {
    }

    public SqlContext(DbContextOptions<SqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<EfmigrationsLock> EfmigrationsLocks { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=sql.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("description");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("name");
            entity.Property(e => e.ProvinceId).HasColumnName("provinceId");

            entity.HasOne(d => d.Province).WithMany(p => p.Cities)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.ToTable("ContactInfo");

            entity.Property(e => e.ContactId).HasColumnName("contactId");
            entity.Property(e => e.Address1)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("address2");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.Phone)
                .HasColumnType("VARCHAR(15)")
                .HasColumnName("phone");
            entity.Property(e => e.ProvinceId).HasColumnName("provinceId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.City).WithMany(p => p.ContactInfos)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Province).WithMany(p => p.ContactInfos)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.ContactInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.ToTable("DeliveryMethod");

            entity.Property(e => e.DeliveryMethodId).HasColumnName("deliveryMethodId");
            entity.Property(e => e.MethodName)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("methodName");
        });

        modelBuilder.Entity<EfmigrationsLock>(entity =>
        {
            entity.ToTable("__EFMigrationsLock");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.IdItemType).HasColumnName("idItemType");
            entity.Property(e => e.ImgUrl).HasColumnName("imgUrl");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("NUMERIC(10,2)")
                .HasColumnName("price");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdItemTypeNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdItemType)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.ToTable("ItemType");

            entity.Property(e => e.ItemTypeId).HasColumnName("itemTypeId");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("description");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.DeliveryMethodId).HasColumnName("deliveryMethodId");
            entity.Property(e => e.OrderDate)
                .HasColumnType("TIMESTAMP")
                .HasColumnName("orderDate");
            entity.Property(e => e.PaymentMethodId).HasColumnName("paymentMethodId");
            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("NUMERIC(10,2)")
                .HasColumnName("totalAmount");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemId });

            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("size");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId).HasColumnName("paymentMethodId");
            entity.Property(e => e.MethodName)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("methodName");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.ProvinceId).HasColumnName("provinceId");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("description");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("description");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("firstName");
            entity.Property(e => e.IsVerified)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isVerified");
            entity.Property(e => e.LastName)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
