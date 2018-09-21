using System;
using System.Collections.Generic;
using Havana500.Models.TagViewModels;

namespace Havana500.Models.ArticleViewModels
{
    public class ArticleBaseViewModel : BaseViewModel<int>
    {
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
        ///     Gets or sets the Article tags
        /// </summary>
        public ICollection<TagBaseViewModel> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the Article start date and time
        /// <remarks>
        ///     With this value we can automate the publication of the Article to some date.
        /// </remarks>    
        /// </summary>
        /// 
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
		///     Gets or sets the Article end date and time
		/// </summary>
        public DateTime? EndDateUtc { get; set; }

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
        ///     Gets or sets the value that indicate the initial
        ///     weight for the current entity. This value is assigned
        ///     by the editor of the article depending on its importance.
        /// </summary>
        public int EditorWeight { get; set; }//TODO: create and enum for this

        /// <summary>
        ///     Gets or sets the value that indicates the amount of minutes that takes
        ///     to read the article.
        /// </summary>
        public float ReadingTime { get; set; }

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
    }
}
