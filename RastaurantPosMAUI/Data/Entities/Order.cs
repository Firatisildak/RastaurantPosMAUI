using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastaurantPosMAUI.Data.Entities;

public class Order
{
    [Key] // Primary key
    public long Id { get; set; }

    public DateTime? OrderDate { get; set; }

    [Required] // Ensures TotalItemsCount is not null
    public int TotalItemsCount { get; set; }

    [Required] // Ensures TotalAmountPaid is not null
    [Column(TypeName = "decimal(18,2)")] // Ensures decimal precision
    public decimal TotalAmountPaid { get; set; }

    [MaxLength(50)] // Sets maximum length for PaymentMode
    public string? PaymentMode { get; set; }
}
