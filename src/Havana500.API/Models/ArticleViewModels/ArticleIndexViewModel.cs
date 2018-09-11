using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public string PublicationDateHumanized { get; set; }
    }
}
