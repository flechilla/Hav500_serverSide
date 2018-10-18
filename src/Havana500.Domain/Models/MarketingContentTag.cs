using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain
{
    /// <summary>
    ///     Pivot table for the relation between
    ///     <see cref="MarketingContent"/> and <see cref="ContentTag"/>
    /// </summary>
    public class MarketingContentTag
    {
        /// <summary>
        ///     Gets or sets the FK of the MarketingContent
        /// </summary>
        public int MarketingContentId { get; set; }

        public MarketingContent MarketingContent { get; set; }

        /// <summary>
        ///     Gets or sets the FK of ContentTag
        /// </summary>
        public int ContentTagId { get; set; }

        public ContentTag ContentTag { get; set; }
    }
}
