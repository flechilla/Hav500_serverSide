﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Havana500.Domain.Base;

namespace Havana500.Domain.Models.Media
{
    public class Picture : AuditableAndTrackableEntity<int>, IHasMedia, ILanguage
    {
        public Picture()
        {
            LanguageCulture = "es";
        }

        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the picture width
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the picture height
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the SEO friendly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        ///     Gets or sets the value that represent the type of the picture.
        /// </summary>
        public PictureType PictureType { get; set; }

        /// <summary>
        /// Gets or sets the media storage PK
        /// </summary>
        public int? MediaStorageId { get; set; }

        /// <summary>
        ///     Gets or sets the media storage. Is optional to storage the
        ///     picture in the DB, only for small ones.
        /// </summary>
        public MediaStorage MediaStorage { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicate the full path
        ///     to the Picture.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        ///     Gets or sets the relative path of the file or the link 
        ///     to YouTUbe
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the extension
        ///     of the picture.
        /// </summary>
        public string PictureExtension { get; set; }

        /// <summary>
        ///     Gets or sets the FK to the article.
        /// </summary>
        public int? ArticleId { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates that this
        ///     picture is active, which means that can be displayed
        ///     for the users.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the property that contains the reference
        ///     to the url where we should take the user after click
        ///     a picture.
        /// </summary>
        public string HRef { get; set; }

        /// <summary>
        /// Gets or Sets the value that indicates the weight for the current MarketingTag. This is used for the algorithm of marketing content visualization.
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets the value that represent the Name of the MarketingContent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the company owner of the marketing content
        /// </summary>
        public string CompanyName { get; set; }

        public string LanguageCulture { get; set; }

        public IEnumerable<PictureContentTag> PicturesContentTags { get; set; }

        /// <summary>
        ///     Gets the Tags related to the current Article.
        /// </summary>
        [NotMapped]
        public IEnumerable<ContentTag> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the value that with the ID of the
        ///     user that this picture is related to.
        /// </summary>
        public string UserId { get; set; }
    }
}
