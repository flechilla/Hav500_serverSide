using Havana500.Domain;
using System.Linq;
using Havana500.Domain.Enums;
using System.Threading.Tasks;

namespace Havana500.DataAccess.Repositories
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="Havana500.Domain.Comment"/> entity.
    /// </summary>
    public interface ICommentsRepository : IBaseRepository<Havana500.Domain.Comment, int>
    {
        #region Sync Members
        /// <summary>
        ///     Filter the comments in the table based on
        ///     the given parentID and Discriminator
        /// </summary>
        IQueryable<Comment> ReadAll(int articleId);

        IQueryable<Comment> ReadAll(int articleId, int count);
        #endregion

        #region Async Members

        /// <summary>
        ///     Asynchronously filter the elements in the table based on
        ///     the given predicate
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>The elements that satisfy the predicate </returns>
        Task<IQueryable<Comment>> ReadAllAsync(int articleId);

        Task<IQueryable<Comment>> ReadAllAsync(int articleId, int count);
        #endregion
    }
}
