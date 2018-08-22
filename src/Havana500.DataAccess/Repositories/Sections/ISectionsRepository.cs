using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain;
using System.Linq;

namespace Havana500.DataAccess.Repositories
{
    public interface ISectionsRepository : IBaseRepository<Section, int>
    {
        /// <summary>
        ///     To get the sections that are root.
        /// </summary>
        /// <returns>Returns the mains sections. Use this method to get the sections
        /// to be rendered in the header menu</returns>
        IQueryable<Section> GetMainSections();

        /// <summary>
        ///     To get the sections that belongs to a section
        ///     with the given <paramref name="outerSectionName"/>
        /// </summary>
        /// <param name="outerSectionName">The name of the section that is the container</param>
        /// <returns></returns>
        IQueryable<Section> GetSecondarySections(string outerSectionName);

        /// <summary>
        ///     Get the name of the main sections.
        ///     <remarks>This method is implemented with Dapper for optimization reasons</remarks>
        /// </summary>
        /// <returns>An array with the name of the main sections</returns>
        string[] GetMainSectionNames();

        /// <summary>
        ///     Get the name of the sections that are children of the section with the given <paramref name="outerSectionName"/>.
        ///     <remarks>This method is implemented with Dapper for optimization reasons</remarks>
        /// </summary>
        /// <returns>An array with the name of secondary sections</returns>
        string[] GetSecondarySectionNames(string outerSectionName);
    }
}
