using System.Collections.ObjectModel;

namespace FoxLib;

public partial class LibraryPage : ContentPage
{
    public ObservableCollection<string> FilterTabs { get; set; } = [];
    public int FilterTabsCount;
    public LibraryPage()
    {
        FilterTabs = ["All", "Active", "Finished", "New"];

        InitializeComponent();
        BindingContext = this;
    }
}