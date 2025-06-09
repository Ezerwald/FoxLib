using System.Collections.ObjectModel;
using System.Windows.Input;
using FoxLib.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Linq;

namespace FoxLib.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Book> RecommendedBooks { get; set; } = new ObservableCollection<Book>();

        public ICommand BookTappedCommand { get; }

        public MainViewModel()
        {
            BookTappedCommand = new Command<Book>(async (book) => await NavigateToRecommendationAsync(book));
            LoadRecommendationsAsync(); // 🔄 Call the async version here
        }

        private async void LoadRecommendationsAsync()
        {
            try
            {
                var httpClient = new HttpClient();
                string topic = "bestseller fiction"; // You can change this topic to e.g. "top science fiction", "fantasy", etc.
                var url = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(topic)}&maxResults=10";

                var response = await httpClient.GetStringAsync(url);
                var result = JsonDocument.Parse(response);

                RecommendedBooks.Clear();

                if (result.RootElement.TryGetProperty("items", out JsonElement items))
                {
                    foreach (var item in items.EnumerateArray())
                    {
                        var volumeInfo = item.GetProperty("volumeInfo");

                        var book = new Book
                        {
                            Title = volumeInfo.GetPropertyOrDefault("title", "No title"),
                            Author = volumeInfo.TryGetProperty("authors", out JsonElement authorsElement) && authorsElement.ValueKind == JsonValueKind.Array
                                ? string.Join(", ", authorsElement.EnumerateArray().Select(a => a.GetString()))
                                : "Unknown author",
                            Description = volumeInfo.GetPropertyOrDefault("description", "No description"),
                            CoverImageUrl = EnsureHttps(volumeInfo.GetNestedPropertyOrDefault("imageLinks", "thumbnail", "")),
                            Status = ReadingStatus.New
                        };

                        RecommendedBooks.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally show a user-friendly error
                Console.WriteLine($"Error loading recommendations: {ex.Message}");
            }
        }

        private string EnsureHttps(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "";

            return url.StartsWith("http://") ? url.Replace("http://", "https://") : url;
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
