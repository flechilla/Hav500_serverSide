using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Havana500.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }

  

    }
}
