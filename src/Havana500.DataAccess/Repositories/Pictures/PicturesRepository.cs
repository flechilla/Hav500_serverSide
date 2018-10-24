using Havana500.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Havana500.DataAccess.Contexts;
using System.Linq;
using Dapper;
using Havana500.Domain;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Havana500.DataAccess.Repositories.Pictures
{
    public class PicturesRepository : BaseRepository<Picture, int>, IPicturesRepository
    {
        public PicturesRepository(Havana500DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Picture> GetAllByType(PictureType pictType)
        {
            return this.Entities.
                Where(p => p.PictureType == pictType);
        }
        public async Task<IQueryable<Picture>> GetAllByTypeAsync(PictureType pictType)
        {

            return await Task.Factory.StartNew(() =>
            {
                return this.Entities.
                    Where(p => p.PictureType == pictType);
            });

        }

        //TODO: INclude the MediaStorage

        public IQueryable<Picture> GetAllByTypeWithData(PictureType pictType)
        {
            return this.Entities.
                Where(p => p.PictureType == pictType).
                Include(p => p.MediaStorage);
        }

        //TODO: INclude the MediaStorage

        public async Task<IQueryable<Picture>> GetAllByTypeWithDataAsync(PictureType pictType)
        {
            return await Task.Factory.StartNew(() =>
            {
                return this.Entities.
                    Where(p => p.PictureType == pictType).
                Include(p => p.MediaStorage);
            });
        }

        public IQueryable<Picture> GetByType(PictureType pictType, int amount)
        {
            return this.Entities.
               Where(p => p.PictureType == pictType).
               Take(amount);
        }

        public async Task<IQueryable<Picture>> GetByTypeASync(PictureType pictType, int amount)
        {
            return await Task.Factory.StartNew(() =>
            {
                return this.Entities.
                    Where(p => p.PictureType == pictType).
                    Take(amount);
            });
        }

        /// <summary>
        ///     Gets the Article with the given <param name="articleId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="articleId">The Id of the Article</param>
        /// <returns>The Article with its related Tags</returns>
        public async Task<Picture> GetPictureWithTagsAsync(int pictureId)//TODO: Improve this implementation without using multiple query and mapping the result
        {
            //var query = $@"SELECT  A.Id, A.Title, A.Body, A.ReadingTime, A.StartDateUtc, A.AllowComments, A.AllowAnonymousComments, A.MetaDescription, A.MetaKeywords, A.MetaTitle, A.Views,  CT.Name
            //            FROM Articles AS A
            //            INNER JOIN ArticleContentTag AS ACT
            //            ON A.Id = ACT.ArticleId
            //            INNER JOIN ContentTags AS CT
            //            ON ACT.ContentTagId = CT.ID
            //            WHERE A.Id = {articleId}
            //            GROUP BY A.Id, A.Title, A.Body, A.ReadingTime, A.StartDateUtc, A.AllowComments, A.AllowAnonymousComments, A.MetaDescription, A.MetaKeywords, A.MetaTitle, A.Views,  CT.Name";

            var query = $@"SELECT * 
                        FROM Pictures
                        WHERE Id = {pictureId}

                        SELECT CT.Id, CT.Name
                        FROM PictureContentTag AS PCT
                        INNER JOIN ContentTags AS CT
                        ON PCT.ContentTagId = CT.ID
                        WHERE PCT.PictureId = {pictureId}";

            var connection = OpenConnection(out var closeManually);

            Picture result;

            try
            {
                using (var queryResult = await connection.QueryMultipleAsync(query))
                {
                    result = await queryResult.ReadFirstAsync<Picture>();

                    if (result != null)
                        result.Tags = await queryResult.ReadAsync<ContentTag>();
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public async Task<PictureContentTag> AddPictureContentTagAsync(PictureContentTag pictureContentTag)
        {
            if (pictureContentTag == null)
                throw new ArgumentNullException("The given entity must not be null");

            await DbContext.Set<PictureContentTag>().AddAsync(pictureContentTag);

            return pictureContentTag;
        }

        public async Task RemoveArticleContentTagAsync(PictureContentTag pictureContentTag)
        {
            if (pictureContentTag == null)
                throw new ArgumentNullException("The given entity must not be null");

            await Task.Factory.StartNew(() =>
            {
                DbContext.Set<PictureContentTag>().Remove(pictureContentTag);
            });

        }
    }
}
