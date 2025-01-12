using TruthOrDrinkApp.Views;
using TruthOrDrinkApp.Services;
using TruthOrDrinkApp.ViewModels;
using Microsoft.Maui.Hosting;

namespace TruthOrDrinkApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Stel de startpagina in
            MainPage = new NavigationPage(new MainPage());
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureServices();

            return builder.Build();
        }
    }

    // Extensiemethode voor serviceconfiguratie
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            // Registreer services en viewmodels
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<MainViewModel>();

            // Registreer de hoofdpagina met viewmodel
            builder.Services.AddTransient<MainPage>(provider =>
            {
                var viewModel = provider.GetRequiredService<MainViewModel>();
                return new MainPage { BindingContext = viewModel };
            });

            return builder;
        }
    }
}