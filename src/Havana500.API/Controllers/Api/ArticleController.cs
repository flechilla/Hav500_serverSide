using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Domain;
using Havana500.Models.ArticleViewModels;
using AutoMapper;

namespace Havana500.Controllers.Api
{
    public class ArticlesController : BaseApiController<
        IArticlesApplicationService,
        Article,
        int,
        ArticleBaseViewModel,
        ArticleCreateViewModel,
        ArticleCreateViewModel,
        ArticleIndexViewModel>
    {
        public ArticlesController(IArticlesApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }
        /// <summary>
        ///     Indicates the amount of comments to show when showing the article.
        ///     After this, the user can pull more comments.
        /// </summary>
        private const int DEFAULT_AMOUNT_OF_COMMENTS_PER_ARTICLE = 20;

        [HttpGet]
        public async Task<IActionResult> GetComments(int articleId, int currentPage, int amountOfComments = DEFAULT_AMOUNT_OF_COMMENTS_PER_ARTICLE)
        {
            if (!await this.ApplicationService.ExistsAsync(articleId))
            {
                return NotFound(articleId);
            }

            var articleComments = this.ApplicationService.GetComments(articleId, currentPage, amountOfComments);

            var viewModelComments = this.Mapper.Map<Models.CommentViewModel.CommentsIndexViewModel>(articleComments);

            return Ok(viewModelComments);
        }

    }
}