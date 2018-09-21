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
                        OFFSET {currentPage*amountOfComments} ROWS
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
    }
}
