using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class BoardsController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public BoardsController(TaskBoardAppDbContext context)
        {
            this.data = context;
        }

        public IActionResult All()
        {
            var boards = data.Boards
                .Select(b => new BoardViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = data.Tasks
                        .Where(t => t.BoardId == b.Id)
                        .Select(t => new TaskViewModel
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Owner = t.Owner.UserName
                        })
                    .ToList()
                })
                .ToList();

            return View(boards);
        }
    }
}
