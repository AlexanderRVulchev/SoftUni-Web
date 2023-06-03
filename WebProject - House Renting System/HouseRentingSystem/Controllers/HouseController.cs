using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HouseRentingSystem.Controllers
{
    using Core.Models.House;
    using Core.Contracts;

    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService houseService;

        public HouseController(IHouseService _houseService)
        {
            this.houseService = _houseService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = new HouseQueryModel();

            return View(model);
        }


        public async Task<IActionResult> Mine()
        {
            var model = new HouseQueryModel();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = new HouseDetailsModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(HouseModel model)
        {
            int id = 1;
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new HouseModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseModel model)
        {
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
