using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;

namespace Havana500.DataAccess.Repositories.Tags
{
    public class TagRepository : BaseRepository<ContentTag, int>, ITagRepository
    {
        public TagRepository(Havana500DbContext dbContext) : base(dbContext)
        {
        }
    }
}
