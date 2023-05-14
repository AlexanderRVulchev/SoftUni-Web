﻿namespace Library.Contracts
{
    using Models.Book;

    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();
    }
}
