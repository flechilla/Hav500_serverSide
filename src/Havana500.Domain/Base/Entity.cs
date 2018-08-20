using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <inheritdoc />
    public class Entity<TKey> : IEntity<TKey>
    {
        /// <summary>
        ///     Gets or sets the PK of the entity
        /// </summary>
        public TKey Id { get; set; }
    }
}
