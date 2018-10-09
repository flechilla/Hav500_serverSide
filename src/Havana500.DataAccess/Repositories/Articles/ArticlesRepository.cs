using Havana500.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Havana500.DataAccess.Repositories.Articles
{
    public class ArticlesRepository : BaseRepository<Article, int>, IArticlesRepository
    {
        public ArticlesRepository(Havana500DbContext dbContext) : base(dbContext)
        {
        }

        public int AddView(int articleId)
        {
            int result;

            var connection = OpenConnection(out bool closeConn);
            try
            {
                var query = @"
                                UPDATE Articles
                                SET Views = Views + 1
                                WHERE Id = @articleId;

                                SELECT Views
                                FROM Articles
                                WHERE Id = @articleId;";
                result = connection.ExecuteScalar<int>(query, new { articleId });
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public async Task<int> AddViewAsync(int articleId)
        {
            int result;
            using (var connection = OpenConnection(out bool closeConn))
            {
                try
                {
                    var query = @"
                                UPDATE Articles
                                SET Views = Views + 1
                                WHERE Id = @articleId

                                SELECT Views
                                FROM Articles
                                WHERE Id = @articleId";
                    result = await connection.ExecuteScalarAsync<int>(query, new { articleId });
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        /// <summary>
        ///     Increment the amount of comments in the Article
        ///     with Id equal to <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of comments for the given article.</returns>
        public int AddComment(int articleId)
        {
            int result;

            var connection = OpenConnection(out bool closeConn);
            try
            {
                var query = @"
                                UPDATE Articles
                                SET AmountOfComments = AmountOfComments + 1
                                WHERE Id = @articleId;

                                SELECT AmountOfComments
                                FROM Articles
                                WHERE Id = @articleId;";
                result = connection.ExecuteScalar<int>(query, new { articleId });
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        /// <summary>
        ///     Increment the amount of comments in the Article
        ///     with Id equal to <paramref name="articleId"/> asynchronously.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of comments for the given article.</returns>
        public async Task<int> AddCommentAsync(int articleId)
        {
            int result;

            var connection = OpenConnection(out bool closeConn);
            try
            {
                var query = @"
                                UPDATE Articles
                                SET AmountOfComments = AmountOfComments + 1
                                WHERE Id = @articleId;

                                SELECT AmountOfComments
                                FROM Articles
                                WHERE Id = @articleId;";
                result = await connection.ExecuteScalarAsync<int>(query, new { articleId });
            }
            finally
            {
                connection.Close();
            }
            return result;
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
            var query = $@"SELECT C.*
                        FROM Articles AS A
                        INNER JOIN Comments AS C
                        ON A.Id = C.ArticleId
                        WHERE A.Id = {articleId}
                        ORDER BY C.CreatedAt DESC
                        OFFSET {currentPage * amountOfComments} ROWS
                        FETCH NEXT {amountOfComments} ROWS ONLY";

            var connection = OpenConnection(out bool closeConnection);
            IEnumerable<Comment> result;

            try
            {
                result = await connection.QueryAsync<Comment>(query);
            }

            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        ///     Gets the Article with the given <param name="articleId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="articleId">The Id of the Article</param>
        /// <returns>The Article with its related Tags</returns>
        public Article GetArticleWithTags(int articleId)//TODO: Improve this implementation without using multiple query and mapping the result
        {
            //var query = $@"SELECT  A.Id, A.Title, A.Body, A.ReadingTime, A.StartDateUtc, A.AllowComments, A.AllowAnonymousComments, A.MetaDescription, A.MetaKeywords, A.MetaTitle, A.Views,  CT.Name
            //            FROM Articles AS A
            //            INNER JOIN ArticleContentTag AS ACT
            //            ON A.Id = ACT.ArticleId
            //            INNER JOIN ContentTags AS CT
            //            ON ACT.ContentTagId = CT.ID
            //            WHERE A.Id = {articleId}
            //            GROUP BY A.Id, A.Title, A.Body, A.ReadingTime, A.StartDateUtc, A.AllowComments, A.AllowAnonymousComments, A.MetaDescription, A.MetaKeywords, A.MetaTitle, A.Views,  CT.Name";

            var query = $@"SELECT * 
                        FROM Articles
                        WHERE Id = {articleId}

                        SELECT CT.Id, CT.Name
                        FROM ArticleContentTag AS ACT
                        INNER JOIN ContentTags AS CT
                        ON ACT.ContentTagId = CT.ID
                        WHERE ACT.ArticleId = {articleId}";

            var connection = OpenConnection(out bool closeManually);

            Article result;

            try
            {
                using (var queryResult = connection.QueryMultiple(query))
                {
                    result = queryResult.ReadFirst<Article>();

                    if (result != null)
                        result.Tags = queryResult.Read<ContentTag>();
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public async Task<ArticleContentTag> AddArticleContentTagAsync(ArticleContentTag articleContentTag)
        {
            if (articleContentTag == null)
            {
                throw new ArgumentNullException("The given entity must not be null");
            }

            await this.DbContext.Set<ArticleContentTag>().AddAsync(articleContentTag);

            return articleContentTag;
        }

        public async Task RemoveArticleContentTagAsync(ArticleContentTag articleContentTag)
        {
            if (articleContentTag == null)
            {
                throw new ArgumentNullException("The given entity must not be null");
            }

            await Task.Factory.StartNew(() =>
            {
                this.DbContext.Set<ArticleContentTag>().Remove(articleContentTag);
            });

        }

        /// <summary>
        ///     Gets a list with all the articles and the amount of new comments
        ///     since daysAgo
        /// </summary>
        /// <param name="daysAgo">The amount of days ago to calculate the amount of comments</param>
        /// <returns>A list with the Articles that have at leat 1 new comment</returns>
        public IEnumerable<Article> GetArticlesWithNewCommentsInfo(int daysAgo, int pageNumber, int pageSize, string columnNameForSorting, string sortingType, string columnsToReturn = "*"){
               var query = $@"SELECT A.Id, A.Title, A.StartDateUtc, 
                            COUNT(C.Id) AS AmountOfComments, COUNT(C.IsApproved) AS ApprovedCommentsCount
                            FROM Articles AS A
                            INNER JOIN Comments AS C
                            ON A.Id = C.ArticleId
                            WHERE DATEDIFF(DAY, C.CreatedAt, GETDATE()) <= {daysAgo}
                            GROUP BY A.Id,A.Title, A.StartDateUtc
                            HAVING COUNT(C.Id)>0
                            ORDER BY COUNT(C.Id) DESC
                            OFFSET {pageNumber*pageSize} ROWS
                            FETCH NEXT {pageSize} ROWS ONLY";

            var connection = OpenConnection(out bool closeConnection);
            IEnumerable<Article> result;

            try
            {
                result = connection.Query<Article>(query);
            }

            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        ///     Gets the Articles related to the article with the given <param name="articleId"></param>
        /// </summary>
        /// <param name="articleId">The Id of the article that its related have to be returned.</param>
        /// <returns>The related articles with short properties</returns>
        public async Task<IEnumerable<Article>> GetRelatedArticles(int articleId)
        {
            var result = DbContext.Set<Article>()
                .Where(_ => true)
                .Take(4)
                .Select(a => 
                new Article()
                {
                    Id= a.Id,
                    Title = a.Title,
                    Views = a.Views,
                    ApprovedCommentCount= a.ApprovedCommentCount,
                    ReadingTime = a.ReadingTime,
                    StartDateUtc = a.StartDateUtc,
                    Body = a.Body.Substring(0, 100)+"..."
                });

            return await result.ToListAsync();
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
            var query =
                $@"SELECT A.Title, SUBSTRING(A.Body, 0, 100)+'...' AS Body, A.Views, A.ApprovedCommentCount, A.StartDateUtc, A.Id
FROm Articles A
INNER JOIN Sections As S ON S.Id = A.SectionId
WHERE s.Name = '{sectionName}'
ORDER BY A.Weight DESC
OFFSET {currentPage*amountOfArticles} ROWS
FETCH NEXT {amountOfArticles} ROWS ONLY";

            var connection = OpenConnection(out bool closeConnection);
            IEnumerable<Article> result;

            try
            {
                result = await connection.QueryAsync<Article>(query);
            }

            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
