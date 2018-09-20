using Havana500.Models;

namespace Havana500.API.Models.StatsViewModels
{
    public class TrendingArticleViewModel : BaseViewModel<int>
    {
        public string Title { get; set; }

        public int Views { get; set; }

        public int AmountOfComments { get; set; }

        public int ApprovedCommentCount { get; set; }

        public int NotApprovedCommentCount { get; set; }

    }
}