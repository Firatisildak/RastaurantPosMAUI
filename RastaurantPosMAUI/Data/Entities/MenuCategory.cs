using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastaurantPosMAUI.Data.Entities;

public class MenuCategory
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required] // Optional, ensures Name is not null in the database
    [MaxLength(100)] // Optional, sets maximum length for the Name column
    public string Name { get; set; }

    public string Icon { get; set; } // Optional, no constraints for now
    public ICollection<MenuItemCategoryMapping> MenuItemCategoriesMapping { get; set; }
}
