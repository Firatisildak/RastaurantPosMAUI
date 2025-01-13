using CommunityToolkit.Mvvm.Messaging;
using RastaurantPosMAUI.Models;

namespace RastaurantPosMAUI.ViewModels
{
    public class SettingsViewModel
    {
        private const string NameKey = "name";
        private const string TaxPercentagekey = "tax";

        private bool _isInitialized;
        public async ValueTask InitializeAsync()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            var name = Preferences.Default.Get<string?>(NameKey, null);
            if (name is null)
            {
                do
                {
                    name = await Shell.Current.DisplayPromptAsync("Your name", "Enter your name");
                } while (string.IsNullOrEmpty(name));

                Preferences.Default.Set<string>(NameKey, name);
            }
            WeakReferenceMessenger.Default.Send(NameChangedMessage.From(name));
        }

        public int GetTaxPercentage() => Preferences.Default.Get<int>(TaxPercentagekey, 0);

        public void SetTaxPercentage(int taxPercentage) => Preferences.Default.Set<int>(TaxPercentagekey, taxPercentage);
    }
}
