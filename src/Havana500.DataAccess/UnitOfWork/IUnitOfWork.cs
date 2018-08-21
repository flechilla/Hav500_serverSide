using Havana500.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Havana500.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes to the underlying store
        /// </summary>
        /// <returns>The number of afected objects</returns>
        int SaveChanges();

        /// <summary>
        /// Saves changes to the underlying store asynchronously
        /// </summary>
        /// <returns>The number of afected objects</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Executes a raw query to the underlying store
        /// </summary>
        /// <typeparam name="TResult">The type of the entities returned by the query</typeparam>
        /// <param name="query">The query to execute</param>
        /// <param name="queryParams">The query parameters</param>
        /// <returns>A collection of elements returned by the query</returns>
        ICollection<TResult> RawQuery<TResult>(string query, object queryParams = null);

        /// <summary>
        /// Executes a raw query to the underlying store asynchronously
        /// </summary>
        /// <typeparam name="TResult">The type of the entities returned by the query</typeparam>
        /// <param name="query">The query to execute</param>
        /// <param name="queryParams">The query parameters</param>
        /// <returns>A collection of elements returned by the query</returns>
        Task<ICollection<TResult>> RawQueryAsync<TResult>(string query, object queryParams = null);
    }
}
