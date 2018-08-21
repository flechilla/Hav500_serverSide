using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using System;
using Havana500.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace Havana500.DataAccess.Repositories
{
    public class CommentsRepository : BaseRepository<Comment, int>, ICommentsRepository
    {
        public CommentsRepository(Havana500DbContext dbContext) : base(dbContext)
        {

        }
        /// <summary>
        /// method for get all comments related with discriminator
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<Comment> ReadAll((int idparent, Discriminator discriminator) filter)
        {

            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query<Comment>("SELECT * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new { discriminator = filter.discriminator, ParentId = filter.idparent });
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
        /// <param name="filter">Filters for Search in Comments</param>
        /// <param name="count">Number of Comment to take</param>
        /// <returns></returns>
        public IQueryable<Comment> ReadAll((int idparent, Discriminator discriminator) filter, int count)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query<Comment>("SELECT TOP(@count) * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new {count = count, discriminator = filter.discriminator, ParentId = filter.idparent });
                    IDbConnection.Close();
                    return result.AsQueryable();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IQueryable<Comment>> ReadAllAsync((int idparent, Discriminator discriminator) filter)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query<Comment>("SELECT * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new { discriminator = filter.discriminator, ParentId = filter.idparent });
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

        public async Task<IQueryable<Comment>> ReadAllAsync((int idparent, Discriminator discriminator) filter, int count)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query<Comment>("SELECT TOP(@count) * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new {count = count, discriminator = filter.discriminator, ParentId = filter.idparent });
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
