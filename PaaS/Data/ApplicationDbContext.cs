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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        SeedRoles(builder);
        SeedProvinces(builder);
        SeedCities(builder);
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
}