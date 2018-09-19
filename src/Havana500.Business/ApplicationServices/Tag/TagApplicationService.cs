using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Business.Base;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Tags;
using Havana500.Domain;

namespace Havana500.Business.ApplicationServices.Tag
{
    public class TagApplicationService : BaseApplicationService<ContentTag, int>, ITagApplicationService
    {
        public TagApplicationService(ITagRepository repository) : base(repository)
        {
        }

        public new ITagRepository Repository => base.Repository as ITagRepository;
    }
}
