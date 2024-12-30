using RastaurantPosMAUI.Models;

namespace RastaurantPosMAUI.Controls;

public partial class SaveMenuItemFormControl : ContentView
{
    public SaveMenuItemFormControl()
    {
        InitializeComponent();
    }
    //public static readonly BindableProperty CategoriesProperty =
    //   BindableProperty.Create(nameof(Categories), typeof(MenuCategoryModel[]), typeof(SaveMenuItemFormControl), Array.Empty<MenuCategoryModel>());


    //public MenuCategoryModel[] Categories
    //{
    //    get => (MenuCategoryModel[])GetValue(CategoriesProperty);
    //    set => SetValue(CategoriesProperty, value);
    //}

    public static readonly BindableProperty ItemProperty =
       BindableProperty.Create(nameof(Item), typeof(MenuItemModel), typeof(SaveMenuItemFormControl), new MenuItemModel());

    public MenuItemModel Item
    {
        get => (MenuItemModel)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }


    private void ToggleCategorySelection(MenuCategoryModel category)=>
        category.IsSelected = !category.IsSelected;
}