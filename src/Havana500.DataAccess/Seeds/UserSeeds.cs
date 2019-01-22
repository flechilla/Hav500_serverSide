using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using SeedEngine.Core;
using Havana500.Domain.Constants;
using System.Collections.Generic;
using Bogus;

namespace Havana500.DataAccess.Seeds
{
    /// <inheritdoc />
    /// Contains the implementation of the seeds for 
    /// the
    /// <see cref="ApplicationUser" />
    public class UserSeeds : ISeed<Havana500DbContext>
    {
        /// <inheritdoc />
        public int OrderToByApplied => 1;

        public void AddOrUpdate(Havana500DbContext context, int amountOfObjects = 20)
        {
            if (context.Users.Any())
                return;

            var mainUser = new ApplicationUser
            {
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                UserName = "a.flechilla",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                Role = UserRoles.ADMIN,
                PhoneNumber = "+53 360 5812",
                FirstName = "Adriano",
                LastName = "Flechilla"
            };

            var passHasher = new PasswordHasher<ApplicationUser>();

            var hashedPass = passHasher.HashPassword(mainUser, "admin123");

            mainUser.PasswordHash = hashedPass;

            var users = GenerateUsers();

            foreach (var user in users)
            {
                user.PasswordHash = hashedPass;
            }

            context.Users.Add(mainUser);
            context.Users.AddRange(GenerateUsers());
            context.SaveChanges();
        }

        private List<ApplicationUser> GenerateUsers()
        {
            var roles = new List<string>()
            {
                UserRoles.ADMIN,
                UserRoles.COMMMENT_MODERATOR,
                UserRoles.EDITOR
            };
            var userGenerator = new Faker<ApplicationUser>()
                .RuleFor(u => u.PhoneNumber, (f, a) => f.Phone.PhoneNumber())
                .RuleFor(u => u.LastName, (f, a) => f.Name.LastName())
                .RuleFor(u => u.FirstName, (f, a) => f.Name.FirstName())
                .RuleFor(u => u.UserName, (f, a) => $"{a.FirstName} {a.LastName}")
                .RuleFor(u => u.Email, (f, a) => f.Person.Email)
                .RuleFor(u => u.NormalizedEmail, (f, a) => a.Email.ToUpper())
                .RuleFor(u => u.NormalizedUserName, (f, a) => a.UserName.ToUpper())
                .RuleFor(u => u.Role, (f, a) => f.Random.ListItem(roles))
                .RuleFor(u => u.EmailConfirmed, (f, a) => f.Random.Bool())
                .RuleFor(u => u.SecurityStamp, Guid.NewGuid().ToString());


            return userGenerator.Generate(20);


        }
    }
}