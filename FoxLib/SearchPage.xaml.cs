using FoxLib.ViewModels;

namespace FoxLib;

public partial class SearchPage : ContentPage
{
    private readonly SearchViewModel viewModel;

    public SearchPage()
    {
        InitializeComponent();
        viewModel = new SearchViewModel();
        BindingContext = viewModel;
    }
}
