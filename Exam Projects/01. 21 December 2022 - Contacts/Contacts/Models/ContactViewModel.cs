using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    using static Data.DataConstants.Contact;

    public class ContactViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;
        
        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;
        
        [Required]
        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }
        
        [Required]
        [RegularExpression(WebsiteRegex)]
        public string Website { get; set; } = null!;
    }
}
