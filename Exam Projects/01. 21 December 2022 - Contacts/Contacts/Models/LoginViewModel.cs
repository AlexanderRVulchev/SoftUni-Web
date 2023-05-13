using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    using static Data.DataConstants.User;

    public class LoginViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;
    }
}
