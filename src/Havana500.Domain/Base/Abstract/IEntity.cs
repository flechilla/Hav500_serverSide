using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <summary>
    ///     Declare the PK for all the entities of the system.
    /// </summary>
    /// <typeparam name="TKey">The type of the PK</typeparam>
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
