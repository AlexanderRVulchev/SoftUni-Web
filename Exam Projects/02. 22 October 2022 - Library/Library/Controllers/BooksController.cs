using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    using Contracts;
    using Models.Book;

    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await service.GetAllCategoriesAsync();

            var model = new BookFormModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel model)
        {
            var validCategories = await service.GetAllCategoriesAsync();

            if (!validCategories.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Invalid category Id");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = validCategories;
                return View(model);
            }

            await service.AddNewBookAsync(model);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddToCollection(int bookId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await service.AddBookToUserCollection(userId, bookId);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
