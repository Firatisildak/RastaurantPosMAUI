using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MenuItem = RastaurantPosMAUI.Data.MenuItem;

namespace RastaurantPosMAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly OrdersViewModel _ordersViewModel;
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

        public decimal TaxAmount => (Subtotal * TaxPercentage)/100;

        public decimal Total => Subtotal + TaxAmount;

        public HomeViewModel(DatabaseService databaseService, OrdersViewModel ordersViewModel)
        {
            _databaseService = databaseService;
            _ordersViewModel = ordersViewModel;
            CartItems.CollectionChanged += CartItems_CollectionChanged;
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
        private void RemoveItemFromCart(CartModel cartItem)=> CartItems.Remove(cartItem);

        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if(await Shell.Current.DisplayAlert("Clear Cart", "Do you really want to clear the cart", "Yes", "No"))
            {
                CartItems.Clear();
            }
        }

        private void RecalculateAmounts()
        {
            Subtotal=CartItems.Sum(c => c.Amount);
        }

        [RelayCommand]
        private async Task TaxPercentageClickAsync()
        {
            var result = await Shell.Current.DisplayPromptAsync("Tax Percentage", "Enter the applicable tax percentage", placeholder: "10", initialValue: TaxPercentage.ToString());
            if (!string.IsNullOrWhiteSpace(result)) 
            {
                if(!int.TryParse(result, out int enteredTaxPercentage))
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
            }        
        }

        [RelayCommand]
        private async Task PlaceOrderAsync(bool isPaidOnline)
        {
            IsLoading = true;
            if(await _ordersViewModel.PlaceOrderAsync([.. CartItems], isPaidOnline)){
                //Order creating successfull
                //clear the cart items
                CartItems.Clear();
            }
            IsLoading = false;
        }
    }
}
