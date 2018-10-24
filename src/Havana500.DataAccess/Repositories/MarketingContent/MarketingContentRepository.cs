using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;

namespace Havana500.DataAccess.Repositories.MarketingContent
{
    public class MarketingContentRepository : BaseRepository<Domain.MarketingContent, int>, IMarketingContentRepository
    {
        public MarketingContentRepository(Havana500DbContext dbContext) : base(dbContext)
        {
        }
    }
}
