using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models.SystemUsers
{
    public class UserUpdateViewModel : BaseUserViewMode
    {
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}
