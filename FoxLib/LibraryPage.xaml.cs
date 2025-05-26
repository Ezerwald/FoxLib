using System.Collections.ObjectModel;

namespace FoxLib;

public partial class LibraryPage : ContentPage
{
    private Button _selectedTab;

    public LibraryPage()
    {
        InitializeComponent();
        SelectTab(TabAll); // default selected tab
    }

    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender is Button clickedTab)
        {
            SelectTab(clickedTab);

            // Optionally filter your book list based on clickedTab.Text
            string selectedCategory = clickedTab.Text;
            Console.WriteLine($"Filtering for: {selectedCategory}");
        }
    }

    private void SelectTab(Button newTab)
    {
        if (_selectedTab != null)
        {
            // Reset old tab style
            _selectedTab.BackgroundColor = Application.Current.Resources["White"] as Color ?? Colors.White;
            _selectedTab.TextColor = Application.Current.Resources["Black"] as Color ?? Colors.Black;
        }

        // Highlight new tab
        newTab.BackgroundColor = Application.Current.Resources["Primary"] as Color ?? Colors.Blue;
        newTab.TextColor = Application.Current.Resources["White"] as Color ?? Colors.White;

        _selectedTab = newTab;
    }
}