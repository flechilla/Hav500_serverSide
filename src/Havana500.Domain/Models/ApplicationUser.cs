using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Havana500.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserImageHRef { get; set; }

        public string UserImageLocalPath { get; set; }

        /// <summary>
        ///     Gets or sets the value that represents the user's role
        /// </summary>
        /// <remarks>
        ///     Because this is a pretty simple app, we'll have only one
        ///     role per user.
        /// </remarks>
        public string Role { get; set; }
    }
}
