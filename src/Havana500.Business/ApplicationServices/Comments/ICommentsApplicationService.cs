using Havana500.Business.Base;
using Havana500.Domain;
using Havana500.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IQueryable<Comment> ReadAll(int idparent, Discriminator discriminator);
        IQueryable<Comment> ReadAll(int idparent, int Count, Discriminator discriminator);
        void AddComment(Comment comments, Discriminator discriminator);
        #endregion

        #region Async Members
        /// <summary>
        ///     Asynchronously filter the elements in the table based on
        ///     the given predicate
        /// </summary>
        /// <param name="filter">A function to be applied in each element of the table</param>
        /// <returns>The elements that satisfy the predicate <paramref name="filter"/></returns>
        Task<IQueryable<Comment>> ReadAllAsync(int idparent, Discriminator discriminator);
        Task<IQueryable<Comment>> ReadAllAsync(int idparent, int Count, Discriminator discriminator);

        #endregion
    }
}
