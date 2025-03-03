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
    public DbSet<ItemType> ItemType { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<PayPalConfirmationModel> PayPalConfirmationModel { get; set; }

<<<<<<< HEAD
    public DbSet<ContactInfo> ContactInfos { get; set; } = null;

    public DbSet<Order> Order { get; set; }

    public DbSet<OrderItem> OrderItem { get; set; }

    public DbSet<Item> Item { get; set; }

    public DbSet<City> Cities { get; set; }
    public DbSet<Province> Provinces { get; set; }


=======
>>>>>>> main
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedRoles(builder);
        SeedItemsData(builder);
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
    
    private void SeedItemsData(ModelBuilder builder)
    {
        builder.Entity<ItemType>().HasData(
            new ItemType { ItemTypeId = 1, Description = "Pizza" },
            new ItemType { ItemTypeId = 2, Description = "Slide" }, 
            new ItemType { ItemTypeId = 3, Description = "Drink" }
        );
        builder.Entity<Category>().HasData(
            new Category { IdCategory = 1, Description = "Specialty Pizzas" },
            new Category { IdCategory = 2, Description = "Vegetarian Pizzas" },
            new Category { IdCategory = 3, Description = "Appetizers" },
            new Category { IdCategory = 4, Description = "Custom" }
        );
        
        builder.Entity<Item>().HasData(
            new Item
            {
                ItemId = 1,
                Name = "BBQ Chicken",
                Description = "Grilled chicken, BBQ sauce, red onions, and cilantro",
                Price = 10m,
                ItemTypeId = 1,
                IdCategory = 1
            },
            new Item
            {
                ItemId = 2,
                Name = "Vegan Delight",
                Description = "Plant-based cheese, mushrooms, peppers, and vegan sausage",
                Price = 18.99m,
                ItemTypeId = 1,
                IdCategory = 2
            },
            new Item
            {
                ItemId = 3,
                Name = "Mozzarella Sticks",
                Description = "Breaded mozzarella with marinara sauce",
                Price = 6m,
                ItemTypeId = 2,
                IdCategory = 3
            }
        );
    }
}