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
        public async Task<ICollection<Comment>> GetComments(int articleId, int currentPage, int amountOfComments)
        {
            return await this.Entities.
                Where(a => a.Id == articleId).
              Include(a => a.Comments).
              Select(a => a.Comments).
              Skip(currentPage*amountOfComments).
              Take(amountOfComments).
              FirstAsync();
        }
    }
}
