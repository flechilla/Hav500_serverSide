using Havana500.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Repositories;
using Havana500.Domain;
using System.Linq;
using Accord.Math;

namespace Havana500.Business.ApplicationServices.Section
{
    public class SectionsApplicationService : BaseApplicationService<Domain.Section, int>, ISectionsApplicationService
    {
        public SectionsApplicationService(ISectionsRepository repository) : base(repository)
        {
        }

        public new ISectionsRepository Repository { get { return base.Repository as ISectionsRepository; } }

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

          /// <summary>
        ///     Get the sections and its subsections
        ///     <remarks>This method is implemented with Dapper for optimization reasons</remarks>
        /// </summary>
        /// <returns>An array with the Id and Name of the main section, and it's sub-sections</returns>
        public Domain.Section[] GetSectionAndSubSection(){
            var sections = Repository.GetSectionAndSubSection();
            var outputSections= new List<Domain.Section>();

              for (var i = 0; i < sections.Length; i++)
              {
                  var section = sections[i];
                  if (section.IsMainSection)
                  {
                      outputSections.Add(section);
                      sections.RemoveAt(i);
                      continue;
                  }

                  var parentSection = sections.FirstOrDefault(s => s.Id == section.ParentSectionId);
                  if (parentSection == null)
                      parentSection = outputSections.First(s => s.Id == section.ParentSectionId);

                  parentSection.SubSections.Add(section);
              }

              return outputSections.ToArray();
        }
    }
}
