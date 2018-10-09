using Havana500.Domain.Base;

namespace Havana500.Domain.Models.Media
{
    public class Picture : AuditableAndTrackableEntity<int>, IHasMedia
    {
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
        ///     Gets or sets the relative path of the file.
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
    }
}
