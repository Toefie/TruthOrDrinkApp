using TruthOrDrinkApp.Services;
using TruthOrDrinkApp.ViewModels;
using TruthOrDrinkApp.Views;

namespace TruthOrDrinkApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "truthordrink.db");

        builder.Services.AddSingleton(new DatabaseService(dbPath));
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();


        return builder.Build();
    }
}