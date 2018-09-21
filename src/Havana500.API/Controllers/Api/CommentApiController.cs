using System.Linq;
using Havana500.Business.ApplicationServices.Comments;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Havana500.Models.CommentViewModel;
using Havana500.Domain;

namespace Havana500.Controllers.Api
{
    public class CommentsController : BaseApiController<ICommentsApplicationService, 
        Comment, 
        int, 
        CommentsBaseViewModel,
        CommentsCreateViewModel, 
        CommentsEditViewModel, 
        CommentsIndexViewModel>
    {
        private readonly ICommentsApplicationService _commentApplicationService;
        private readonly IMapper _mapper;
        private const int _defaultAmountOfComments = 20;

        public CommentsController(ICommentsApplicationService commentApplicationService,
            IMapper mapper) : base(commentApplicationService, mapper)
        {
            _mapper = mapper;
            _commentApplicationService = commentApplicationService;
        }

        /// <summary>
        ///     Gets a list of Comments related to the given article.
        /// </summary>
        /// <param name="articleId">The Id of the <see cref="Article"/>that is parent of the comments</param>
        /// <param name="page">The page of the wanted comments. This can be seen as the amount of times that the system has retrieved comments</param>
        /// <param name="amountOfComments">The amount of comments to retrieve</param>
        /// <returns>The entity with ID=<paramref name="id"/>, null if not found</returns>
        /// <response code="200">When the entity is found by its id</response>
        /// <response code="404">When the entity couldn't be found</response>
        [HttpGet()]
        public IActionResult GetArticleComments(int articleId, int page, int amountOfComments = _defaultAmountOfComments)
        {
            
           var comments = _commentApplicationService.ReadAll(articleId, Domain.Enums.Discriminator.Article).
                Skip(page*amountOfComments).
                Take(amountOfComments).
                ToAsyncEnumerable();

            var outputComments = _mapper.Map<CommentsIndexViewModel>(comments);

            return Ok(outputComments);
        }
    }
}
