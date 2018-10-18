using System.Collections.Generic;
using Havana500.Domain.Models.Media;
using Havana500.Models;
using Havana500.Models.TagViewModels;

namespace Havana500.API.Models.PictureViewModels
{
    public class BasePictureViewModel : BaseViewModel<int>
    {

        public string RelativePath { get; set; }
        public string SeoFileName { get; set; }

        public string MimeType { get; set; }

        public string HRef { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public PictureType PictureType { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public string CompanyName { get; set; }

        public string LanguageCulture { get; set; }



    }
}