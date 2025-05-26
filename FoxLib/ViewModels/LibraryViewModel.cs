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
        }

        public ICommand LoadBooksCommand { get; }

        private async Task LoadBooksAsync()
        {
            Books.Clear();
            var books = await App.Database.GetBooksAsync();
            foreach (var book in books)
                Books.Add(book);
        }
    }
}

