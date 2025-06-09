using FoxLib.Models;
using FoxLib.ViewModels;

namespace FoxLib;

[QueryProperty(nameof(SelectedBook), "SelectedBook")]
public partial class RecommendationDetailsPage : ContentPage
{
    public Book SelectedBook
    {
        get => BindingContext as Book;
        set
        {
            BindingContext = new RecommendationDetailsViewModel(value);
        }
    }

    public RecommendationDetailsPage()
    {
        InitializeComponent();
    }
}
