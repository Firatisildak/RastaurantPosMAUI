using Microsoft.EntityFrameworkCore;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Data.Entities;
using RastaurantPosMAUI.Models;
using MenuItem = RastaurantPosMAUI.Data.Entities.MenuItem;

namespace RastaurantPosMAUI.Services
{
    public class DatabaseService : IAsyncDisposable
    {
        private readonly AppDBContext _dbContext;

        // DbContext'i dependency injection ile alıyoruz.
        public DatabaseService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Veritabanını oluşturma ve seed işlemi
        public async Task InitializeDatabaseAsync()
        {
            await _dbContext.Database.MigrateAsync();  // Veritabanını güncelle (migrasyonlar)
            await SeedDataAsync();  // Seed verileri ekleyelim
        }

        private async Task SeedDataAsync()
        {
            // Veritabanında veri var mı kontrol et
            if (await _dbContext.MenuCategories.AnyAsync())
                return;

            // Seed verileri
            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings();

            // Veritabanına ekle
            await _dbContext.MenuCategories.AddRangeAsync(categories);
            await _dbContext.MenuItems.AddRangeAsync(menuItems);
            await _dbContext.MenuItemCategoriesMapping.AddRangeAsync(mappings);
            await _dbContext.SaveChangesAsync();
        }

        // Menü kategorilerini getirme
        public async Task<MenuCategory[]> GetMenuCategoriesAsync() =>
            await _dbContext.MenuCategories.ToArrayAsync();

        // Kategorisine göre Menü öğelerini getirme
        public async Task<MenuItem[]> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return await _dbContext.MenuItems
                .Where(m => m.MenuItemCategoriesMapping.Any(c => c.MenuCategoryId == categoryId))
                .ToArrayAsync();
        }

        // Sipariş oluşturma
        public async Task<string?> PlaceOrderAsync(OrderModel model)
        {
            var order = new Order
            {
                OrderDate = model.OrderDate,
                PaymentMode = model.PaymentMode,
                TotalAmountPaid = model.TotalAmountPaid,
                TotalItemsCount = model.TotalItemsCount,
            };

            // Siparişi ekle
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // OrderId'yi sipariş öğelerine ekle
            foreach (var item in model.Items)
            {
                item.OrderId = order.Id;
            }

            // Sipariş öğelerini ekle
            await _dbContext.OrderItems.AddRangeAsync(model.Items);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 0)
            {
                // Sipariş öğeleri eklenemediyse, siparişi sil
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return "Error in inserting order items";
            }

            model.Id = order.Id;
            return null;
        }

        // Tüm siparişleri getirme
        public async Task<Order[]> GetOrdersAsync() =>
            await _dbContext.Orders.ToArrayAsync();

        // Bir siparişin öğelerini getirme
        public async Task<OrderItem[]> GetOrderItemsAsync(long orderId) =>
            await _dbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToArrayAsync();

        // Menü öğesinin kategorilerini getirme
        public async Task<MenuCategory[]> GetCategoriesOfMenuItem(int menuItemId)
        {
            return await _dbContext.MenuCategories
                .Where(cat => cat.MenuItemCategoriesMapping.Any(map => map.MenuItemId == menuItemId))
                .ToArrayAsync();
        }

        // Menü öğesini kaydetme (Yeni veya güncelleme)
        public async Task<string?> SaveMenuItemAsync(MenuItemModel model)
        {
            if (model.Id == 0)
            {
                // Yeni Menü Öğesi oluştur
                var menuItem = new MenuItem
                {
                    Name = model.Name,
                    Description = model.Description,
                    Icon = model.Icon,
                    Price = model.Price
                };

                // Menü öğesini ekle
                await _dbContext.MenuItems.AddAsync(menuItem);
                await _dbContext.SaveChangesAsync();

                // Kategorileri ilişkilendir
                var categoryMappings = model.SelectedCategories.Select(c => new MenuItemCategoryMapping
                {
                    MenuCategoryId = c.Id,
                    MenuItemId = menuItem.Id
                });

                await _dbContext.MenuItemCategoriesMapping.AddRangeAsync(categoryMappings);
                await _dbContext.SaveChangesAsync();

                model.Id = menuItem.Id;
                return null;
            }
            else
            {
                // Var olan Menü Öğesini güncelle
                var menuItem = await _dbContext.MenuItems.FindAsync(model.Id);
                if (menuItem == null) return "Menu item not found";

                menuItem.Name = model.Name;
                menuItem.Description = model.Description;
                menuItem.Icon = model.Icon;
                menuItem.Price = model.Price;

                // Menü öğesini güncelle
                _dbContext.MenuItems.Update(menuItem);

                // Kategorileri güncelle
                var existingMappings = await _dbContext.MenuItemCategoriesMapping
                    .Where(m => m.MenuItemId == menuItem.Id)
                    .ToListAsync();

                _dbContext.MenuItemCategoriesMapping.RemoveRange(existingMappings);

                var newMappings = model.SelectedCategories.Select(c => new MenuItemCategoryMapping
                {
                    MenuCategoryId = c.Id,
                    MenuItemId = menuItem.Id
                });

                await _dbContext.MenuItemCategoriesMapping.AddRangeAsync(newMappings);
                await _dbContext.SaveChangesAsync();

                return null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
