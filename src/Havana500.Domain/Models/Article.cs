using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain.Base;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Havana500.Domain.Models;

namespace Havana500.Domain
{
    /// <summary>
    ///     Represents an article of the app.
    ///     This can be used for the blog, for the section
    ///     and so.
    /// </summary>
    public class Article : AuditableAndTrackableEntity<int>, ILanguage
    {
        public Article()
        {
            StartDateUtc = DateTime.Now;
            EndDateUtc = DateTime.MaxValue;
            LanguageCulture = "es";
        }

        /// <summary>
        ///     Gets or sets the value that represent the
        ///     Title of the Article.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the value that represents
        ///     the Body of the Article.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets the FK to the parent section
        ///     of this article.
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        ///     Gets or sets the reference to the parent section.
        /// </summary>
        public Section Section { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the Article comments are allowed 
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the Article comments are allowed 
        ///     for anonymous users. 
        ///     
        /// </summary>
        public bool AllowAnonymousComments { get; set; }

        /// <summary>
		///     Gets or sets the total number of approved comments
		/// <remarks>
        /// The same as if we run newsItem.NewsComments.Where(n => n.IsApproved).Count()
		/// We use this property for performance optimization (no SQL command executed)
		/// </remarks>
		/// </summary>
        public int ApprovedCommentCount { get; set; }


        /// <summary>
        /// Gets or sets the total number of not approved comments
        /// <remarks>The same as if we run newsItem.NewsComments.Where(n => !n.IsApproved).Count()
        /// We use this property for performance optimization (no SQL command executed)</remarks>
        /// </summary>
        public int NotApprovedCommentCount { get; set; }

        /// <summary>
        ///     Gets or sets the Article tags
        /// </summary>
        public List<ArticleContentTag> ArticleContentTags { get; set; }

        /// <summary>
        ///     Gets or sets the Article start date and time
        /// <remarks>
        ///     With this value we can automate the publication of the Article to some date.
        /// </remarks>    
        /// </summary>
        /// 
        public DateTime StartDateUtc { get; set; }

        /// <summary>
		///     Gets or sets the Article end date and time
		/// </summary>
        public DateTime EndDateUtc { get; set; }

        /// <summary>
		/// Gets or sets the meta keywords
		/// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }


        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        ///     Gets or sets the Comments that are related
        ///     to this Article.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the amount
        ///     of views of this Article.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the amount
        ///     of comments of the current Article.
        /// </summary>
        /// <remarks>
        ///     Each time that a comment is added to the article, 
        ///     this field as to be updated with the '++' operator.
        ///     This value is the same that 'this.Comments.Count()'
        ///     but exist for optimization purposes.
        /// </remarks>
        public int AmountOfComments { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the weight for the current
        ///     Article.!-- This is used for the algorithm to sort the articles.
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicate the initial
        ///     weight for the current entity. This value is assigned
        ///     by the editor of the article depending on its importance.
        /// </summary>
        public int EditorWeight { get; set; }//TODO: create and enum for this

        /// <summary>
        ///     Gets or sets the value that indicates the amount of minutes that takes
        ///     to read the article.
        /// </summary>
        public int ReadingTime { get; set; }

        /// <summary>
        ///     Gets the Tags related to the current Article.
        /// </summary>
        [NotMapped]
        public IEnumerable<ContentTag> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the Culture for the current
        ///     entity
        /// </summary>
        /// <remarks>
        ///     The value of this property is in neutral
        ///     culture. Ex: 'es', 'en' or 'fr'
        /// </remarks>
        public string LanguageCulture { get; set; }
    }
}
