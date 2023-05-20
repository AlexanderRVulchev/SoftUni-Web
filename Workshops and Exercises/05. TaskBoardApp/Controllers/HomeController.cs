using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public HomeController(TaskBoardAppDbContext context)
        {
            this.data = context;
        }

        public IActionResult Index()
        {
            List<string> taskBoards = this.data.Boards.Select(b => b.Name).Distinct().ToList();
            var tasksCount = new List<HomeBoardModel>();
            foreach (var boardName in taskBoards)
            {
                int tasksInBoard = this.data.Tasks.Where(t => t.Board.Name == boardName).Count();
                tasksCount.Add(new HomeBoardModel
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (this.User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = this.data.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel
            {
                AllTasksCount = this.data.Tasks.Count(),
                BoardsWithTasksCount = tasksCount,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}