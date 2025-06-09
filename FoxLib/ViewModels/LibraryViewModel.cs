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

        public LibraryViewModel()
        {
            LoadBooksCommand = new Command(async () => await LoadBooksAsync());
            AddDummyBookCommand = new Command(async () => await AddDummyBookAsync());

            ShowBookMenuCommand = new Command<Book>(async (book) => await ShowMenuAsync(book));
            BookTappedCommand = new Command<Book>(async (book) => await NavigateToBookAsync(book));
        }


        public ICommand LoadBooksCommand { get; }
        public ICommand AddDummyBookCommand { get; }

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

        private async Task DeleteBookAsync(Book book)
        {
            if (book != null)
            {
                await App.Database.DeleteBookAsync(book);
                await LoadBooksAsync();
            }
        }

        private async Task UpdateStatusAsync(Book book, ReadingStatus status)
        {
            if (book != null)
            {
                book.Status = status;
                await App.Database.SaveBookAsync(book);
                await LoadBooksAsync();
            }
        }

        public ICommand ShowBookMenuCommand { get; }

        private async Task ShowMenuAsync(Book book)
        {
            string action = await Shell.Current.DisplayActionSheet("Book Options", "Cancel", null,
                "Delete", "Mark as Active", "Mark as Finished", "Mark as New");

            switch (action)
            {
                case "Delete":
                    await DeleteBookAsync(book);
                    break;
                case "Mark as Active":
                    await UpdateStatusAsync(book, ReadingStatus.Active);
                    break;
                case "Mark as Finished":
                    await UpdateStatusAsync(book, ReadingStatus.Finished);
                    break;
                case "Mark as New":
                    await UpdateStatusAsync(book, ReadingStatus.New);
                    break;
            }
        }


        public ICommand BookTappedCommand { get; }

        private async Task NavigateToBookAsync(Book book)
        {
            if (book != null)
            {
                await Shell.Current.GoToAsync(nameof(BookDetailsPage), true, new Dictionary<string, object>
        {
            { "SelectedBook", book }
        });
            }
        }
    }
}

