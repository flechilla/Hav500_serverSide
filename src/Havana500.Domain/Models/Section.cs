using Havana500.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Havana500.Domain
{
    /// <summary>
    ///     This entity represent the outer container of data
    ///     in the app. Its work as the elements of the header and
    ///     is direct container of subSections
    /// </summary>
    public class Section : AuditableAndTrackableEntity<int> //TODO: Add the property Description.
    {
        /// <summary>
        ///     The name of the section
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Description of the current Section. 
        ///     This is an optional data.     
        /// </summary>
        public string Description {get; set;}

        /// <summary>
        ///     Gets or sets the value that indicates
        ///     the container of the current section.
        ///     
        /// </summary>
        /// <remarks>
        ///     If the value of this field is equal to -1, this means 
        ///     that the instance is a root section, so the 
        ///     field <see cref="IsMainSection"/> should be true.
        /// </remarks>
        public short ParentSectionId { get; set; }

        /// <summary>
        ///     Gets or sets the references to the sections that are
        ///     contained by this section.
        /// </summary>
        public ICollection<Section> SubSection { get; set; }

        /// <summary>
        ///     If true indicate that this instance is a root, 
        ///     so it may contains other sections and should by
        ///     used in the header.
        /// </summary>
        public bool IsMainSection { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the 
        ///     amount of times that the user has entered
        ///     to this section.
        /// </summary>
        public ulong Views { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the 
        ///     amount of comments that the section has.
        /// </summary>
        public uint AmountOfComments { get; set; }
    }
}
