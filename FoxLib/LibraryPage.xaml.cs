using System.Collections.ObjectModel;
using FoxLib.Models;
using FoxLib.ViewModels;

namespace FoxLib;

public partial class LibraryPage : ContentPage
{
    private Button _selectedTab;
    private LibraryViewModel viewModel;

    public LibraryPage()
    {
        InitializeComponent();
        SelectTab(TabAll); // default selected tab

        BindingContext = viewModel = new LibraryViewModel();
    }


    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender is Button clickedTab)
        {
            SelectTab(clickedTab);

            string selectedCategory = clickedTab.Text;
            Console.WriteLine($"Filtering for: {selectedCategory}");

            ReadingStatus? status = selectedCategory switch
            {
                "All" => null,
                "New" => ReadingStatus.New,
                "Active" => ReadingStatus.Active,
                "Finished" => ReadingStatus.Finished,
                _ => null
            };

            _ = viewModel.LoadBooksByStatusAsync(status);
        }
    }


    private void SelectTab(Button newTab)
    {
        if (_selectedTab != null)
        {
            // Reset to default filter tab style
            _selectedTab.Style = Application.Current.Resources["FilterTabButtonStyle"] as Style;
        }

        // Apply selected style
        newTab.Style = Application.Current.Resources["SelectedFilterTabButtonStyle"] as Style;

        _selectedTab = newTab;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LoadBooksCommand.Execute(null);
    }
}