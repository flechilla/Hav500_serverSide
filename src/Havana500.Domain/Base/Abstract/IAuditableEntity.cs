using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <summary>
    ///     Declare the properties for the AuditableEntities.
    ///     This is to track the the user that modify and create 
    ///     some entity
    /// </summary>
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }
    }
}
