using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RastaurantPosMAUI.Data.Entities;

public class MenuItemCategoryMapping
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("MenuCategory")]
    public int MenuCategoryId { get; set; }

    [ForeignKey("MenuItem")]
    public int MenuItemId { get; set; }
}
