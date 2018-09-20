using System;
using Havana500.Models;

namespace Havana500.API.Models.ArticleViewModels
{
    public class ArticleCommentsInfoViewModel : BaseViewModel<int>
    {
        public string Title { get; set; }

        public int AmountOfComments { get; set; }

        public DateTime StartDateUtc { get; set; }

        
    }
}