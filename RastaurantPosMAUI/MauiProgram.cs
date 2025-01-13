using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RastaurantPosMAUI.Data;
using RastaurantPosMAUI.Pages;
using RastaurantPosMAUI.Services;
using RastaurantPosMAUI.ViewModels;

namespace RastaurantPosMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<AppDBContext>();

            // Register Services, ViewModels, and Pages
            builder.Services
                .AddSingleton<DatabaseService>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton<OrdersViewModel>()
                .AddSingleton<OrdersPage>()
                .AddTransient<ManageMenuItemsViewModel>()
                .AddTransient<ManageMenuItemPage>()
                .AddSingleton<SettingsViewModel>();

            return builder.Build();
        }
    }
}
