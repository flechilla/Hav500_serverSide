using Havana500.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havana500.DataAccess.Repositories.Articles
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="Havana500.Domain.Article"/> entity.
    /// </summary>
    public interface IArticlesRepository : IBaseRepository<Article, int>
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
        ///     Increment the amount of comments in the Article
        ///     with Id equal to <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of comments for the given article.</returns>
        int AddComment(int articleId);

        /// <summary>
        ///     Increment the amount of comments in the Article
        ///     with Id equal to <paramref name="articleId"/> asynchronously.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of comments for the given article.</returns>
        Task<int> AddCommentAsync(int articleId);

        /// <summary>
        ///     Gets the comments related to the <see cref="Article"/> with the 
        ///     given <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article that is parent of the comments.</param>
        /// <param name="currentPage">The currentPage of comments. This can be seen as the amount of pulls from the client.</param>
        /// <param name="amountOfComments">The amount of comments to return.</param>
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
        ///     Gets a list with all the articles and the amount of new comments
        ///     since daysAgo
        /// </summary>
        /// <param name="daysAgo">The amount of days ago to calculate the amount of comments</param>
        /// <returns>A list with the Articles that have at leat 1 new comment</returns>
        IEnumerable<Article> GetArticlesWithNewCommentsInfo(int daysAgo, int pageNumber, int pageSize, string columnNameForSorting, string sortingType, string columnsToReturn = "*");
       
         /// Adds a relation between an <see cref="Article"/> and a <see cref="ContentTag"/>
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

        /// <summary>
        ///     Gets the Articles related to the article with the given <param name="articleId"></param>
        /// </summary>
        /// <param name="articleId">The Id of the article that its related have to be returned.</param>
        /// <returns>The related articles with short properties</returns>
        Task<IEnumerable<Article>> GetRelatedArticles(int articleId);

        /// <summary>
        ///     Gets the articles that belongs to the given <param name="sectionName"></param>, 
        ///     sending just the given <param name="amountOfArticles"></param> that belongs to the 
        ///     given <param name="currentPage"></param>
        /// </summary>
        /// <param name="sectionName">The name of the section that the articles belongs.</param>
        /// <param name="currentPage">The current page</param>
        /// <param name="amountOfArticles">The amount of articles per page.</param>
        /// <returns></returns>
        Task<IEnumerable<Article>> GetArticlesBasicDataBySectionName(string sectionName, int currentPage,
            int amountOfArticles);
    }
}
