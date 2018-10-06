using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Domain;
using Havana500.Models.ArticleViewModels;
using AutoMapper;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Models.ArticleTagViewModels;
using Havana500.Models.CommentViewModel;
using System.ComponentModel.DataAnnotations;
using Havana500.API.Models.ArticleViewModels;

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
        private ITagApplicationService TagApplicatioService { get; set; }
        public ArticlesController(IArticlesApplicationService appService, IMapper mapper, ITagApplicationService tagApplicationService) : base(appService, mapper)
        {
            this.TagApplicatioService = tagApplicationService;
        }
        /// <summary>
        ///     Indicates the amount of comments to show when showing the article.
        ///     After this, the user can pull more comments.
        /// </summary>
        private const int DEFAULT_AMOUNT_OF_COMMENTS_PER_ARTICLE = 20;

        private const int DEFAULT_AMOUNT_OF_CONTENT_FOR_SECOND_LEVEL = 20;

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

        /// <summary>
        ///     Gets the Articles related to the article with the given <param name="articleId"></param>
        ///</summary>
        /// <param name="articleId">The Id of the Article</param>
        /// <returns>The related articles with short properties</returns>
        /// <response code="200">When the entity is found by its id</response>
        /// <response code="404">When the entity couldn't be found</response>
        [HttpGet()]
        
        public async Task<IActionResult> GetRelatedArticles(int articleId)
        {
            if (!await this.ApplicationService.ExistsAsync(articleId))
            {
                return NotFound(articleId);
            }

            var result = await ApplicationService.GetRelatedArticles(articleId);

            var resultVM = Mapper.Map<IEnumerable<ArticleBasicDataViewModel>>(result);

            return Ok(resultVM);
        }

        [HttpGet()]
        /// <summary>
        ///     Gets the articles that belongs to the given <param name="sectionName"></param>, 
        ///     sending just the given <param name="amountOfArticles"></param> that belongs to the 
        ///     given <param name="currentPage"></param>
        /// </summary>
        /// <param name="sectionName">The name of the section that the articles belongs.</param>
        /// <param name="currentPage">The current page</param>
        /// <param name="amountOfArticles">The amount of articles per page.</param>
        /// <returns></returns>
        /// <response code="200">When there is a section with the given name</response>
        /// <response code="404">When there is not a section with the given name</response>
        public async Task<IActionResult> GetArticlesBasicDataBySectionName(string sectionName, int currentPage,
            int amountOfArticles = DEFAULT_AMOUNT_OF_CONTENT_FOR_SECOND_LEVEL)
        {
            var articles = await this.ApplicationService.GetArticlesBasicDataBySectionName(sectionName, currentPage, amountOfArticles);

            var articlesVM = Mapper.Map<IEnumerable<ArticleBasicDataViewModel>>(articles);

            return Ok(articlesVM);
        }



        #region ADMIN AREA
        [Area("Admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddTagToArticle([FromBody, Required]ArticleContentTagViewModel articleContentTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ApplicationService.Exists(articleContentTag.ArticleId))
            {
                return NotFound("Article with id: " + articleContentTag.ArticleId);
            }

            if (!TagApplicatioService.Exists(articleContentTag.ContentTagId))
            {
                return NotFound("Tag with id: " + articleContentTag.ContentTagId);
            }

            var result =
                await ApplicationService.AddArticleContentTagAsync(Mapper.Map<ArticleContentTag>(articleContentTag));

            await ApplicationService.SaveChangesAsync();

            var viewModelRes = Mapper.Map<ArticleContentTagViewModel>(result);

            return CreatedAtAction("GET", viewModelRes);

        }

        [Area("Admin")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveTagToArticle(int articleId, int tagId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ApplicationService.Exists(articleId))
            {
                return NotFound("Article with id: " + articleId);
            }

            if (!TagApplicatioService.Exists(tagId))
            {
                return NotFound("Tag with id: " + tagId);
            }

            await ApplicationService.RemoveArticleContentTagAsync(new ArticleContentTag { ArticleId = articleId, ContentTagId = tagId });

            await ApplicationService.SaveChangesAsync();

            return Ok();

        }
        [Area("Admin")]
        public override async Task<IActionResult> Post([FromBody, Required]ArticleCreateViewModel newArticle){
            return await base.Post(newArticle);
        }

        [Area("Admin")]
        public override async Task<IActionResult> Put(int articleId, [FromBody, Required]ArticleCreateViewModel newArticle){
            return await base.Put(articleId, newArticle);
        }

        [Area("Admin")]
        public override async Task<IActionResult> Delete(int articleId){
            return await base.Delete(articleId);
        }

        [HttpGet()]
        public IActionResult GetArticlesWithNewCommentsInfo(int daysAgo, int pageNumber, int pageSize, string columnNameForSorting, string sortingType, string columnsToReturn = "*"){
            var result = ApplicationService.GetArticlesWithNewCommentsInfo(daysAgo, pageNumber, pageSize, columnNameForSorting, sortingType, columnsToReturn = "*");

            var resultViewModel = Mapper.Map<IEnumerable<ArticleCommentsInfoViewModel>>(result);

            return Ok(resultViewModel);
        }
        #endregion

    }
}