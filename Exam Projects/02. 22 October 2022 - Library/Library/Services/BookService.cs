using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    using Contracts;
    using Data;
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
                    Category = b.Category.Name,
                    Description = b.Description
                });

            return models;
        }

        public async Task<IEnumerable<BookViewModel>> GetCurrentUserBooksCollectionAsync(string userId)
        {
            var entities = await context.Books
                .Include(b => b.ApplicationUsersBooks)
                .Include(b => b.Category)
                .Where(b => b.ApplicationUsersBooks
                        .Any(aub => aub.ApplicationUserId == userId))
                .ToArrayAsync();

            var models = entities
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Category = b.Category.Name,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Title = b.Title,
                    Description = b.Description
                });

            return models;
        }

        public async Task<IEnumerable<CategoryItemModel>> GetAllCategoriesAsync()
        {
            var entities = await context.Categories.ToArrayAsync();

            var models = entities.Select(e => new CategoryItemModel
            {
                Id = e.Id,
                Name = e.Name,
            });

            return models;
        }
    }
}
