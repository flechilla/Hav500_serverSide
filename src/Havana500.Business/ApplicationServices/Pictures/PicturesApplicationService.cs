using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havana500.Domain.Models.Media;
using Havana500.Business.Base;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Pictures;
using Havana500.Domain;

namespace Havana500.Business.ApplicationServices.Pictures
{
    public class PicturesApplicationService : BaseApplicationService<Picture, int>, IPicturesApplicationService
    {
        public PicturesApplicationService(IPicturesRepository repository) : base(repository)
        {
        }

        protected new IPicturesRepository Repository { get { return base.Repository as IPicturesRepository; } }

        public IQueryable<Picture> GetAllByType(PictureType pictType)
        {
            return Repository.GetAllByType(pictType);
        }

        public Task<IQueryable<Picture>> GetAllByTypeAsync(PictureType pictType)
        {
            return Repository.GetAllByTypeAsync(pictType);
        }

        public IQueryable<Picture> GetAllByTypeWithData(PictureType pictType)
        {
            return Repository.GetAllByTypeWithData(pictType);
        }

        public Task<IQueryable<Picture>> GetAllByTypeWithDataAsync(PictureType pictType)
        {
            return Repository.GetAllByTypeWithDataAsync(pictType);
        }

        public IQueryable<Picture> GetByType(PictureType pictType, int amount)
        {
            return Repository.GetByType(pictType, amount);
        }

        public Task<IQueryable<Picture>> GetByTypeASync(PictureType pictType, int amount)
        {
            return Repository.GetByTypeASync(pictType, amount);
        }

        /// <summary>
        ///     Gets the Picture with the given <param name="pictureId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="pictureId">The Id of the Picture</param>
        /// <returns>The Picture with its related Tags</returns>
        public async Task<Picture> GetPictureWithTags(int pictureId)
        {
            return await Repository.GetPictureWithTagsAsync(pictureId);
        }

        public async Task<PictureContentTag> AddPictureContentTagAsync(PictureContentTag pictureContentTag)
        {
            if (await Repository.DbContext.Set<PictureContentTag>().FindAsync(pictureContentTag.PictureId, pictureContentTag.ContentTagId) != null)
                return pictureContentTag;
            return await Repository.AddPictureContentTagAsync(pictureContentTag);
        }

        public async Task RemovePictureContentTagAsync(PictureContentTag pictureContentTag)
        {
            var pictureTagToDelete = await Repository.DbContext.Set<PictureContentTag>()
                .FindAsync(pictureContentTag.PictureId, pictureContentTag.ContentTagId);

            if (pictureTagToDelete != null)
                await Repository.RemoveArticleContentTagAsync(pictureTagToDelete);

        }
    }
}
