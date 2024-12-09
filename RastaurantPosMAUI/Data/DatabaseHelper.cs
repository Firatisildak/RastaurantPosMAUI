using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantPosMAUI.Data;
using SQLite;
namespace RastaurantPosMAUI.Data
{
    public class DatabaseHelper : IAsyncDisposable
    {
        private readonly SQLiteAsyncConnection _connection;
        public DatabaseHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "restpos.db3");
            _connection = new SQLiteAsyncConnection(dbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        public async Task SeedDataAsync()
        {
            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null) 
                await _connection.CloseAsync();
        }
    }
}
