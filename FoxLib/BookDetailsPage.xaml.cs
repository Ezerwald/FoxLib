using FoxLib.Models;

namespace FoxLib;

[QueryProperty(nameof(SelectedBook), "SelectedBook")]
public partial class BookDetailsPage : ContentPage
{
    private Book _selectedBook;
    public Book SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            BindingContext = _selectedBook;
        }
    }

    public BookDetailsPage()
    {
        InitializeComponent();
    }
}


