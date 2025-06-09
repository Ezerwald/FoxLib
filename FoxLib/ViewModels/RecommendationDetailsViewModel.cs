using FoxLib.Models;
using System.Windows.Input;
using System.Threading.Tasks;

namespace FoxLib.ViewModels
{
    public class RecommendationDetailsViewModel : BaseViewModel
    {
        public Book Book { get; set; }

        public ICommand AddToLibraryCommand { get; }

        public RecommendationDetailsViewModel(Book book)
        {
            Book = book;
            AddToLibraryCommand = new Command(async () => await AddToLibraryAsync());

            // Optional: Set default status
            Book.Status = ReadingStatus.New;
        }

        private async Task AddToLibraryAsync()
        {
            await App.Database.SaveBookAsync(Book);
            await Shell.Current.DisplayAlert("Success", "Book added to your library!", "OK");
        }

        // Optional Binding Shortcut
        public string Title => Book.Title;
        public string Author => Book.Author;
        public string Description => Book.Description;
        public string CoverImageUrl => Book.CoverImageUrl;
    }
}
