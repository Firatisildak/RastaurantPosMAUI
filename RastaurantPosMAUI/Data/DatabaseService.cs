using RastaurantPosMAUI.Models;
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

        public async Task<string?> PlaceOrderAsync(OrderModel model)
        {
            var order = new Order
            {
                OrderDate = model.OrderDate,
                PaymentMode = model.PaymentMode,
                TotalAmountPaid = model.TotalAmountPaid,
                TotalItemsCount = model.TotalItemsCount,
            };

            if (await _connection.InsertAsync(order) > 0)
            {
                //Order Inserted succesfully
                //now we have newly inserted order id in order.Id
                //We can add the orderId to the OrderItems and Insert OrderItems in the database
                foreach (var item in model.Items)
                {
                    item.OrderId = order.Id;
                }
                if (await _connection.InsertAllAsync(model.Items) == 0)
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
            model.Id = order.Id;
            return null;
        }

        public async Task<Order[]> GetOrdersAsync() =>
            await _connection.Table<Order>().ToArrayAsync();

        public async Task<OrderItem[]> GetOrderItemsAsync(long orderId) =>
            await _connection.Table<OrderItem>().Where(oi => oi.OrderId == orderId).
            ToArrayAsync();

        public async Task<MenuCategory[]> GetCategoriesOfMenuItem(int menuItemId)
        {
            var query = @"SELECT cat.*
                    From MenuCategory cat
                    INNER JOIN MenuItemCategoryMapping map
                        ON cat.Id=map.MenuCategoryId
                    WHERE map.MenuItemId=?";
            var categories = await _connection.QueryAsync<MenuCategory>(query, menuItemId);
            return [.. categories];
        }

        public async Task<string?> SaveMenuItemAsync(MenuItemModel model)
        {
            if (model.Id == 0)
            {
                //Creating a new menu item

                MenuItem menuItem = new()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Icon = model.Icon,
                    Price = model.Price
                };

                if (await _connection.InsertAsync(menuItem) > 0)
                {
                    var categoryMapping = model.SelectedCategories
                        .Select(c => new MenuItemCategoryMapping
                        {
                            Id = c.Id,
                            MenuCategoryId = c.Id,
                            MenuItemId = menuItem.Id
                        });

                    if (await _connection.InsertAllAsync(categoryMapping) > 0)
                    {
                        model.Id = menuItem.Id;
                        return null;
                    }
                    else
                    {
                        //Menu Item Insert was Successfull
                        //but Category mapping Insert operation failed
                        //we should remove the newly inserted menu item from the database
                        await _connection.DeleteAsync(menuItem);
                    }
                }
                return "Error in saving menu item";
            }
            else
            {
                //Updating an existing menu item

                string? errorMessage = null;

                await _connection.RunInTransactionAsync(db =>
                {
                    var menuItem = db.Find<MenuItem>(model.Id);

                    menuItem.Name = model.Name;
                    menuItem.Description = model.Description;
                    menuItem.Icon = model.Icon;
                    menuItem.Price = model.Price;

                    if (db.Update(menuItem) == 0)
                    {
                        //this operation failed
                        //return error message

                        errorMessage = "Error in updating menu item";
                        throw new Exception(); //To trigger rollback
                    }

                    var deleteQuery = @"
                            DELETE FROM MenuItemCategoryMapping
                            WHERE MenuItemId=?";
                    db.Execute(deleteQuery, menuItem.Id);

                    var categoryMapping = model.SelectedCategories
                        .Select(c => new MenuItemCategoryMapping
                        {
                            Id = c.Id,
                            MenuCategoryId = c.Id,
                            MenuItemId = menuItem.Id
                        });
                    if (db.InsertAll(categoryMapping) == 0)
                    {
                        //this operation failed
                        //return error message

                        errorMessage = "Error in updating menu item";
                        throw new Exception(); //To trigger rollback
                    }
                });

                return errorMessage;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
                await _connection.CloseAsync();
        }
    }
}
