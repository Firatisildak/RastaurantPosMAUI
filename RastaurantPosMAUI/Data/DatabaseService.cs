using RastaurantPosMAUI.Models;
using RestaurantPosMAUI.Data;
using SQLite;
namespace RastaurantPosMAUI.Data
{
    public class DatabaseService : IAsyncDisposable
    {
        private readonly SQLiteAsyncConnection _connection;
        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "restpos.db3");
            _connection = new SQLiteAsyncConnection(dbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }
        public async Task InitializeDatabaseAsync()
        {
            await _connection.CreateTableAsync<MenuCategory>();
            await _connection.CreateTableAsync<MenuItem>();
            await _connection.CreateTableAsync<MenuItemCategoryMapping>();
            await _connection.CreateTableAsync<Order>();
            await _connection.CreateTableAsync<OrderItem>();

            await SeedDataAsync();
        }

        private async Task SeedDataAsync()
        {
            var firstCategory = await _connection.Table<MenuCategory>().FirstOrDefaultAsync();

            if (firstCategory != null)
                return;

            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings();

            await _connection.InsertAllAsync(categories);
            await _connection.InsertAllAsync(menuItems);
            await _connection.InsertAllAsync(mappings);
        }

        public async Task<MenuCategory[]> GetMenuCategoriesAsync() =>
            await _connection.Table<MenuCategory>().ToArrayAsync();

        public async Task<MenuItem[]> GetMenuItemsByCategoryAsync(int categoryId)
        {
            var query = @"
                        Select menu.*
                        From MenuItem AS menu
                        INNER JOIN MenuItemCategoryMapping AS mapping
                            On menu.Id = mapping.MenuItemId
                        WHERE mapping.MenuCategoryId=?
                        ";
            var menuItems = await _connection.QueryAsync<MenuItem>(query, categoryId);
            return [.. menuItems];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns Error Message or null(if the operation was successfull)</returns>

        public  async Task<string?> PlaceOrderAsync(OrderModel model)
        {
            var order = new Order
            {
                OrderDate=model.OrderDate,
                PaymentMode= model.PaymentMode,
                TotalAmountPaid=model.TotalAmountPaid,
                TotalItemsCount=model.TotalItemsCount,
            };

            if(await _connection.InsertAsync(order) > 0)
            {
                //Order Inserted succesfully
                //now we have newly inserted order id in order.Id
                //We can add the orderId to the OrderItems and Insert OrderItems in the database
                foreach( var item in model.Items)
                {
                    item.OrderId=order.Id;
                }
                if(await _connection.InsertAllAsync(model.Items) == 0)
                {
                    //OrderItems insert operation failed
                    //Remove the Newly Inserted Order in this method
                    await _connection.DeleteAsync(order);
                    return "Error in inserting order items";
                }
            }
            else
            {
                return "Error in inserting order order";
            }
            model.Id= order.Id;
            return null;
        }

        public async Task<Order[]> GetOrdersAsync()=>
            await _connection.Table<Order>().ToArrayAsync();

        public async Task<OrderItem[]> GetOrderItemsAsync(long orderId)=>
            await _connection.Table<OrderItem>().Where(oi=>oi.OrderId== orderId).
            ToArrayAsync();

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
                await _connection.CloseAsync();
        }
    }
}
