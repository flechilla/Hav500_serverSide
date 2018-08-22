using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain.Base;

namespace Havana500.Domain
{
    public abstract class UserContent : TrackableEntity<int>
    {
        /// <summary>
		/// Gets or sets the IP address
		/// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content is approved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the Id of the user.
        /// <remarks>
        ///     This value might be null if the user is anonymous.    
        /// </remarks>
        /// </summary>
        /// 
        public string ApplicationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the reference to the user.
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }
    }
}
