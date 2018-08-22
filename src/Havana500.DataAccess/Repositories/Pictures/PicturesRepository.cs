using Havana500.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Havana500.DataAccess.Contexts;
using System.Linq;
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
                Include(p=>p.MediaStorage);
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
    }
}
