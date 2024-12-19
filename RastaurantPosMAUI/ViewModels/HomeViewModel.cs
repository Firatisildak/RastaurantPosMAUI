using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Models;

namespace RastaurantPosMAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private MenuCategoryModel[] _categories = [];

        [ObservableProperty]
        private MenuCategoryModel? _selectedCategory = null;

        [ObservableProperty]
        private bool _isLoading;

        public HomeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

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

            IsLoading = false;
        }

        [RelayCommand]
        private void SelectCategory(int categoryId)
        {
            if (SelectedCategory.Id == categoryId)
                return;
            var existingSelectedCategory = Categories.First(c => c.IsSelected);
            existingSelectedCategory.IsSelected = false;

            var newlySelectedCategory = Categories.First(c => c.Id == categoryId);
            newlySelectedCategory.IsSelected = true;

            SelectedCategory= newlySelectedCategory;
        }

    }
}
