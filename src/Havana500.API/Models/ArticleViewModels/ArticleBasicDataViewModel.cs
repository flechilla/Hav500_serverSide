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

        public int AmountOfComments { get; set; }

        public BasePictureViewModel MainPicture { get; set; }

        public string CreationDay { get; set; }

        public string CreationMonth { get; set; }
    }
}
