using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastaurantPosMAUI.Data.Entities;

public class MenuItem
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Ensures Name is not null in the database
    [MaxLength(100)] // Sets maximum length for the Name column
    public string Name { get; set; }

    public string Icon { get; set; } // Optional, no constraints for now

    [MaxLength(500)] // Optional, sets a reasonable maximum length for descriptions
    public string Description { get; set; }

    [Required] // Ensures Price is not null
    [Column(TypeName = "decimal(18,2)")] // Specifies decimal precision
    public decimal Price { get; set; }

    public ICollection<MenuItemCategoryMapping> MenuItemCategoriesMapping { get; set; }
}
