using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <inheritdoc />
    /// <summary>
    ///     Entity to track the creator, editor and the time
    ///     of creation and modification of the entities
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class AuditableAndTrackableEntity<TKey> : Entity<TKey>, IAuditableEntity, ITrackableEntity
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
