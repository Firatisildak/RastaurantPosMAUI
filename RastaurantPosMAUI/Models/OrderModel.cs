using CommunityToolkit.Mvvm.ComponentModel;
using RastaurantPosMAUI.Data.Entities;

namespace RastaurantPosMAUI.Models
{
    public partial class OrderModel : ObservableObject
    {
        public long Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public int TotalItemsCount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public string? PaymentMode { get; set; }
        public OrderItem[] Items { get; set; }

        [ObservableProperty]
        private bool _isSelected;
    }
}
