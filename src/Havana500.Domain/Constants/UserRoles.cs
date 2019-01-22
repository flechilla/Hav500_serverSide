using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Constants
{
    /// <summary>
    ///     Contains the definition for the multiple
    ///     roles in the system.
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        ///     Indicates that the user has the max
        ///     permissions
        /// </summary>
        public const string ADMIN = "Admin";

        /// <summary>
        ///     Indicates that the user has permission to 
        ///     Create and Modify content
        /// </summary>
        public const string EDITOR = "Editor";

        /// <summary>
        ///     Indicate that the user has permission to 
        ///     moderate the comments.
        /// </summary>
        public const string COMMMENT_MODERATOR = "CommentModerator";

    }
}
