using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Havana500.Models.CommentViewModel
{
    public class CommentsCreateViewModel : CommentsBaseViewModel
    {
        public int ParentId { get; set; }

    }
}
