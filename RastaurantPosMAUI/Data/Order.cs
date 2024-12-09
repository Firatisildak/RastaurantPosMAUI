using SQLite;

namespace RastaurantPosMAUI.Data;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public int TotalItemsCount { get; set; }
    public decimal TotalAmountPaid { get; set; }
    public string? PaymentMode { get; set; }
}
