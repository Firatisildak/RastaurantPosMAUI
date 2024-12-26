using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastaurantPosMAUI.ViewModels
{
    public partial class OrdersViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public OrdersViewModel(DatabaseService databaseService) 
        {
            _databaseService = databaseService;
        }

        public ObservableCollection<Order> Orders { get; set; } = [];

        //Return true if the order creating was successgull, false otherwise
        public async Task<bool> PlaceOrderAsync(CartModel[] cartItems, bool isPaidOnline)
        {
            var orderItems = cartItems.Select(c => new OrderItem
            {
                Icon = c.Icon,
                ItemId = c.ItemId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.Quantity
            }).ToArray();

            var orderModel = new OrderModel
            {
                OrderDate = DateTime.Now,
                PaymentMode = isPaidOnline ? "Online" : "Cash",
                TotalAmountPaid = cartItems.Sum(c => c.Amount),
                TotalItemsCount = cartItems.Length,
                Items = orderItems
            };

            var errorMessage=await _databaseService.PlaceOrderAsync(orderModel);
            if (!string.IsNullOrEmpty(errorMessage)) 
            {
                //Order creating failed
                await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
                return false;
            }
            //Order Creating was succesfull
            await Toast.Make("Order placed successfully").Show();
            return true;
        }

        private bool _isInitialized;

        [ObservableProperty]
        private bool _isLoading;

        public async ValueTask InitializeAsync()
        {
            if(_isInitialized) 
                return;
            _isInitialized = true;
            IsLoading= true;
            var orders = await _databaseService.GetOrdersAsync();
            foreach (var order in orders) 
            {
                Orders.Add(order);
            }
            IsLoading = false;
        }
    }
}
