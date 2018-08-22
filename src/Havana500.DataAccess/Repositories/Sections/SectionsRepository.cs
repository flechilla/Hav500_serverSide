using Havana500.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Havana500.DataAccess.Contexts;
using Dapper;

namespace Havana500.DataAccess.Repositories.Sections
{
    public class SectionsRepository : BaseRepository<Section, int>, ISectionsRepository
    {
        public SectionsRepository(Havana500DbContext dbContext) : base(dbContext)
        {
        }

        public string[] GetMainSectionNames()
        {
            using(var connection = OpenConnection(out bool closeConn))
            {
                var query = @"SELECT Name
                              FROM Sections
                              WHERE IsMainSection = 1";
                var result = connection.Query<string>(query);
                connection.Close();

                return result.ToArray();
            }
        }

        public IQueryable<Section> GetMainSections()
        {
            return Entities.
                Where(e => e.IsMainSection);
        }

        public string[] GetSecondarySectionNames(string outerSectionName)
        {
            var outerSection = Entities.FirstOrDefault(s => s.Name == outerSectionName);

            if (outerSection == null)
                throw new ArgumentException("There is not section with the name: " + outerSectionName);

            return Entities.
                Where(s => s.ParentSectionId == outerSection.Id).
                Select(s=>s.Name).
                ToArray();
        }

        public IQueryable<Section> GetSecondarySections(string outerSectionName)
        {
            var outerSection = Entities.FirstOrDefault(s => s.Name == outerSectionName);

            if (outerSection == null)
                throw new ArgumentException("There is not section with the name: " + outerSectionName);

            return Entities.
                Where(s => s.ParentSectionId == outerSection.Id);
        }
    }
}
