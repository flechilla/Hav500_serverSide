using System.Collections.Generic;

namespace Havana500.Models
{
    public class PaginationViewModel<TIndexViewModel>
    {
        public long Length { get; set; }

        public IEnumerable<TIndexViewModel> Entities { get; set; }
    }
}
