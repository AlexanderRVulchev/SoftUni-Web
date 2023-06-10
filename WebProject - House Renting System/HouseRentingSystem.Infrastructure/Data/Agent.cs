using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRentingSystem.Infrastructure.Data
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;
        
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
