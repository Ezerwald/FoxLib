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

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        if (BindingContext is SearchViewModel vm)
        {
            vm.SearchCommand.Execute(null);
        }
    }
}
