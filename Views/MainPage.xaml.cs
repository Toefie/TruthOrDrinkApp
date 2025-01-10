using TruthOrDrinkApp.ViewModels;

namespace TruthOrDrinkApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}


