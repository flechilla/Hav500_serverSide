﻿using System;
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
using System.Reflection;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.DataAccess.UnitOfWork
{
    /// <summary>
    /// Implementation of the Unit of Work Pattern for SQL using EF
    /// </summary>
    public class SqlUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Constructor for testing purposes
        /// </summary>
        /// <param name="options"></param>
        public SqlUnitOfWork(Havana500DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Havana500DbContext DbContext { get; set; }

        protected string ConnectionStringName { get; set; }

        public IDbConnection OpenConnection(out bool closeManually)
        {
            var conn = DbContext.Database.GetDbConnection();
            closeManually = false;
            // Not sure here, should assume always opened??
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
            return DbContext.SaveChangesAsync();
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

        public TResult QueryFirst<TResult>(string query, object queryParams = null)
        {
            var connection = OpenConnection(out bool closeConnection);
            var queryResult = connection.QueryFirst<TResult>(query, queryParams);
            if (closeConnection)
            {
                connection.Close();
            }

            return queryResult;
        }

        public async Task<TResult> QueryFirstAsync<TResult>(string query, object queryParams = null)
        {
            var connection = OpenConnection(out bool closeConnection);
            var queryResult = await connection.QueryFirstAsync<TResult>(query, queryParams);
            if (closeConnection)
            {
                connection.Close();
            }

            return queryResult;
        }

        public IBaseRepository GetRepositoryForType(Type entityType)
        {
            Assembly repositoriesAssembly = Assembly.GetAssembly(typeof(IBaseRepository));

            var repositoryClassType = repositoriesAssembly.
                GetTypes().
                FirstOrDefault(t => t.Name.Contains(entityType.Name) && !t.Name.EndsWith("Seeds") && t.IsClass);

            if (repositoryClassType == null)
                throw new Exception("It ain't an repository that implements the entity with name: " + entityType.Name);

            if(repositoryClassType.Name == "CommentsRepository")
                return (IBaseRepository)Activator.CreateInstance(repositoryClassType, DbContext, new ArticlesRepository(DbContext));

            return (IBaseRepository)Activator.CreateInstance(repositoryClassType, DbContext);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
