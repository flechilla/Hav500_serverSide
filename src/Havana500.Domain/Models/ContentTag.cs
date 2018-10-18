using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain.Base;
using Havana500.Domain.Models;

namespace Havana500.Domain
{
    /// <summary>
    ///     Represent a tag that can be assigned to any
    ///     content (i.e Section, Article...)
    /// </summary>
    public class ContentTag : AuditableAndTrackableEntity<int>, ILanguage
    {
        public ContentTag()
        {
            LanguageCulture = "es";
        }
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
        public List<ArticleContentTag> ArticleContentTags { get; set; }

        /// <summary>
        /// Get or Sets the MarketingContent that contains this tag.
        /// </summary>
        public List<MarketingContentTag> MarketingContentTags { get; set; }

        public List<PictureContentTag> PicturesContentTags { get; set; }

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
