using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Models.Media
{
    /// <summary>
    ///     Defines the memeber to be implemented by
    ///     a class that represent a media file.
    /// </summary>
    public interface IHasMedia
    {
        /// <summary>
		/// Gets or sets the media storage identifier
		/// </summary>
		int? MediaStorageId { get; set; }

        /// <summary>
        /// Gets or sets the media storage
        /// </summary>
        MediaStorage MediaStorage { get; set; }
    }
}
