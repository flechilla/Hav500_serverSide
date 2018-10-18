using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain.Models.Media;

namespace Havana500.Domain
{
    /// <summary>
    ///     Pivot table for the relation between
    ///     <see cref="Picture"/> and <see cref="ContentTag"/>
    /// </summary>
    public class PictureContentTag
    {
        /// <summary>
        ///     Gets or sets the FK of the Picture
        /// </summary>
        public int PictureId { get; set; }

        public Picture Picture { get; set; }


        /// <summary>
        ///     Gets or sets the FK of ContentTag
        /// </summary>
        public int ContentTagId { get; set; }

        public ContentTag ContentTag { get; set; }
    }
}
