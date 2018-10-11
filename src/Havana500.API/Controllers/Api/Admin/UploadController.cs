using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await _imageService.UploadArticleFile(file, articleId, urlHelper);

            if (result)
                return Ok();
                //return Created("The image was upload.");//TODO: get the relative path and send it back
            
            return new StatusCodeResult(500);
        }
    }
}