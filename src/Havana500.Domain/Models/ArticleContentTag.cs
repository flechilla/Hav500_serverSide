using Havana500.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain
{
    /// <summary>
    ///     Pivot table for the relation between
    ///     <see cref="Article"/> and <see cref="ContentTag"/>
    /// </summary>
    public class ArticleContentTag
    {
        /// <summary>
        ///     Gets or sets the FK of the Article
        /// </summary>
        public int ArticleId { get; set; }

        public Article Article { get; set; }


        /// <summary>
        ///     Gets or sets the FK of ContentTag
        /// </summary>
        public int ContentTagId { get; set; }

        public ContentTag ContentTag { get; set; }
    }
}
