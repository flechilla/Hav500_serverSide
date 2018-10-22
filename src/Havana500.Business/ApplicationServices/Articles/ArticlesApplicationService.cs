using Havana500.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain;
using System.Linq;
using System.Threading.Tasks;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.Business.ApplicationServices.Articles
{
    public class ArticlesApplicationService : BaseApplicationService<Article, int>, IArticlesApplicationService
    {
        public ArticlesApplicationService(IArticlesRepository repository) : base(repository)
        {
        }

        public new IArticlesRepository Repository
        {
            get { return base.Repository as IArticlesRepository; }
        }

        public int AddView(int articleId)
        {
            return Repository.AddView(articleId);
        }

        public Task<int> AddViewAsync(int articleId)
        {
            return Repository.AddViewAsync(articleId);
        }

        /// <summary>
        ///     Gets the comments related to the <see cref="Article"/> with the
        ///     given <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article that is parent of the comments.</param>
        /// <param name="currentPage">The currentPage of comments. This can be seen as the amount of pulls from the client.</param>
        /// <param name="amountOfComments">The amount of comments to return.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Comment>> GetComments(int articleId, int currentPage, int amountOfComments)
        {
            return await Repository.GetComments(articleId, currentPage, amountOfComments);
        }

        /// <summary>
        ///     Gets the Article with the given <param name="articleId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="articleId">The Id of the Article</param>
        /// <returns>The Article with its related Tags</returns>
        public async Task<Article> GetArticleWithTagsAsync(int articleId)
        {
            return await Repository.GetArticleWithTagsAsync(articleId);
        }

        /// <summary>
        ///     Gets a list with all the articles and the amount of new comments
        ///     since daysAgo
        /// </summary>
        /// <param name="daysAgo">The amount of days ago to calculate the amount of comments</param>
        /// <param name="length"></param>
        /// <returns>A list with the Articles that have at leat 1 new comment</returns>
        public IEnumerable<Article> GetArticlesWithNewCommentsInfo(int daysAgo, int pageNumber, int pageSize,
            string columnNameForSorting, string sortingType, out long length, string columnsToReturn = "*")
        {
            return Repository.GetArticlesWithNewCommentsInfo(daysAgo, pageNumber, pageSize, columnNameForSorting,
                sortingType, out length, columnsToReturn = "*");
        }

        public async Task<ArticleContentTag> AddArticleContentTagAsync(ArticleContentTag articleContentTag)
        {
            if (await Repository.DbContext.Set<ArticleContentTag>()
                    .FindAsync(articleContentTag.ArticleId, articleContentTag.ContentTagId) != null)
                return articleContentTag;
            return await Repository.AddArticleContentTagAsync(articleContentTag);
        }

        public async Task RemoveArticleContentTagAsync(ArticleContentTag articleContentTag)
        {
            var articleTagToDelete = await Repository.DbContext.Set<ArticleContentTag>()
                .FindAsync(articleContentTag.ArticleId, articleContentTag.ContentTagId);

            if (articleTagToDelete != null)
                await Repository.RemoveArticleContentTagAsync(articleTagToDelete);
        }

        /// <summary>
        ///     Gets the Articles related to the article with the given <param name="articleId"></param>
        /// </summary>
        /// <param name="articleId">The Id of the article that its related have to be returned.</param>
        /// <returns>The related articles with short properties</returns>
        public async Task<IEnumerable<Article>> GetRelatedArticles(int articleId)
        {
            return await Repository.GetRelatedArticles(articleId);
        }

        /// <summary>
        ///     Gets the articles that belongs to the given <param name="sectionName"></param>,
        ///     sending just the given <param name="amountOfArticles"></param> that belongs to the
        ///     given <param name="currentPage"></param>
        /// </summary>
        /// <param name="sectionName">The name of the section that the articles belongs.</param>
        /// <param name="currentPage">The current page</param>
        /// <param name="amountOfArticles">The amount of articles per page.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetArticlesBasicDataBySectionName(string sectionName, int currentPage,
            int amountOfArticles)
        {
            return await this.Repository.GetArticlesBasicDataBySectionName(sectionName, currentPage, amountOfArticles);
        }

        /// <summary>
        ///     Gets the articles that belongs to the given <param name="sectionName"></param>,
        ///     and are related to the at least one of the given <param name="tagsIds"></param>
        ///     sending just the given <param name="amountOfArticles"></param> that belongs to the
        ///     given <param name="currentPage"></param>
        /// </summary>
        /// <param name="sectionName">The name of the section that the articles belongs.</param>
        /// <param name="tagsIds">The Ids of the related ContentTags </param>
        /// <param name="currentPage">The current page</param>
        /// <param name="amountOfArticles">The amount of articles per page.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetArticlesBasicDataBySectionNameAndTagIds(string sectionName,
            int[] tagsIds,
            int currentPage, int amountOfArticles)
        {
            return await Repository.GetArticlesBasicDataBySectionNameAndTagIds(sectionName, tagsIds, currentPage,
                amountOfArticles);
        }
    }
}