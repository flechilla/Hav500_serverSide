using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Havana500.Domain.Enums;

namespace Havana500.Models.CommentViewModel
{
    public class CommentsBaseViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Body { get; set; }

        public string ApplicationUserId { get; set; }
        
        public int Likes { get; set; }
    }
}
