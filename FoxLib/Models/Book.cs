using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace FoxLib.Models
{
    public enum ReadingStatus
    {
        New,
        Active,
        Finished
    }

    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string CoverImageUrl { get; set; }

        public ReadingStatus Status { get; set; }
    }
}
