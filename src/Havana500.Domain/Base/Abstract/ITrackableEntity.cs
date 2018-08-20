using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Base
{
    /// <summary>
    ///     Declare the properties to track the time of 
    ///     the creation and modification of the entities.
    /// </summary>
    public interface ITrackableEntity
    {
        /// <summary>
        ///     Gets or sets the time of creation of this entity
        /// </summary>
        DateTime CreatedAt { get; set; }


        /// <summary>
        ///     Gets or sets the time of modification of this entity
        /// </summary>
        DateTime ModifiedAt { get; set; }
    }
}
