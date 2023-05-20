using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardApp.Data.Entities
{
    using static Data.DataConstants.Task;

    public class Task
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(MaxTaskTitle)]
        public string Title { get; set; } = null!;
        
        [Required]
        [MaxLength(MaxTaskDescription)]
        public string Description { get; set; } = null!;
        
        public DateTime CreatedOn { get; set; }
                
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;
                
        public string OwnerId { get; set; } = null!;
        public User Owner { get; set; } = null!;
    }
}
