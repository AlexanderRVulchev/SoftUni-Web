using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Contacts.Data.Entities
{
    using static DataConstants.User;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ApplicationUsersContacts = new List<ApplicationUserContact>();
        }

        [Required]
        [MaxLength(UserNameMaxLength)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public override string Email { get => base.Email; set => base.Email = value; }

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; } = null!;
    }
}
