using CommunityToolkit.Mvvm.ComponentModel;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastaurantPosMAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        public MenuCategoryModel[] _categories = [];

        [ObservableProperty]
        private bool _isLoading;

        public HomeViewModel(DatabaseService databaseService) 
        { 
            _databaseService = databaseService;
        }

        private bool _isIntialized;

        public async ValueTask InitializeAsyn()
        {
            if (_isIntialized)
                return;

            _isIntialized = true;
             
            IsLoading = true;

            Categories =(await _databaseService.GetMenuCategoriesAsync())
                .Select(MenuCategoryModel.FromEntity).ToArray();

            IsLoading = false;
        }
    }
}
