using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Havana500.Domain.Base;
using Havana500.Domain.Enums;
using Havana500.Domain.Models;
using Havana500.Domain.Models.Media;

namespace Havana500.Domain
{
    public class MarketingContent : AuditableAndTrackableEntity<int>, ILanguage
    {
        public MarketingContent()
        {
            this.MarketingContentTags = new List<MarketingContentTag>();
            LanguageCulture = "es";
        }

        /// <summary>
        /// Gets or sets the value that represent the Name of the MarketingContent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the company owner of the marketing content
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Indicates the content level where the marketing correspond
        /// </summary>
        public ContentLevel ContentLevel { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates if the MarketingContent is active or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or Sets the value that indicates the weight for the current MarketingTag. This is used for the algorithm of marketing content visualization.
        /// </summary>
        public float Weight { get; set; }

        public IEnumerable<MarketingContentTag> MarketingContentTags { get; set; }

        /// <summary>
        /// Get or Sets the Tags related to the current MarketingContent
        /// </summary>
        [NotMapped]
        public IEnumerable<ContentTag> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the picture of the MarketingContent.
        /// </summary>
        public Picture Picture { get; set; }

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
