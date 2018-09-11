using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models
{
    public class ContentTagViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }

        /// <summary>
        ///     This is just needed for the admin area.
        /// </summary>
        public int AmountOfContent { get; set; }
    }
}
