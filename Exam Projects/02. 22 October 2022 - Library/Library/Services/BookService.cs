using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Book;

    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var entities = await context.Books
                .Include(b => b.Category)
                .ToArrayAsync();

            var models = entities
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Title = b.Title,
                    Category = b.Category.Name
                });

            return models;
        }
    }
}
