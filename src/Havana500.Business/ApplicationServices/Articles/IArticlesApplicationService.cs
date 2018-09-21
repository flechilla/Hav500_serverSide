using Havana500.Business.Base;
using Havana500.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.Business.ApplicationServices.Articles
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Havana500.Domain.Article" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Havana500.Domain.Article" /> entity
    ///         through the <see cref="IArticlesRepository" /> but when necessary
    ///         adds some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface IArticlesApplicationService : IBaseApplicationService<Article, int>
    {
        /// <summary>
        ///     Increment the amount of views in the Article
        ///     with Id equal to <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of views for the given article.</returns>
        int AddView(int articleId);

        /// <summary>
        ///     Increment the amount of views in the Article
        ///     with Id equal to <paramref name="articleId"/> asynchronously.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of views for the given article.</returns>
        Task<int> AddViewAsync(int articleId);

        /// <summary>
        ///     Gets the comments related to the <see cref="Article"/> with the 
        ///     given <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article that is parent of the comments.</param>
        /// <param name="currentPage">The currentPage of comments. This can be seen as the amount of pulls from the client.</param>
        /// <param name="amountOfComments">The amount of commets to return.</param>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetComments(int articleId, int currentPage, int amountOfComments);

        /// <summary>
        ///     Gets the Article with the given <param name="articleId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="articleId">The Id of the Article</param>
        /// <returns>The Article with its related Tags</returns>
        Article GetArticleWithTags(int articleId);

        /// <summary>
        /// Adds a relation between an<see cref="Article"/> and a<see cref="ContentTag"/>
        /// </summary>
        /// <param name="articleContentTag"></param>
        /// <returns></returns>
        Task<ArticleContentTag> AddArticleContentTagAsync(ArticleContentTag articleContentTag);

        /// <summary>
        /// Removes the relation between an <see cref="Article"/> and a <see cref="ContentTag"/>
        /// </summary>
        /// <param name="articleContentTag"></param>
        /// <returns></returns>
        Task RemoveArticleContentTagAsync(ArticleContentTag articleContentTag);
    }
}
