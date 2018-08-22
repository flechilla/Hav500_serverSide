using Havana500.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;
using System.Threading.Tasks;
using Dapper;

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
    }
}
