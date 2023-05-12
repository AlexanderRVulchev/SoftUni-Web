﻿using Contacts.Data.Entities;
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
            /* builder
                .Entity<Contact>()
                .HasData(new Contact()
                {
                    Id = 1,
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    PhoneNumber = "+359881223344",
                    Address = "Gotham City",
                    Email = "imbatman@batman.com",
                    Website = "www.batman.com"
                });
            */
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