using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaaS.Models;


namespace PaaS.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<ContactInfo> ContactInfo { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<Province> Province { get; set; }
    public DbSet<Order> Order { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        SeedRoles(builder);
        SeedProvinces(builder);
        SeedCities(builder);
        // SeedOrders(builder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
            new IdentityRole { Id = "3", Name = "Customer", NormalizedName = "CUSTOMER" }
        );

        builder.Entity<Role>().HasData(
            new Role { RoleId = 1, Description = "Admin" },
            new Role { RoleId = 2, Description = "Manager" },
            new Role { RoleId = 3, Description = "Customer" }
        );
    }

    private void SeedCities(ModelBuilder builder)
    {
        builder.Entity<City>().HasData(
            new City { CityId = -1, Name = "", ProvinceId = -1 },
            new City { CityId = 1, Name = "Vancouver", ProvinceId = 1 },
            new City { CityId = 2, Name = "Toronto", ProvinceId = 2 }
        );
    }

    private void SeedProvinces(ModelBuilder builder)
    {
        builder.Entity<Province>().HasData(
            new Province { ProvinceId = -1, Name = "" },
            new Province { ProvinceId = 1, Name = "British Columbia" },
            new Province { ProvinceId = 2, Name = "Ontario" }
        );
    }

    // private void SeedOrders(ModelBuilder builder)
    // {
    //     builder.Entity<Order>().HasData(
    //         new Order { OrderId = 1, UserId = 1, OrderDate = new DateTime(2025, 1, 26), TotalAmount = 100, DeliveryMethodId = 1, PaymentMethodId = 1, StatusId = 1 },
    //         new Order { OrderId = 2, UserId = 1, OrderDate = new DateTime(2025, 1, 27), TotalAmount = 200, DeliveryMethodId = 2, PaymentMethodId = 2, StatusId = 2 }
    //     );
    // }
}