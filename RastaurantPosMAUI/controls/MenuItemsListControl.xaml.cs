using CommunityToolkit.Mvvm.Input;

namespace RastaurantPosMAUI.Controls;

public partial class MenuItemsListControl : ContentView
{
	public MenuItemsListControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty ItemsProperty =
		BindableProperty.Create(nameof(Items), typeof(MenuItem[]), typeof(MenuItemsListControl), Array.Empty<MenuItem>());

	public event Action<MenuItem> OnSelectItem;

	public MenuItem[] Items 
	{ 
		get => (MenuItem[])GetValue(ItemsProperty); 
		set => SetValue(ItemsProperty, value); 
	}

	[RelayCommand]
	private void SelectItem(MenuItem item) => OnSelectItem?.Invoke(item);
}