using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;


namespace TaskBoardApp.Controllers
{
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using TaskBoardApp.Models;

    public class TasksController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public TasksController(TaskBoardAppDbContext context)
        {
            data = context;
        }

        public IActionResult Create()
        {
            TaskFormModel taskModel = new ()
            {
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Create(TaskFormModel taskModel)
        {
            if (!GetBoards().Any(b => b.Id == taskModel.BoardId))
            {
                this.ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            string currentUserId = GetUserId();
            Task task = new()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                BoardId = taskModel.BoardId,
                OwnerId = currentUserId,
                CreatedOn = DateTime.Now
            };

            this.data.Tasks.Add(task);
            this.data.SaveChanges();

            var boards = this.data.Boards;
            return RedirectToAction("All", "Boards");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = data.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel
                {
                    CreatedOn = t.CreatedOn.ToString("dd-MM-yyyy HH:mm"),
                    Board = t.Board.Name,
                    Id = t.Id,
                    Description = t.Description,
                    Owner = t.Owner.UserName,
                    Title = t.Title
                })
                .FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public IActionResult Edit(int id)
        {
            Task? task = data.Tasks.Find(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            var taskModel = new TaskFormModel
            {
                Title = task.Title,
                Description = task.Description,
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskFormModel taskModel)
        {
            Task? task = data.Tasks.Find(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                Unauthorized();
            }

            if (!GetBoards().Any(b => b.Id == task.BoardId))
            {
                this.ModelState.AddModelError(nameof(task.BoardId), "Board does not exist!");
            }

            task.Title = taskModel.Title;
            task.BoardId = taskModel.BoardId;
            task.Description = taskModel.Description;

            this.data.SaveChanges();

            return RedirectToAction("All", "Boards");
        }

        public IActionResult Delete(int id)
        {
            Task task = data.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                Unauthorized();
            }

            var taskModel = new TaskViewModel
            {
                Id = task.Id,
                Description = task.Description,
                Title = task.Title
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Delete(TaskViewModel taskModel)
        {
            Task? task = data.Tasks.Find(taskModel.Id);

            if (task == null)
            {
                BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task?.OwnerId)
            {
                return Unauthorized();
            }

            data.Tasks.Remove(task);
            data.SaveChanges();
            return RedirectToAction("All", "Boards");
        }

        private ICollection<TaskBoardModel> GetBoards()
        {
            return this.data.Boards
                .Select(b => new TaskBoardModel
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
