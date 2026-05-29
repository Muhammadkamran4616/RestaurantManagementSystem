using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Burgers"
            },
            new Category
            {
                Id = 2,
                Name = "Pizzas"
            },
            new Category
            {
                Id = 3,
                Name = "Drinks"
            }
        );

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem
            {
                Id = 1,
                Name = "Zinger Burger",
                Description = "Crispy chicken burger",
                Price = 550,
                IsAvailable = true,
                CategoryId = 1
            },
            new MenuItem
            {
                Id = 2,
                Name = "Pepperoni Pizza",
                Description = "Large pepperoni pizza",
                Price = 1800,
                IsAvailable = true,
                CategoryId = 2
            },
            new MenuItem
            {
                Id = 3,
                Name = "Coca Cola",
                Description = "Cold drink",
                Price = 120,
                IsAvailable = true,
                CategoryId = 3
            }
        );
    }
}