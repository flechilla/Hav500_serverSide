using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Policy;
using System.Threading.Tasks;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Domain.Models.Media;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.ML.Transforms;


namespace Havana500.Services
{
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IArticlesApplicationService _applicationService;
        private readonly IPicturesApplicationService _picturesApplicationService;
        private readonly IUrlHelper _urlHelper;

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

        public async Task<bool> UploadArticleFile(IFormFile formFile, int articleId, IUrlHelper urlHelper)
        {            
            var webRootPath = _hostingEnvironment.WebRootPath;
            var articleUploadFolder = _configuration.GetSection("Files:ArticleUploadFolder").Value;
            var contentPath = Path.Combine(webRootPath, articleUploadFolder, articleId.ToString());
            try
            {
             if (!Directory.Exists(contentPath))
                    Directory.CreateDirectory(contentPath);
                var fileNameParts = formFile.FileName.Split('.');
                //var fileName = Guid.NewGuid().ToString() + "." + fileNameParts[1];
                var fileName = "mainPicture" + "." + fileNameParts[1];

                var fullPath = Path.Combine(contentPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                #region get image dimensions
                //TODO: Add the System.Drawing.Image library
                #endregion

               
                await SaveMainImageInDb(articleId, formFile, fileName, fullPath, fileNameParts, urlHelper);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;

        }

        private async Task SaveMainImageInDb(int articleId, IFormFile formFile,string fileName, string fullPath, string[] fileNameParts, IUrlHelper urlHelper){

            // if(!await _picturesApplicationService.ExistsAsync(img=>img.ArticleId == articleId && img.PictureType == PictureType.ArticleMainPicture))
            // {
                //TODO: improve this query
                await _picturesApplicationService.RemoveAsync(img => img.ArticleId == articleId && img.PictureType == PictureType.ArticleMainPicture);
            // }

             var picture = new Picture()
                {
                    FullPath = fullPath,
                    IsNew = true,
                    MimeType = formFile.ContentType,
                    PictureType = PictureType.ArticleMainPicture,
                    SeoFilename = fileNameParts[0],
                    ArticleId = articleId,
                    PictureExtension = fileNameParts[1],
                    RelativePath = urlHelper.Content($"~/articlesUploadImages/{articleId}/{fileName}")
                };

             _picturesApplicationService.Add(picture);
             await _picturesApplicationService.SaveChangesAsync();
        }
    }
}
