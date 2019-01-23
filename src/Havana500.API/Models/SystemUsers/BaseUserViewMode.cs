using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models.SystemUsers
{
    public class BaseUserViewMode : BaseViewModel<string>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }
    }
}
