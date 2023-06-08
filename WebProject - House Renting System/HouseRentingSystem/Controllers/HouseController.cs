using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.House;
    using Extensions;
    using HouseRentingSystem.Models;

    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(
            IHouseService _houseService,
            IAgentService _agentService)
        {
            this.houseService = _houseService;
            this.agentService = _agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var result = await houseService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = result.TotalHousesCount;
            query.Categories = await houseService.AllCategoriesNames();
            query.Houses = result.Houses;

            return View(query);
        }


        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses;
            var userId = User.Id();

            if (await agentService.ExistsById(userId))
            {
                int agentId = await agentService.GetAgentId(userId);
                myHouses = await houseService.AllHousesByAgentId(agentId);
            }
            else
            {
                myHouses = await houseService.AllHousesByUserId(userId);
            }

            return View(myHouses);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {

            if (!await houseService.Exists(id))
            {
                return RedirectToAction(nameof(All));
            }

            var houseModel = await houseService.HouseDetailsbyId(id);

            return View(houseModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await agentService.ExistsById(User.Id()) == false)
            {
                RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            var model = new HouseModel()
            {
                HouseCategories = await houseService.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseModel model)
        {
            if (await agentService.ExistsById(User.Id()) == false)
            {
                RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (await houseService.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.HouseCategories = await houseService.AllCategories();
                return View(model);
            }

            string userId = User.Id();
            int agentId = await houseService.GetAgentId(userId);

            int id = await houseService.Create(model, agentId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await houseService.Exists(id))
            {
                return RedirectToAction(nameof(All));
            }

            if (!await houseService.HasAgentWithId(id, User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HouseDetailsbyId(id);
            var categoryId = await houseService.GetHouseCategoryId(id);

            var model = new HouseModel
            {
                Address = house.Address,
                Description = house.Description,
                CategoryId = categoryId,
                Id = id,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                Title = house.Title,
                HouseCategories = await houseService.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseModel model)
        {
            if (!await houseService.Exists(id))
            {
                model.HouseCategories = await houseService.AllCategories();
                ModelState.AddModelError("", "House does not exist");
                return View(model);
            }

            if (!await houseService.HasAgentWithId(id, User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (!await houseService.CategoryExists(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.HouseCategories = await houseService.AllCategories();
                return View(model);
            }

            await houseService.Edit(id, model);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await houseService.Exists(id))
            {
                return RedirectToAction(nameof(All));
            }

            if (!await houseService.HasAgentWithId(id, User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await houseService.HouseDetailsbyId(id);
            var model = new HouseDetailsViewModel
            {
                Address = house.Address,
                Id = id,
                ImageUrl = house.ImageUrl,
                Title = house.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if (!await houseService.Exists(model.Id))
            {
                return RedirectToAction(nameof(All));
            }

            if (!await houseService.HasAgentWithId(model.Id, User.Id()))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await houseService.Delete(model.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (!await houseService.Exists(id))
            {
                return BadRequest();
            }

            if (await agentService.ExistsById(User.Id()))
            {
                return Unauthorized();
            }

            if (await houseService.IsRented(id))
            {
                return BadRequest();
            }

            await houseService.Rent(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
