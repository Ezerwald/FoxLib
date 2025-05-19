using System.Collections.ObjectModel;

namespace FoxLib;

public partial class LibraryPage : ContentPage
{
    public ObservableCollection<string> FilterTabs { get; set; } = new() { "All", "Active", "Finished", "New" };
    public LibraryPage()
	{
        InitializeComponent();
        BindingContext = this;
    }
}