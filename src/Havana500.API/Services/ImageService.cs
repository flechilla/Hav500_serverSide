using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Domain.Models.Media;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Havana500.Services
{
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IArticlesApplicationService _applicationService;
        private readonly IPicturesApplicationService _picturesApplicationService;

        public ImageService(IConfiguration configuration, 
            IHostingEnvironment hostingEnvironment,
            IArticlesApplicationService applicationService,
            IPicturesApplicationService picturesApplicationService)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _applicationService = applicationService;
            _picturesApplicationService = picturesApplicationService;
        }

        public async Task<bool> UploadArticleFile(IFormFile formFile, int articleId)
        {
            
            var webRootPath = _hostingEnvironment.WebRootPath;
            var articleUploadFolder = _configuration.GetSection("ArticleUploadFolder").Value;
            var contentPath = Path.Combine(webRootPath, articleUploadFolder, articleId.ToString());
            try
            {
             

                if (!Directory.Exists(contentPath))
                    Directory.CreateDirectory(contentPath);

                var fileName = Guid.NewGuid().ToString();
                var fullPath = Path.Combine(contentPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                var picture = new Picture()
                {
                    FullPath = fullPath,
                    IsNew = true,
                    MimeType = formFile.ContentType,
                    PictureType = PictureType.ArticleMainPicture,
                    SeoFilename = formFile.Name,
                    ArticleId = articleId,

                };

                _picturesApplicationService.Add(picture);
                await _picturesApplicationService.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;

        }
    }
}
