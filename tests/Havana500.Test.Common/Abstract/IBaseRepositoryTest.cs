using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;
using Havana500.DataAccess.Repositories;
using Havana500.Domain.Base;

namespace Havana500.Test.Common.Abstract
{
    /// <summary>
    ///     Declares the common member for all the unit Tests 
    ///     for the DataAccess layer.
    /// </summary>
    /// <typeparam name="TRepository">The type of the <see cref="IBaseRepository{TEntity,TKey}"/>
    /// to the tested.</typeparam>
    /// <typeparam name="TEntity">The type of entity for manipulate</typeparam>
    /// <typeparam name="TKey">The type of the key for that entity</typeparam>
    public interface IBaseRepositoryTest<out TRepository, TEntity, TKey>
        where TRepository : IBaseRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        /// <summary>
        ///     Create an instance of the actual implementation of the interface
        ///      <value>TApplicationService</value> for testing its operations
        /// </summary>
        /// <param name="context">The DBcontext for be used in the unit tests</param>
        /// <returns>An instance of the actual implementation of the interface</returns>
        TRepository GetInstance(Havana500DbContext context);
    }
}
