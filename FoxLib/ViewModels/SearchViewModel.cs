using FoxLib.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoxLib.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<Book> SearchResults { get; set; } = new ObservableCollection<Book>();

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                // Optionally trigger search on text change
                // await SearchBooksAsync();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddToLibraryCommand { get; }

        public SearchViewModel()
        {
            SearchCommand = new Command(async () => await SearchBooksAsync());
            AddToLibraryCommand = new Command<Book>(async (book) => await AddToLibraryAsync(book));
        }

        private async Task SearchBooksAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return;

            var httpClient = new HttpClient();
            var url = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(SearchText)}";

            var response = await httpClient.GetStringAsync(url);
            var result = JsonDocument.Parse(response);

            SearchResults.Clear();

            if (result.RootElement.TryGetProperty("items", out JsonElement items))
            {
                foreach (var item in items.EnumerateArray())
                {
                    var volumeInfo = item.GetProperty("volumeInfo");

                    var book = new Book
                    {
                        Title = volumeInfo.GetPropertyOrDefault("title", "No title"),
                        Author = volumeInfo.GetPropertyOrDefault("authors", "Unknown author"),
                        Description = volumeInfo.GetPropertyOrDefault("description", "No description"),
                        CoverImageUrl = volumeInfo.GetNestedPropertyOrDefault("imageLinks", "thumbnail", ""),
                        Status = ReadingStatus.New
                    };

                    SearchResults.Add(book);
                }
            }
        }

        private async Task AddToLibraryAsync(Book book)
        {
            await App.Database.SaveBookAsync(book);
        }
    }

    public static class JsonHelpers
    {
        public static string GetPropertyOrDefault(this JsonElement element, string propertyName, string defaultValue)
        {
            return element.TryGetProperty(propertyName, out JsonElement prop) ? prop.ToString() : defaultValue;
        }

        public static string GetNestedPropertyOrDefault(this JsonElement element, string parent, string child, string defaultValue)
        {
            if (element.TryGetProperty(parent, out JsonElement parentElement) &&
                parentElement.TryGetProperty(child, out JsonElement childElement))
                return childElement.ToString();
            return defaultValue;
        }
    }
}
