namespace Library.Contracts
{
    using Models.Book;

    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();

        Task<IEnumerable<BookViewModel>> GetCurrentUserBooksCollectionAsync(string userId);

        Task<IEnumerable<CategoryItemModel>> GetAllCategoriesAsync();
    }
}
