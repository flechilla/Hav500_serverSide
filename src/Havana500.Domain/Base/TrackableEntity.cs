using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <inheritdoc />
    public class TrackableEntity<TKey> : Entity<TKey>, ITrackableEntity
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the time of creation of this entity
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the time of modification of this entity
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }
}
