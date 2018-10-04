using Havana500.Business.Base;
using Havana500.Domain;
using Havana500.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havana500.DataAccess.Repositories;

namespace Havana500.Business.ApplicationServices.Comments
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Havana500.Domain.Comment" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Havana500.Domain.Comment" /> entity
    ///         through the <see cref="ICommentsRepository" /> but when necessary
    ///         adda some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface ICommentsApplicationService : IBaseApplicationService<Havana500.Domain.Comment,int>
    {
        #region Sync Members
        /// <summary>
        ///     Filter the comments in the table based on
        ///     the given parentID and Discriminator
        /// </summary>
        IQueryable<Comment> ReadAll(int articleId);
        IQueryable<Comment> ReadAll(int articleId, int Count);
        void AddComment(Comment comment);
        #endregion

        #region Async Members

        /// <summary>
        ///     Asynchronously filter the elements in the table based on
        ///     the given predicate
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="discriminator"></param>
        /// <param name="filter">A function to be applied in each element of the table</param>
        /// <returns>The elements that satisfy the predicate <paramref name="filter"/></returns>
        Task<IQueryable<Comment>> ReadAllAsync(int articleId);
        Task<IQueryable<Comment>> ReadAllAsync(int articleId, int count);

        #endregion
    }
}
