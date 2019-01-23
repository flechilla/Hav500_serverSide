using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models.SystemUsers
{
    public class UserIndexViewModel : BaseUserViewMode
    {
        public string UserName { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string UserImageHRef { get; set; }
    }
}
