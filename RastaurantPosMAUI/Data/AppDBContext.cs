using Microsoft.EntityFrameworkCore;
using RastaurantPosMAUI.Data.Entities;
using MenuItem = RastaurantPosMAUI.Data.Entities.MenuItem;

namespace RastaurantPosMAUI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {

        }

        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemCategoryMapping> MenuItemCategoriesMapping { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=RestaurantPosDB;User ID=sa;Password=sa;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuCategory>().HasData(SeedData.GetMenuCategories());
            modelBuilder.Entity<MenuItem>().HasData(SeedData.GetMenuItems());
            modelBuilder.Entity<MenuItemCategoryMapping>().HasData(SeedData.GetMenuItemCategoryMappings());
        }
    }
}
