using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havana500.API.Models.PictureViewModels;

namespace Havana500.Models.ArticleViewModels
{
    public class ArticleBasicDataViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string PublicationDateHumanized { get; set; }

        public string Body { get; set; }

        public int Views { get; set; }

        public int ApprovedCommentCount { get; set; }

        public BasePictureViewModel MainPicture { get; set; }
    }
}
