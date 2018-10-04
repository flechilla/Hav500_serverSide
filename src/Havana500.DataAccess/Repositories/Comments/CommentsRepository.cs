using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using System;
using Havana500.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.DataAccess.Repositories
{
    public class CommentsRepository : BaseRepository<Comment, int>, ICommentsRepository
    {
        private readonly IArticlesRepository _articlesRepository;

        public CommentsRepository(Havana500DbContext dbContext, IArticlesRepository articlesRepository) : base(dbContext)
        {
            _articlesRepository = articlesRepository;
        }

        /// <summary>
        /// method for get all comments related with discriminator
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public IQueryable<Comment> ReadAll(int articleId)
        {

            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = OpenConnection(out bool closeConn))
                {
                    var result = IDbConnection.Query<Comment>("SELECT * FROM Comments WHERE ArticleId = @articleId", new { articleId });
                    IDbConnection.Close();
                    return result.AsQueryable();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// get comments for given discriminator and parentId, and take Count
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="count">Number of Comment to take</param>
        /// <returns></returns>
        public IQueryable<Comment> ReadAll(int articleId, int count)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = OpenConnection(out bool closeConn))
                {
                    var result = IDbConnection.Query<Comment>("SELECT TOP(@count) * FROM Comments WHERE ArticleId = @articleId", new {count, articleId });
                    IDbConnection.Close();
                    return result.AsQueryable();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int articleId)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = OpenConnection(out bool closeConn))
                {
                    var result = IDbConnection.Query<Comment>("SELECT * FROM Comments WHERE ArticleId = @articleId", new {articleId});
                    IDbConnection.Close();
                    return await Task.Factory.StartNew(() =>
                    {
                        return result.AsQueryable();
                    });
                }
            }
            catch (Exception e)
            {
                return null;
            }           
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int articleId, int count)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = OpenConnection(out bool closeConn))
                {
                    var result = IDbConnection.Query<Comment>("SELECT TOP(@count) * FROM Comments WHERE ArticleId = @articleId", new {count = count, articleId });
                    IDbConnection.Close();
                    return await Task.Factory.StartNew(() =>
                    {
                        return result.AsQueryable();
                    });
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
