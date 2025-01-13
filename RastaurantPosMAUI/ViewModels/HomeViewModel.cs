using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Models;
using RastaurantPosMAUI.Services;
using System.Collections.ObjectModel;
using MenuItem = RastaurantPosMAUI.Data.Entities.MenuItem;

namespace RastaurantPosMAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject, IRecipient<MenuItemChangedMessage>
    {
        private readonly DatabaseService _databaseService;
        private readonly OrdersViewModel _ordersViewModel;
        private readonly SettingsViewModel _settingsViewModel;

        [ObservableProperty]
        private MenuCategoryModel[] _categories = [];

        [ObservableProperty]
        private MenuItem[] _menuItems = [];

        [ObservableProperty]
        private MenuCategoryModel? _selectedCategory = null;

        public ObservableCollection<CartModel> CartItems { get; set; } = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TaxAmount))]
        [NotifyPropertyChangedFor(nameof(Total))]
        private decimal _subtotal;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TaxAmount))]
        [NotifyPropertyChangedFor(nameof(Total))]
        private int _taxPercentage;

        public decimal TaxAmount => (Subtotal * TaxPercentage) / 100;

        public decimal Total => Subtotal + TaxAmount;

        [ObservableProperty]
        private string _name = "Guest";

        public HomeViewModel(DatabaseService databaseService, OrdersViewModel ordersViewModel, SettingsViewModel settingsViewModel)
        {
            _databaseService = databaseService;
            _ordersViewModel = ordersViewModel;
            _settingsViewModel = settingsViewModel;
            CartItems.CollectionChanged += CartItems_CollectionChanged;

            WeakReferenceMessenger.Default.Register<MenuItemChangedMessage>(this);
            WeakReferenceMessenger.Default.Register<NameChangedMessage>(this, (reciepent, message) => Name = message.Value);

            //Get TaxPercentange from Preferences
            TaxPercentage = _settingsViewModel.GetTaxPercentage();

        }

        private void CartItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //It will be executed whenevr
            //we are adding any item to the cart
            //removing item from the cart
            //or clearing the cart
            RecalculateAmounts();
        }

        private bool _isIntialized;

        public async ValueTask InitializeAsync()
        {
            if (_isIntialized)
                return;

            _isIntialized = true;

            IsLoading = true;

            Categories = (await _databaseService.GetMenuCategoriesAsync())
                .Select(MenuCategoryModel.FromEntity)
                .ToArray();

            Categories[0].IsSelected = true;
            SelectedCategory = Categories[0];

            MenuItems = await _databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

            IsLoading = false;
        }

        [RelayCommand]
        private async Task SelectCategoryAsync(int categoryId)
        {
            if (SelectedCategory.Id == categoryId)
                return;

            IsLoading = true;

            var existingSelectedCategory = Categories.First(c => c.IsSelected);
            existingSelectedCategory.IsSelected = false;

            var newlySelectedCategory = Categories.First(c => c.Id == categoryId);
            newlySelectedCategory.IsSelected = true;

            SelectedCategory = newlySelectedCategory;

            MenuItems = await _databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

            IsLoading = false;
        }

        [RelayCommand]
        private void AddToCart(MenuItem menuItem)
        {
            var cartItem = CartItems.FirstOrDefault(c => c.ItemId == menuItem.Id);
            if (cartItem == null)
            {
                //Item does not exist in the cart
                //Add item to Cart
                cartItem = new CartModel
                {
                    ItemId = menuItem.Id,
                    Icon = menuItem.Icon,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Quantity = 1
                };

                CartItems.Add(cartItem);
            }
            else
            {
                //This item exists in cart
                //Increase the quantity for this item in the cart
                cartItem.Quantity++;
                RecalculateAmounts();
            }
        }

        [RelayCommand]
        private void IncreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity++;
            RecalculateAmounts();
        }

        [RelayCommand]
        private void DecreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity--;
            if (cartItem.Quantity == 0)
                CartItems.Remove(cartItem);
            else
                RecalculateAmounts();
        }

        [RelayCommand]
        private void RemoveItemFromCart(CartModel cartItem) => CartItems.Remove(cartItem);

        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if (await Shell.Current.DisplayAlert("Clear Cart", "Do you really want to clear the cart", "Yes", "No"))
            {
                CartItems.Clear();
            }
        }

        private void RecalculateAmounts()
        {
            Subtotal = CartItems.Sum(c => c.Amount);
        }

        [RelayCommand]
        private async Task TaxPercentageClickAsync()
        {
            var result = await Shell.Current.DisplayPromptAsync("Tax Percentage", "Enter the applicable tax percentage", placeholder: "10", initialValue: TaxPercentage.ToString());
            if (!string.IsNullOrWhiteSpace(result))
            {
                if (!int.TryParse(result, out int enteredTaxPercentage))
                {
                    await Shell.Current.DisplayAlert("Invalid Value", "Entered tax percentage is valid", "Ok");
                    return;
                }

                //it was a valid numeric value
                if (enteredTaxPercentage > 100)
                {
                    await Shell.Current.DisplayAlert("Invalid Value", "Tax percentage cannot be more than 100", "Ok");
                    return;
                }
                TaxPercentage = enteredTaxPercentage;

                //Save it in preferences
                _settingsViewModel.SetTaxPercentage(enteredTaxPercentage);

            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync(bool isPaidOnline)
        {
            IsLoading = true;
            if (await _ordersViewModel.PlaceOrderAsync([.. CartItems], isPaidOnline))
            {
                //Order creating successfull
                //clear the cart items
                CartItems.Clear();
            }
            IsLoading = false;
        }

        public void Receive(MenuItemChangedMessage message)
        {
            var model = message.Value;
            var menuItem = MenuItems.FirstOrDefault(m => m.Id == model.Id);
            if (menuItem != null)
            {
                //This menu item is on the screen the right now

                //check if the the this still has a mapping to selected category
                if (!model.SelectedCategories.Any(c => c.Id == SelectedCategory.Id))
                {
                    //This item no longer belongs to the selected category
                    //Remove this item from the current UI Menu Items list
                    MenuItems = [.. MenuItems.Where(m => m.Id == model.Id)];
                    return;
                }

                //update the details
                menuItem.Price = model.Price;
                menuItem.Description = model.Description;
                menuItem.Name = model.Name;
                menuItem.Icon = model.Icon;

                MenuItems = [.. MenuItems];
            }
            else if (model.SelectedCategories.Any(c => c.Id == SelectedCategory.Id))
            {
                //This item was not on the UI
                //We updated the item by adding this currently selected category
                //So add this menu item to the current ui menu items list
                var newMenuItem = new MenuItem
                {
                    Id = model.Id,
                    Description = model.Description,
                    Icon = model.Icon,
                    Name = model.Name,
                    Price = model.Price
                };

                MenuItems = [.. MenuItems, newMenuItem];
            }

            //Check if the updated menu item is added in the cart
            //if yes, updated the info in the cart
            var cartItem = CartItems.FirstOrDefault(c => c.ItemId == model.Id);
            if (cartItem != null)
            {
                cartItem.Price = model.Price;
                cartItem.Icon = model.Icon;
                cartItem.Name = model.Name;

                var itemIndex = CartItems.IndexOf(cartItem);

                //It will trigger CollectionChanged event for replacng this item
                //Which will recalculate amounts
                CartItems[itemIndex] = cartItem;
            }
        }
    }
}
