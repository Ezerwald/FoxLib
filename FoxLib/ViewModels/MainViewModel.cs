using System.Collections.ObjectModel;
using System.Windows.Input;
using FoxLib.Models;
using System.Threading.Tasks;

namespace FoxLib.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Book> RecommendedBooks { get; set; } = new ObservableCollection<Book>();

        public ICommand BookTappedCommand { get; }

        public MainViewModel()
        {
            LoadRecommendations();
            BookTappedCommand = new Command<Book>(async (book) => await NavigateToRecommendationAsync(book));
        }

        private void LoadRecommendations()
        {
            // You can later replace this with a real API or smarter logic
            RecommendedBooks.Add(new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", CoverImageUrl = "", Description = "A fantasy novel..." });
            RecommendedBooks.Add(new Book { Title = "1984", Author = "George Orwell", CoverImageUrl = "", Description = "Dystopian classic..." });
            RecommendedBooks.Add(new Book { Title = "Dune", Author = "Frank Herbert", CoverImageUrl = "", Description = "Epic science fiction..." });
            RecommendedBooks.Add(new Book { Title = "Foundation", Author = "Isaac Asimov", CoverImageUrl = "", Description = "Pioneering sci-fi series..." });
        }

        private async Task NavigateToRecommendationAsync(Book book)
        {
            if (book != null)
            {
                await Shell.Current.GoToAsync(nameof(RecommendationDetailsPage), true, new Dictionary<string, object>
                {
                    { "SelectedBook", book }
                });
            }
        }
    }
}
