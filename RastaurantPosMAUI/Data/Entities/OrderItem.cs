using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastaurantPosMAUI.Data.Entities;

public class OrderItem
{
    [Key] // Primary key
    public long Id { get; set; }

    [Required] // Foreign key for Order
    public long OrderId { get; set; }

    [Required] // Foreign key for MenuItem (ItemId)
    public int ItemId { get; set; }

    [Required]
    [MaxLength(100)] // Sets maximum length for Name
    public string Name { get; set; }

    [MaxLength(200)] // Sets maximum length for Icon
    public string Icon { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")] // Ensures decimal precision
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [NotMapped] // Computed property, excluded from database mapping
    public decimal Amount => Quantity * Price;
}
