using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using FoxLib.Models;

namespace FoxLib.Services
{
    public class BookDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public BookDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Book>().Wait();
        }

        public Task<List<Book>> GetBooksAsync()
        {
            return _database.Table<Book>().ToListAsync();
        }

        public Task<List<Book>> GetBooksByStatusAsync(ReadingStatus status)
        {
            return _database.Table<Book>().Where(b => b.Status == status).ToListAsync();
        }

        public Task<int> SaveBookAsync(Book book)
        {
            if (book.Id != 0)
                return _database.UpdateAsync(book);
            else
                return _database.InsertAsync(book);
        }

        public Task<int> DeleteBookAsync(Book book)
        {
            return _database.DeleteAsync(book);
        }
    }
}
