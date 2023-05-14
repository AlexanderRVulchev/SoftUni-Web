using Library.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService service;

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> All()
        {
            var books = await service.GetAllBooksAsync();
            return View(books);
        }
    }
}
