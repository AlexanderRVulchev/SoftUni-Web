using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    using static Data.DataConstants.User;

    public class ApplicationUser : IdentityUser
    {      
        public ApplicationUser() : base()
        {
            ApplicationUsersBooks = new List<ApplicationUserBook>();
        }

        [Required]
        [StringLength(UserNameMaxLength)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [Required]
        [StringLength(EmailMaxLength)]
        public override string Email { get => base.Email; set => base.Email = value; }

        public ICollection<ApplicationUserBook> ApplicationUsersBooks;
    }
}
