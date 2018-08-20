using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <inheritdoc />
    public class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
