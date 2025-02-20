﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Models;
using RastaurantPosMAUI.Services;
using MenuItem = RastaurantPosMAUI.Data.Entities.MenuItem;

namespace RastaurantPosMAUI.ViewModels
{
    public partial class ManageMenuItemsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public ManageMenuItemsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [ObservableProperty]
        private MenuCategoryModel[] _categories = [];

        [ObservableProperty]
        private MenuItem[] _menuItems = [];

        [ObservableProperty]
        private MenuCategoryModel? _selectedCategory = null;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private MenuItemModel _menuItem = new();

        private bool _isIntialized;

        public async ValueTask InitializeAsync()
        {
            if (_isIntialized)
                return;

            _isIntialized = true;

            IsLoading = true;

            Categories = (await _databaseService.GetMenuCategoriesAsync())
                .Select(MenuCategoryModel.FromEntity)
                .ToArray();

            Categories[0].IsSelected = true;
            SelectedCategory = Categories[0];

            MenuItems = await _databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

            SetEmptyCategoriesToItem();

            IsLoading = false;
        }

        private void SetEmptyCategoriesToItem()
        {
            MenuItem.Categories.Clear();
            foreach (var category in Categories)
            {
                var categoryOfItem = new MenuCategoryModel
                {
                    Id = category.Id,
                    Icon = category.Icon,
                    Name = category.Name,
                    IsSelected = false
                };
                MenuItem.Categories.Add(categoryOfItem);
            }
        }

        [RelayCommand]
        private async Task SelectCategoryAsync(int categoryId)
        {
            if (SelectedCategory.Id == categoryId)
                return;

            IsLoading = true;

            var existingSelectedCategory = Categories.First(c => c.IsSelected);
            existingSelectedCategory.IsSelected = false;

            var newlySelectedCategory = Categories.First(c => c.Id == categoryId);
            newlySelectedCategory.IsSelected = true;

            SelectedCategory = newlySelectedCategory;

            MenuItems = await _databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

            IsLoading = false;
        }

        [RelayCommand]
        private async Task EditMenuItemAsync(MenuItem menuItem)
        {
            //await Shell.Current.DisplayAlert("Edit", "Edit menu item", "Ok");
            var menuItemModel = new MenuItemModel
            {
                Description = menuItem.Description,
                Icon = menuItem.Icon,
                Id = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price
            };

            var itemCategories = await _databaseService.GetCategoriesOfMenuItem(menuItem.Id);
            foreach (var category in Categories)
            {
                var categoryOfItem = new MenuCategoryModel
                {
                    Icon = category.Icon,
                    Id = category.Id,
                    Name = category.Name
                };
                if (itemCategories.Any(c => c.Id == category.Id))
                    categoryOfItem.IsSelected = true;
                else
                    categoryOfItem.IsSelected = false;
                menuItemModel.Categories.Add(categoryOfItem);
            }

            MenuItem = menuItemModel;
        }

        [RelayCommand]
        private void Cancel()
        {
            MenuItem = new();
            SetEmptyCategoriesToItem();
        }

        [RelayCommand]
        private async Task SaveMenuItemAsync(MenuItemModel model)
        {
            IsLoading = true;

            //Save this item to database
            var errorMessage = await _databaseService.SaveMenuItemAsync(model);

            if (errorMessage != null)
            {
                await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
            }
            else
            {
                await Toast.Make("Menu item saved successfully").Show();
                HandleMenuItemChanged(model);

                //Send the updated menu item details to the other parts of the app
                WeakReferenceMessenger.Default.Send(MenuItemChangedMessage.From(model));
                Cancel();
            }

            IsLoading = false;
        }

        private void HandleMenuItemChanged(MenuItemModel model)
        {
            var menuItem = MenuItems.FirstOrDefault(m => m.Id == model.Id);
            if (menuItem != null)
            {
                //This menu item is on the screen the right now

                //check if the the this still has a mapping to selected category
                if (!model.SelectedCategories.Any(c => c.Id == SelectedCategory.Id))
                {
                    //This item no longer belongs to the selected category
                    //Remove this item from the current UI Menu Items list
                    MenuItems = [.. MenuItems.Where(m => m.Id == model.Id)];
                    return;
                }

                //update the details
                menuItem.Price = model.Price;
                menuItem.Description = model.Description;
                menuItem.Name = model.Name;
                menuItem.Icon = model.Icon;

                MenuItems = [.. MenuItems];
            }
            else if (model.SelectedCategories.Any(c => c.Id == SelectedCategory.Id))
            {
                //This item was not on the UI
                //We updated the item by adding this currently selected category
                //So add this menu item to the current ui menu items list
                var newMenuItem = new MenuItem
                {
                    Id = model.Id,
                    Description = model.Description,
                    Icon = model.Icon,
                    Name = model.Name,
                    Price = model.Price
                };

                MenuItems = [.. MenuItems, newMenuItem];
            }
        }
    }
}
