using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Havana500.Domain;
using Havana500.DataAccess.Contexts;

namespace Havana500.DataAccess.Repositories.Base
{
    /// <summary>
    /// Implementation of the Unit of Work Pattern for SQL using EF
    /// </summary>
    public class SqlUnitOfWork : Havana500DbContext, IUnitOfWork
    {
        /// <summary>
        /// Constructor for testing purposes
        /// </summary>
        /// <param name="options"></param>
        public SqlUnitOfWork(DbContextOptions<Havana500DbContext> options)
            : base(options)
        {
        }

        protected string ConnectionStringName { get; set; }

        protected IDbConnection OpenConnection(out bool closeManually)
        {
            var conn = base.Database.GetDbConnection();
            closeManually = false;
            // Not sure here, should asume always opened??
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                closeManually = true;
            }

            return conn;
        }

        public ICollection<TResult> RawQuery<TResult>(string query, object queryParams = null)
        {
            var connection = OpenConnection(out bool closeConnection);
            var queryResult = connection.Query<TResult>(query, queryParams).ToList();
            if (closeConnection)
            {
                connection.Close();
            }

            return queryResult;
        }

        //IBaseRepository<TEntity, TKey> Set<TEntity, TKey>()
        //{
        //    return new BaseRepository<TEntity, TKey>(this);
        //}



        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public async Task<ICollection<TResult>> RawQueryAsync<TResult>(string query, object queryParams = null)
        {
            var connection = OpenConnection(out bool closeConnection);
            var queryResult = await connection.QueryAsync<TResult>(query, queryParams);
            if (closeConnection)
            {
                connection.Close();
            }
            return queryResult.ToList();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionStringName);
            }
        }
    }
}
