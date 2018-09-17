using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models.SectionViewModel
{
    public class SectionBaseViewModel : BaseViewModel<int>
    {
        /// <summary>
        ///     The name of the section
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Description of the current Section. 
        ///     This is an optional data.     
        /// </summary>
        public string Description { get; set; }

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
        public int ParentSectionId { get; set; }


        /// <summary>
        ///     If true indicate that this instance is a root, 
        ///     so it may contains other sections and should by
        ///     used in the header.
        /// </summary>
        public bool IsMainSection { get; set; }

    }
}
