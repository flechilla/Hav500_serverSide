using System.ComponentModel.DataAnnotations;

namespace Havana500.Models.CommentViewModel
{
    public class CommentsBaseViewModel : BaseViewModel<int>
    {
        [Required]
        [MaxLength(255)]
        public string Body { get; set; }

        public string ApplicationUserId { get; set; }
        
        public int Likes { get; set; }

        public int ArticleId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }
    }
}
