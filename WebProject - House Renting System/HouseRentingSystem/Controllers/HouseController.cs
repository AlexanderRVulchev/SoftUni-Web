using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HouseRentingSystem.Controllers
{
    using Core.Models.House;

    [Authorize]
    public class HouseController : Controller
    {
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

        public async Task<IActionResult> Edit(int id, HouseModel model)
        {
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
