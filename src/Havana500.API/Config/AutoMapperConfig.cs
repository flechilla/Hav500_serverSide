using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using Havana500.Domain;
using Havana500.Models.CommentViewModel;


namespace Havana500.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //this.CreateMap<Thought, ThoughtIndexViewModel>()
            //    .AfterMap((src, dest) =>
            //    {
            //        dest.CreateAtHumanized = src.CreatedAt.Humanize();
            //        dest.ModifiedAtHumanized = src.ModifiedAt.Humanize();
            //    });

            

            this.CreateMap<Comment, CommentsIndexViewModel>().AfterMap((src, dest) =>
               {
                   dest.CreateAtHumanized = src.CreatedAt.Humanize();
                   dest.ModifiedAtHumanized = src.ModifiedAt.Humanize();
               });

            this.CreateMap<CommentsCreateViewModel, Comment>();

            this.CreateMap<CommentsEditViewModel, Comment>();

            this.CreateMap<CommentsBaseViewModel, Comment>();
        }
    }
}
