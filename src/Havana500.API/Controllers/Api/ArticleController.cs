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
using Havana500.Models.CommentViewModel;
using Microsoft.EntityFrameworkCore.Query;

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

            var articleComments = await this.ApplicationService.GetComments(articleId, currentPage, amountOfComments);

            var viewModelComments = this.Mapper.Map<IEnumerable<CommentsIndexViewModel>>(articleComments);

            return Ok(viewModelComments);
        }

        /// <summary>
        ///     Gets the Article with the given <param name="articleId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="articleId">The Id of the Article</param>
        /// <returns>The Article with its related Tags</returns>
        /// <response code="200">When the entity is found by its id</response>
        /// <response code="404">When the entity couldn't be found</response>
        [HttpGet]
        public async Task<IActionResult> GetArticleWithTags(int articleId)
        {
            if (!await this.ApplicationService.ExistsAsync(articleId))
            {
                return NotFound(articleId);
            }

            var result = ApplicationService.GetArticleWithTags(articleId);

            var resultViewModel = Mapper.Map<ArticleIndexViewModel>(result);

            return Ok(resultViewModel);
        }

    }
}