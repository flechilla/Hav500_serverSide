﻿using System;
using System.IO;
using System.Threading.Tasks;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Domain.Models.Media;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Havana500.Services
{
    public class ImageService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IArticlesApplicationService _applicationService;
        private readonly IPicturesApplicationService _picturesApplicationService;
        private readonly IUrlHelper _urlHelper;
        private const string _generalImagesConfigKey = "Files:GeneralImagesUploadFolder";

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

        public async Task<Picture> UploadGeneralImageFile(IFormFile formFile, int marketingId, IUrlHelper urlHelper,
            string domain)
        {
            var contentPath = CreateGeneralImageFolder(marketingId);

            try
            {
                var fileNameParts = formFile.FileName.Split('.');
                //var fileName = Guid.NewGuid().ToString() + "." + fileNameParts[1];
                var fileName = $"{fileNameParts[0]}_{marketingId}_{Guid.NewGuid().ToString()}.{fileNameParts[fileNameParts.Length-1]}";

                var fullPath = Path.Combine(contentPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                #region get image dimensions

                //TODO: Add the System.Drawing.Image library

                #endregion

                return await SaveGeneralImageInDb(marketingId, formFile, fileName, fullPath, fileNameParts,
                    urlHelper, domain);
            }

            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<Picture> UploadArticleFile(IFormFile formFile, int articleId, IUrlHelper urlHelper,
            string domain)
        {
            var contentPath = CreateArticleFolder(articleId);
            try
            {
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


                return await SaveMainImageInDb(articleId, formFile, fileName, fullPath, fileNameParts, urlHelper,
                    domain);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Picture> UploadUserImage(IFormFile formFile, string userId, IUrlHelper urlHelper,
            string domain)
        {
            var contentPath = CreateUserImageFolder(userId);
            try
            {
                var fileNameParts = formFile.FileName.Split('.');
                //var fileName = Guid.NewGuid().ToString() + "." + fileNameParts[1];
                var fileName = $"userProfileImage_{Guid.NewGuid().ToString()}.{fileNameParts[1]}";

                var fullPath = Path.Combine(contentPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                #region get image dimensions

                //TODO: Add the System.Drawing.Image library

                #endregion


                return await SaveUserProfileImageInDb(userId, formFile, fileName, fullPath, fileNameParts, urlHelper,
                    domain);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        ///     Read the article body, get the images
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="articleBody"></param>
        /// <param name="urlHelper"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public async Task<string> ProcessArticleBodyImages(int articleId, string articleBody, IUrlHelper urlHelper,
            string domain)
        {
            int indexOfImageTagStart = articleBody.IndexOf("<img");

            if (indexOfImageTagStart == -1)
                return articleBody;

            while (indexOfImageTagStart != -1)
            {
                int indexOfImageTagEnd = articleBody.IndexOf("\">", indexOfImageTagStart);

                //check if the content of the image in the source
                var indexOfImageText = articleBody.IndexOf("image/", indexOfImageTagStart);
                if (indexOfImageText == -1 || indexOfImageText > indexOfImageTagEnd)
                {
                    indexOfImageTagStart = articleBody.IndexOf("<img", indexOfImageTagEnd);
                    continue;
                }

                var imageTypeStartIndex = articleBody.IndexOf('/', indexOfImageTagStart) + 1;
                var imageTypeEndIndex = articleBody.IndexOf(';', indexOfImageTagStart);
                var imgTypeLength = imageTypeEndIndex - imageTypeStartIndex;

                var imageType = articleBody.Substring(imageTypeStartIndex, imgTypeLength);

                var imgContentStartIndex = articleBody.IndexOf(',', imageTypeStartIndex) + 1;
                var imgCotnentLength = indexOfImageTagEnd - imgContentStartIndex;

                var imgContent = articleBody.Substring(imgContentStartIndex, imgCotnentLength);

                var newSrc = await SaveArticleImage(articleId, imgContent, imageType, urlHelper, domain);

                var indexSrcStart = articleBody.IndexOf("=\"", indexOfImageTagStart) + 2;
                //articleBody = articleBody.Replace(imgContent, newSrc);
                articleBody = articleBody.Remove(indexSrcStart, indexOfImageTagEnd - indexSrcStart);
                articleBody = articleBody.Insert(indexSrcStart, newSrc);


                indexOfImageTagEnd = articleBody.IndexOf("\">", indexOfImageTagStart);

                indexOfImageTagStart = articleBody.IndexOf("<img", indexOfImageTagEnd);
            }

            return articleBody;
        }

        private async Task<string> SaveArticleImage(int articleId, string imgContent, string imgType,
            IUrlHelper urlHelper, string domain)
        {
            var contentBytes = Convert.FromBase64String(imgContent);
            var imgStream = new MemoryStream(contentBytes);

            var contentPath = CreateArticleFolder(articleId);

            var imgName = Guid.NewGuid().ToString();

            var fullPath = Path.Combine(contentPath, imgName) + "." + imgType;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imgStream.CopyToAsync(stream);
            }

            var picture = new Picture()
            {
                FullPath = fullPath,
                IsNew = true,
                MimeType = "image/" + imgType,
                PictureType = PictureType.ArticlePicture,
                ArticleId = articleId,
                PictureExtension = imgType,
                RelativePath =
                    domain + urlHelper.Content(
                        $"~/articlesUploadImages/{articleId}/{imgName}.{imgType}") //TODO: ADd the domain
            };

            _picturesApplicationService.Add(picture);
            await _picturesApplicationService.SaveChangesAsync();

            return picture.RelativePath;
        }

        private string CreateArticleFolder(int articleId)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var articleUploadFolder = _configuration.GetSection("Files:ArticleUploadFolder").Value;
            var contentPath = Path.Combine(webRootPath, articleUploadFolder, articleId.ToString());

            if (!Directory.Exists(contentPath))
                Directory.CreateDirectory(contentPath);

            return contentPath;
        }

        private string CreateUserImageFolder(string userId)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var userImageUploadFolder = _configuration.GetSection("Files:UserImageUploadFolder").Value;
            var contentPath = Path.Combine(webRootPath, userImageUploadFolder, userId);

            if (!Directory.Exists(contentPath))
                Directory.CreateDirectory(contentPath);

            return contentPath;
        }

        private string CreateGeneralImageFolder(int marketingId)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var marketingUploadFolder = _configuration.GetSection(_generalImagesConfigKey).Value;
            var contentPath = Path.Combine(webRootPath, marketingUploadFolder);

            if (!Directory.Exists(contentPath))
                Directory.CreateDirectory(contentPath);

            return contentPath;
        }

        private async Task<Picture> SaveMainImageInDb(int articleId, IFormFile formFile, string fileName,
            string fullPath, string[] fileNameParts, IUrlHelper urlHelper, string domain)
        {
            // if(!await _picturesApplicationService.ExistsAsync(img=>img.ArticleId == articleId && img.PictureType == PictureType.ArticleMainPicture))
            // {
            //TODO: improve this query
            await _picturesApplicationService.RemoveAsync(img =>
                img.ArticleId == articleId && img.PictureType == PictureType.ArticleMainPicture);
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
                RelativePath = domain + urlHelper.Content($"~/articlesUploadImages/{articleId}/{fileName}")
            };

            var resPic = await _picturesApplicationService.AddAsync(picture);
            await _picturesApplicationService.SaveChangesAsync();

            return resPic;
        }

        private async Task<Picture> SaveGeneralImageInDb(int marketingId, IFormFile formFile, string fileName,
            string fullPath, string[] fileNameParts, IUrlHelper urlHelper, string domain)
        {
            var picture = await _picturesApplicationService.SingleOrDefaultAsync(marketingId);
            picture.RelativePath = domain + urlHelper.Content($"~/{_configuration.GetSection(_generalImagesConfigKey).Value}/{fileName}");
            picture.FullPath = fullPath;
            picture.IsNew = true;
            picture.MimeType = formFile.ContentType;
            if (string.IsNullOrEmpty(picture.SeoFilename))
            {
                picture.SeoFilename = fileNameParts[0];
            }
            picture.PictureExtension = fileNameParts[1];


            var resPic = await _picturesApplicationService.UpdateAsync(picture);
            await _picturesApplicationService.SaveChangesAsync();

            return resPic;
        }


        private async Task<Picture> SaveUserProfileImageInDb(string userId, IFormFile formFile, string fileName,
            string fullPath, string[] fileNameParts, IUrlHelper urlHelper, string domain)
        {
            await _picturesApplicationService.RemoveAsync(img =>
                img.UserId == userId && img.PictureType == PictureType.Avatar);

            var picture = new Picture()
            {
                FullPath = fullPath,
                IsNew = true,
                MimeType = formFile.ContentType,
                PictureType = PictureType.Avatar,
                SeoFilename = fileNameParts[0],
                PictureExtension = fileNameParts[1],
                RelativePath = domain + urlHelper.Content($"~/{_configuration.GetSection("Files:UserImageUploadFolder").Value}/{userId}/{fileName}"),
                
            };

            var resPic = await _picturesApplicationService.AddAsync(picture);
            await _picturesApplicationService.SaveChangesAsync();

            return resPic;
        }
    }
}