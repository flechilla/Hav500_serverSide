using Havana500.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Havana500.Domain;


namespace Havana500.DataAccess.Repositories.Pictures
{
    public interface IPicturesRepository : IBaseRepository<Picture, int>
    {
        /// <summary>
        ///    Return all the Pictures with the given <paramref name="pictType"/>.
        /// </summary>
        /// <param name="pictType">The type of the picture to recover.</param>
        /// <returns>A set of pictures with the same type.</returns>
        IQueryable<Picture> GetAllByType(PictureType pictType);

        /// <summary>
        ///      Return  the Pictures with the given <paramref name="pictType"/>.
        /// </summary>
        /// <param name="pictType">The type of picture.</param>
        /// <param name="amount">The amount of pictures to return.</param>
        /// <returns>A set with size <paramref name="amount"/> and type <paramref name="pictType"/></returns>
        IQueryable<Picture> GetByType(PictureType pictType, int amount);

        /// <summary>
        ///     Returns all the Pictures with a given <paramref name="pictType"/>
        ///     and its data.
        /// </summary>
        /// <param name="pictType">The type of the picture to recover.</param>
        /// <returns>A set of pictures with the same type and its <see cref="MediaStorage"/>.</returns>
        IQueryable<Picture> GetAllByTypeWithData(PictureType pictType);

        /// <summary>
        ///    Return all the Pictures with the given <paramref name="pictType"/> asynchronously.
        /// </summary>
        /// <param name="pictType">The type of the picture to recover.</param>
        /// <returns>A set of pictures with the same type.</returns>
        Task<IQueryable<Picture>> GetAllByTypeAsync(PictureType pictType);

        /// <summary>
        ///      Return  the Pictures with the given <paramref name="pictType"/> asynchronously.
        /// </summary>
        /// <param name="pictType">The type of picture.</param>
        /// <param name="amount">The amount of pictures to return.</param>
        /// <returns>A set with size <paramref name="amount"/> and type <paramref name="pictType"/></returns>
        Task<IQueryable<Picture>> GetByTypeASync(PictureType pictType, int amount);

        /// <summary>
        ///     Returns all the Pictures with a given <paramref name="pictType"/>
        ///     and its data asynchronously.
        /// </summary>
        /// <param name="pictType">The type of the picture to recover.</param>
        /// <returns>A set of pictures with the same type and its <see cref="MediaStorage"/>.</returns>
        Task<IQueryable<Picture>> GetAllByTypeWithDataAsync(PictureType pictType);

        Task<Picture> GetPictureWithTagsAsync(int pictureId);

        Task<PictureContentTag> AddPictureContentTagAsync(PictureContentTag pictureContentTag);

        Task RemoveArticleContentTagAsync(PictureContentTag pictureContentTag);
    }
}
