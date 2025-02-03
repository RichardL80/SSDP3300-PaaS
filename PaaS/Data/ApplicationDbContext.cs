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
    public DbSet<PayPalConfirmationModel> PayPalConfirmationModel { get; set; }

    public DbSet<ContactInfo> ContactInfos { get; set; } = null;

    public DbSet<Order> Order { get; set; }

    public DbSet<OrderItem> OrderItem { get; set; }

    public DbSet<Item> Item { get; set; }

    public DbSet<City> Cities { get; set; }
    public DbSet<Province> Provinces { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        SeedRoles(builder);
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
}