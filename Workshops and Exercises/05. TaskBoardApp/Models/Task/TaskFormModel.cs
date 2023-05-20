using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Models.Task
{
    using static Data.DataConstants.Task;

    public class TaskFormModel
    {
        public TaskFormModel()
        {
            Boards = new List<TaskBoardModel>();
        }

        [Required]
        [StringLength(MaxTaskTitle, MinimumLength = MinTaskTitle, ErrorMessage = "Title should be at least {2} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxTaskDescription, MinimumLength = MinTaskDescription, ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; set; } = null!;

        public int BoardId { get; set; }

        public ICollection<TaskBoardModel> Boards { get; set; }
    }
}
