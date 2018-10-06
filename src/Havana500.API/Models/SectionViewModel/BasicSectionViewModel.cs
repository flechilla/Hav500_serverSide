using System.Collections.Generic;

namespace Havana500.API.Models.SectionViewModel
{
    public class BasicSectionViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public IEnumerable<BasicSectionViewModel> SubSections { get; set; }
    }
}