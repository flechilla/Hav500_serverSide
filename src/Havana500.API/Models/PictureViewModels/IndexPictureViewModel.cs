using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havana500.API.Models.PictureViewModels;
using Havana500.Models.TagViewModels;

namespace Havana500.Models.PictureViewModels
{
    public class IndexPictureViewModel : BasePictureViewModel
    {

        public List<TagIndexViewModel> Tags { get; set; }

    }
}
