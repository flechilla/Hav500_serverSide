using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Business.Base;
using Havana500.DataAccess.Repositories;

namespace Havana500.Business.ApplicationServices.MarketingContent
{
    public class MarketingContentApplicationService : BaseApplicationService<Domain.MarketingContent, int>, IMarketingContentApplicationService
    {
        public MarketingContentApplicationService(IBaseRepository<Domain.MarketingContent, int> repository) : base(repository)
        {
        }
    }
}
