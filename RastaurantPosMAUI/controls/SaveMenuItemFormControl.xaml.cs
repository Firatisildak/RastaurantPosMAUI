using CommunityToolkit.Mvvm.Input;
using RastaurantPosMAUI.Models;

namespace RastaurantPosMAUI.Controls;

public partial class SaveMenuItemFormControl : ContentView
{
    private const string DefaultIcon = "image.png";
    public SaveMenuItemFormControl()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ItemProperty =
       BindableProperty.Create(nameof(Item), typeof(MenuItemModel), typeof(SaveMenuItemFormControl), new MenuItemModel(), propertyChanged: OnItemChanged);

    public MenuItemModel Item
    {
        get => (MenuItemModel)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    private static void OnItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is MenuItemModel menuItemModel)
        {
            if (bindable is SaveMenuItemFormControl thisControl)
            {
                if (menuItemModel.Id > 0)
                {
                    thisControl.SetIconImage(false, menuItemModel.Icon, thisControl);
                    thisControl.ExistingIcon = menuItemModel.Icon;
                }
                else
                {
                    thisControl.SetIconImage(true, null, thisControl);
                }
            }
        }
    }

    public string? ExistingIcon { get; set; }

    [RelayCommand]
    private void ToggleCategorySelection(MenuCategoryModel category) =>
        category.IsSelected = !category.IsSelected;

    public event Action? OnCancel;

    [RelayCommand]
    private void Cancel() => OnCancel?.Invoke();

    private async void PickImageButton_Clicked(object sender, EventArgs e)
    {
        var fileResult = await MediaPicker.PickPhotoAsync();
        if (fileResult != null)
        {
            //User selected an image from the Image Picker Dialog

            //Upload/Save the image on disc
            var imageStream = await fileResult.OpenReadAsync();

            var localPath=Path.Combine(FileSystem.AppDataDirectory, fileResult.FileName);

            using var fs= new FileStream(localPath, FileMode.Create, FileAccess.Write);
            await imageStream.CopyToAsync(fs);
            //Update the imageIcon on the ui

            SetIconImage(isDeFault: false, localPath);

            Item.Icon = localPath;
        }
        else
        {
            //User canceled from Image Picker Dialog

            if(ExistingIcon != null)
            {
                SetIconImage(isDeFault: false, ExistingIcon);
            }
            else
            {
                SetIconImage(isDeFault: true);
            }
        }
    }

    public void SetIconImage(bool isDeFault, string? iconSource=null, SaveMenuItemFormControl? control=null)
    {
        int size = 100;
        if (isDeFault) 
        {
            iconSource= DefaultIcon;
            size = 36;
        }
        control = control ?? this;

        control.itemIcon.Source = iconSource;
        control.itemIcon.HeightRequest = control.itemIcon.WidthRequest= size;
    }

    public event Action<MenuItemModel>? OnSaveItem;


    [RelayCommand]
    private async Task SaveMenuItemAsync()
    {
        //Validation

        if(string.IsNullOrWhiteSpace(Item.Name) || string.IsNullOrWhiteSpace(Item.Description))
        {
            await ErrorAlertAsync("Item name and description are mendatory");
            return;
        }

        if (Item.SelectedCategories.Length==0)
        {
            await ErrorAlertAsync("Please select at-least 1 category");
            return;
        }

        if (Item.Icon == DefaultIcon) 
        {
            await ErrorAlertAsync("Icon image is mendatory");
            return;
        }

        OnSaveItem?.Invoke(Item);

        static async Task ErrorAlertAsync(string message) =>
            await Shell.Current.DisplayAlert("Validation Error", message, "Ok");
    }
}