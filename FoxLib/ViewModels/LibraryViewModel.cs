using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using System.Windows.Input;
using FoxLib.Models;
using System.Threading.Tasks;

namespace FoxLib.ViewModels
{
    public class LibraryViewModel : BaseViewModel
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        public ICommand LoadBooksCommand { get; }
        public ICommand AddDummyBookCommand { get; }

        public LibraryViewModel()
        {
            LoadBooksCommand = new Command(async () => await LoadBooksAsync());
            AddDummyBookCommand = new Command(async () => await AddDummyBookAsync());
        }

        private async Task LoadBooksAsync()
        {
            Books.Clear();
            var books = await App.Database.GetBooksAsync();
            foreach (var book in books)
                Books.Add(book);
        }
        
        private async Task AddDummyBookAsync()
        {
            var dummyBook = new Book
            {
                Title = "Sample Book",
                Author = "Unknown Author",
                Description = "This is a sample book for testing.",
                CoverImageUrl = "", // Empty to test placeholder later
                Status = ReadingStatus.New
            };

            await App.Database.SaveBookAsync(dummyBook);
            await LoadBooksAsync();
        }

        public async Task LoadBooksByStatusAsync(ReadingStatus? status)
        {
            Books.Clear();

            var books = status.HasValue
                ? await App.Database.GetBooksByStatusAsync(status.Value)
                : await App.Database.GetBooksAsync();

            foreach (var book in books)
                Books.Add(book);
        }

    }
}

