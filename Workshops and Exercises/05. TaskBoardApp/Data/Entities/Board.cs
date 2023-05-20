using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Entities
{
    using static Data.DataConstants.Board;

    public class Board
    {
        public Board()
        {
            Tasks = new List<Task>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxBoardName, MinimumLength = MinBoardName)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; } = null!;
    }
}
