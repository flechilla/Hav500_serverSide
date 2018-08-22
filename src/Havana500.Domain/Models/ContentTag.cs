using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain.Base;

namespace Havana500.Domain
{
    /// <summary>
    ///     Represent a tag that can be assigned to any
    ///     content (i.e Section, Article...)
    /// </summary>
    public class ContentTag : AuditableAndTrackableEntity<int>
    {
        /// <summary>
        ///     Gets or sets the name of the Tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the amount
        ///     of times that the tag has been used.
        /// </summary>
        public int AmountOfContent { get; set; }

        /// <summary>
        ///     Gets or sets the articles that contains this tag.
        /// </summary>
        public ICollection<Article> Articles { get; set; }
    }
}
