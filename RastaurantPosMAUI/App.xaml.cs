using RastaurantPosMAUI.Data;

namespace RastaurantPosMAUI
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;

        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            _databaseService = databaseService;
        }

        protected override async void OnStart()
        {
            base.OnStart();
            //Initialzie and Seed Databse
            await _databaseService.InitializeDatabaseAsync();
        }
    }
}
