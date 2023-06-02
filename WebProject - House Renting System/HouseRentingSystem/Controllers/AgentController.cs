namespace HouseRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Models.Agent;
    using Extensions;
    using HouseRentingSystem.Core.Contracts;
    using HouseRentingSystem.Core.Constants;

    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService _agentService)
        {
            this.agentService = _agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await agentService.ExistsById(User.Id()))
            {
                TempData[MessageConstant.ErrorMessage] = "Вече сте агент";
                return RedirectToAction("Index", "Home");
            }

            BecomeAgentModel model = new();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = User.Id();

            if (await agentService.ExistsById(userId))
            {
                TempData[MessageConstant.ErrorMessage] = "Вие вече сте агент";
                return RedirectToAction("Index", "Home");
            }

            if (await agentService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                TempData[MessageConstant.ErrorMessage] = "Телефонът вече съществува";
                return RedirectToAction("Index", "Home");
            }

            if (await agentService.UserHasRents(userId))
            {
                TempData[MessageConstant.ErrorMessage] = "Не трябва да имате наеми за да станете агент";
                return RedirectToAction("Index", "Home");
            }

            await agentService.Create(userId, model.PhoneNumber);

            return RedirectToAction("All", "House");
        }
    }
}
