using System.Threading.Tasks;
using Havana500.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Havana500.Controllers.Api.Admin
{
    [Produces("application/json")]
    [Route("api/v1/Upload")]
    [Area("Admin")]
    public class UploadController : Controller
    {
        private readonly ImageService _imageService;

        public UploadController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("UploadArticleMainPicture")]
        public async Task<IActionResult> UploadArticleMainPicture(int articleId, IFormFile file)
        {
            IUrlHelper urlHelper = new UrlHelper(ControllerContext);
            var domain = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";

            var result = await _imageService.UploadArticleFile(file, articleId, urlHelper, domain);


            if (result != null)
                return Ok(result);
            //return Created("The image was upload.");//TODO: get the relative path and send it back

            return new StatusCodeResult(500);
        }
    }
}