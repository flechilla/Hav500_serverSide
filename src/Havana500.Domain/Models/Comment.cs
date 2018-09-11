using Havana500.Domain.Base;
using Havana500.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Havana500.Domain
{
    public class Comment : UserContent
    {

        /// <summary>
        /// Gets or sets the body for the Comment
        /// </summary>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the FK to the related <see cref="ParentId"/>
        /// </summary>
        //public int ParentId { get; set; }

        /// <summary>
        ///     Gets or sets the amount of likes for the current comment.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        ///     Gets or sets the amount of dislikes of the current comment
        /// </summary>
        public int Dislikes { get; set; }

        /// <summary>
        /// Gets or sets the discriminator for the parent of the Comment
        /// </summary>
        //public Discriminator ParentDiscriminator { get; set; }

        ///<summary>
        ///     Gets or sets the reference to the Article that is the
        ///     container of this Comment.
        /// </summary>
        public Article Article { get; set; }

        /// <summary>
        ///     Gets or sets the FK to the related Article
        /// </summary>
        public int ArticleId { get; set; }
    }
}
