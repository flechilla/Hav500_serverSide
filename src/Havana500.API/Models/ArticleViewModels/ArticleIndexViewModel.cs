using System.Collections.Generic;
using Havana500.API.Models.PictureViewModels;
using Havana500.Models.PictureViewModels;
using Havana500.Models.TagViewModels;

namespace Havana500.Models.ArticleViewModels
{
    public class ArticleIndexViewModel : ArticleBaseViewModel
    {
        public ArticleIndexViewModel()
        {

        }
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
        ///     Gets or sets the value that indicates the amount
        ///     of views of this Article.
        /// </summary>
        public int Views { get; set; }

        public string PublicationDateHumanized { get; set; }

        public new IEnumerable<TagIndexViewModel> Tags { get; set; }

        public List<IndexPictureViewModel> Pictures { get; set; }
    }
}
