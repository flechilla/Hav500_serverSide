using Havana500.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Repositories;
using Havana500.Domain;
using System.Linq;

namespace Havana500.Business.ApplicationServices.Section
{
    public class SectionsApplicationService : BaseApplicationService<Domain.Section, int>, ISectionsApplicationService
    {
        protected SectionsApplicationService(ISectionsRepository repository) : base(repository)
        {
        }

        public new ISectionsRepository Repository { get { return this.Repository as ISectionsRepository; } }

        public string[] GetMainSectionNames()
        {
            return Repository.GetMainSectionNames();
        }

        public IQueryable<Domain.Section> GetMainSections()
        {
            return Repository.GetMainSections();
        }

        public string[] GetSecondarySectionNames(string outerSectionName)
        {
            return Repository.GetSecondarySectionNames(outerSectionName);
        }

        public IQueryable<Domain.Section> GetSecondarySections(string outerSectionName)
        {
            return Repository.GetSecondarySections(outerSectionName);
        }
    }
}
