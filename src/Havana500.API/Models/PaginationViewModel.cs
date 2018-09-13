using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models
{
    public class PaginationViewModel<TIndexViewModel>
    {
        public long Length { get; set; }

        public IEnumerable<TIndexViewModel> Entities { get; set; }
    }
}
