using Havana500.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain;
using System.Linq;

namespace Havana500.Business.ApplicationServices.Section
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Havana500.Domain.Section" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Havana500.Domain.Section" /> entity
    ///         through the <see cref="ISectionsRepository" /> but when necessary
    ///         adds some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface ISectionsApplicationService : IBaseApplicationService<Domain.Section, int>
    {
        /// <summary>
        ///     To get the sections that are root.
        /// </summary>
        /// <returns>Returns the mains sections. Use this method to get the sections
        /// to be rendered in the header menu</returns>
        IQueryable<Domain.Section> GetMainSections();

        /// <summary>
        ///     To get the sections that belongs to a section
        ///     with the given <paramref name="outerSectionName"/>
        /// </summary>
        /// <param name="outerSectionName">The name of the section that is the container</param>
        /// <returns></returns>
        IQueryable<Domain.Section> GetSecondarySections(string outerSectionName);

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


          /// <summary>
        ///     Get the sections and its subsections
        ///     <remarks>This method is implemented with Dapper for optimization reasons</remarks>
        /// </summary>
        /// <returns>An array with the Id and Name of the main section, and it's sub-sections</returns>
        Domain.Section[] GetSectionAndSubSection();
    }
}
