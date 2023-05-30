using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    using Core.Models.House;

    public class HouseController : Controller
    {
        public IActionResult All()
        {
            var model = new AllHousesQueryModel();

            return View(model);
        }
    }
}
