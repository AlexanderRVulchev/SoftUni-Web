using Contacts.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ContactsDbContext : IdentityDbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
            this.Database.Migrate();            
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<ApplicationUserContact> ApplicationUsersContacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserContact>(entity =>
            {
                entity.HasKey(auc => new { auc.ContactId, auc.ApplicationUserId });
            });

            base.OnModelCreating(builder);
        }
    }
}