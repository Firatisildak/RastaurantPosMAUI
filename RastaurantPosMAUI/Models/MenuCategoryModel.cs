using CommunityToolkit.Mvvm.ComponentModel;

namespace RastaurantPosMAUI.Models
{
    public partial class MenuCategoryModel : ObservableObject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }

        [ObservableProperty]
        private bool _isSelected;

        public static MenuCategoryModel FromEntity(MenuCategoryModel entity) =>
            new() 
            {
                Id = entity.Id,
                Name = entity.Name,
                Icon = entity.Icon,
            };
    }
}
