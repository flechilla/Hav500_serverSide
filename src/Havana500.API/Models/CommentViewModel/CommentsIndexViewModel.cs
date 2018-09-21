namespace Havana500.Models.CommentViewModel
{
    public class CommentsIndexViewModel : CommentsBaseViewModel
    {
        public int Id { get; set; }

        public string CreateAtHumanized { get; set; }

        public string ModifiedAtHumanized { get; set; }
    }
}
