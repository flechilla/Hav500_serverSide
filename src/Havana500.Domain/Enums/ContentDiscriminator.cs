using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Enums
{
    /// <summary>
    ///     Contains the values to discriminate the different
    ///     types of content in the app.
    /// </summary>
    public enum ContentDiscriminator
    {
        Section, 
        Article, 
        Event, 
        Comment
    }
}
