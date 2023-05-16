using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    using Contracts;
    using Data;
    using Data.Entities;
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

        public async Task AddNewBookAsync(BookFormModel model)
        {
            var book = new Book
            {
                Author = model.Author,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Title = model.Title
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();
        }

        public async Task AddBookToUserCollectionAsync(string userId, int bookId)
        {
            var book = await context.Books
                .Include(b => b.ApplicationUsersBooks)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                    ?? throw new ArgumentNullException(nameof(bookId), "Invalid book id.");

            if (!book.ApplicationUsersBooks.Any(aub => aub.ApplicationUserId == userId))
            {
                book.ApplicationUsersBooks.Add(new ApplicationUserBook
                {
                    ApplicationUserId = userId
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveBookFromUserCollectionAsync(string userId, int bookId)
        {
            var book = await context.Books
                .Include(b => b.ApplicationUsersBooks)
                .FirstOrDefaultAsync(b => b.Id == bookId)
                    ?? throw new ArgumentNullException(nameof(bookId), "Invalid book id.");

            var entityToRemove = book.ApplicationUsersBooks
                .FirstOrDefault(aub => aub.ApplicationUserId == userId);
                    
            if (entityToRemove != null)
            {
                book.ApplicationUsersBooks.Remove(entityToRemove);
            }

            await context.SaveChangesAsync();
        }
    }
}
