using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Infrastructure.Data
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; } = null!;
    }
}
