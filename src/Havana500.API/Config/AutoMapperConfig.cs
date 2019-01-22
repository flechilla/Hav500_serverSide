
using AutoMapper;
using Humanizer;
using Havana500.Domain;
using Havana500.Models.ArticleTagViewModels;
using Havana500.Models.ArticleViewModels;
using Havana500.Models.CommentViewModel;
using Havana500.Models.SectionViewModel;
using Havana500.API.Models.StatsViewModels;
using Havana500.Models.TagViewModels;
using Havana500.API.Models.ArticleViewModels;
using Havana500.API.Models.PictureViewModels;
using Havana500.API.Models.SectionViewModel;
using Havana500.Domain.Models.Media;
using Havana500.Models.PictureContentTagViewModels;
using Havana500.Models.PictureViewModels;
using Havana500.Models.SystemUsers;

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


            #region Comments configs
            this.CreateMap<Comment, CommentsIndexViewModel>().AfterMap((src, dest) =>
               {
                   dest.CreatedAtHumanized = src.CreatedAt.Humanize();
                   dest.ModifiedAtHumanized = src.ModifiedAt.Humanize();
               });

            this.CreateMap<CommentsCreateViewModel, Comment>();

            this.CreateMap<CommentsEditViewModel, Comment>();

            this.CreateMap<CommentsBaseViewModel, Comment>();
            #endregion

            #region Article configs
            this.CreateMap<Article, ArticleIndexViewModel>().AfterMap((src, dest) =>
            {
                dest.PublicationDateHumanized = src.StartDateUtc.Humanize();
            });
            this.CreateMap<Article, ArticleBasicDataViewModel>().AfterMap((src, dest) =>
            {
                dest.PublicationDateHumanized = src.StartDateUtc.Humanize();
            });
            this.CreateMap<ArticleCreateViewModel, Article>();
            this.CreateMap<ArticleBaseViewModel, Article>();
            this.CreateMap<Article, ArticleBaseViewModel>();

            this.CreateMap<Article, TrendingArticleViewModel>();
            this.CreateMap<ArticleContentTag, ArticleContentTagViewModel>().ReverseMap();
            this.CreateMap<Article, ArticleCommentsInfoViewModel>();
            #endregion

            #region Section configs
            this.CreateMap<Section, SectionIndexViewModel>();
            this.CreateMap<SectionCreateViewModel, Section>();
            this.CreateMap<SectionBaseViewModel, Section>();
            this.CreateMap<Section, SectionBaseViewModel>();
            this.CreateMap<Section, BasicSectionViewModel>();

            #endregion

            this.CreateMap<ContentTag, TagBaseViewModel>();
            this.CreateMap<ContentTag, TagIndexViewModel>();

            this.CreateMap<TagBaseViewModel, ContentTag>();

            this.CreateMap<Picture, BasePictureViewModel>().ReverseMap();
            this.CreateMap<PictureContentTag, PictureContentTagViewModel>().ReverseMap();
            this.CreateMap<Picture, IndexPictureViewModel>();

            this.CreateMap<BaseUserViewMode, ApplicationUser>();
            this.CreateMap<ApplicationUser, UserIndexViewModel>();




        }
    }
}
