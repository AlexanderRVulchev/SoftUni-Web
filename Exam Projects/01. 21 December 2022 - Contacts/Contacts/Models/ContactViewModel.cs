using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    using static Data.DataConstants.Contact;

    public class ContactViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;
        
        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;
        
        [Required]
        [RegularExpression(PhoneNumberRegex)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }
        
        [Required]
        [RegularExpression(WebsiteRegex)]
        public string Website { get; set; } = null!;
    }
}
