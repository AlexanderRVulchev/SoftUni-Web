using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    using static Data.DataConstants.User;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
