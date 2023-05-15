using Library.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public async Task<IActionResult> Mine()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var books = await service.GetCurrentUserBooksCollectionAsync(userId);
            return View(books);
        }
    }
}
