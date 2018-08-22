using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havana500.Domain.Models.Media;
using Havana500.Business.Base;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Pictures;

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
    }
}
