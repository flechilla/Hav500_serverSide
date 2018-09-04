using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models
{
    public class BaseViewModel<TKey>
    {
        /// <summary>
        ///     Gets or sets the Id of the current entity.
        /// </summary>
        public TKey Id { get; set; }
    }
}
