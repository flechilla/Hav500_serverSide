using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Havana500.API.Models.PictureViewModels;
using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Domain;
using Havana500.Domain.Models.Media;
using Havana500.Models.PictureContentTagViewModels;
using Havana500.Models.PictureViewModels;
using Havana500.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Havana500.Controllers.Api
{
    [Produces("application/json")]
    public class MarketingPicturesController : BaseApiController<IPicturesApplicationService, Picture, int,
        BasePictureViewModel, BasePictureViewModel, BasePictureViewModel, IndexPictureViewModel>
    {
        private readonly ITagApplicationService _tagApplicationService;
        private readonly ImageService _imageService;
        public MarketingPicturesController(IPicturesApplicationService appService, IMapper mapper, ITagApplicationService tagApplicationService, ImageService imageService) : base(appService,
            mapper)
        {
            _tagApplicationService = tagApplicationService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImagesByLevel(int level, int count)
        {
            PictureType pictureType = PictureType.FirstLevelMarketing;
            switch (level)
            {
                case 1:
                    pictureType = PictureType.FirstLevelMarketing;
                    break;
                case 2:
                    pictureType = PictureType.SecondaryLevelMarketing;
                    break;
                case 3:
                    pictureType = PictureType.TertiaryLevelMarketing;
                    break;
            }
            var result = await ApplicationService.GetByTypeASync(pictureType, count);

            var resultVM = Mapper.Map<IEnumerable<BasePictureViewModel>>(result);

            return Ok(resultVM);
        }

        #region Admin
        [Area("Admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateTemporaryPicture()
        {
            var empty = new BasePictureViewModel();
            return await base.Post(empty);
        }

        /// <summary>
        ///     Gets the Picture with the given <param name="pictureId"></param>
        ///     and its related Tags
        /// </summary>
        /// <param name="pictureId">The Id of the Picture</param>
        /// <returns>The Picture with its related Tags</returns>
        /// <response code="200">When the entity is found by its id</response>
        /// <response code="404">When the entity couldn't be found</response>
        [HttpGet]
        public async Task<IActionResult> GetPictureWithTags(int pictureId)
        {
            if (!await this.ApplicationService.ExistsAsync(pictureId))
            {
                return NotFound(pictureId);
            }

            var result = await ApplicationService.GetPictureWithTags(pictureId);

            var resultViewModel = Mapper.Map<IndexPictureViewModel>(result);

            return Ok(resultViewModel);
        }

        [Area("Admin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddTagToPicture([FromBody, Required]PictureContentTagViewModel pictureContentTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ApplicationService.Exists(pictureContentTag.PictureId))
            {
                return NotFound("Picture with id: " + pictureContentTag.PictureId);
            }

            if (!_tagApplicationService.Exists(pictureContentTag.ContentTagId))
            {
                return NotFound("Tag with id: " + pictureContentTag.ContentTagId);
            }

            var result =
                await ApplicationService.AddPictureContentTagAsync(Mapper.Map<PictureContentTag>(pictureContentTag));

            await ApplicationService.SaveChangesAsync();

            var viewModelRes = Mapper.Map<PictureContentTagViewModel>(result);


            return CreatedAtAction("POST", viewModelRes);

        }

        [Area("Admin")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveTagToPicture(int pictureId, int tagId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ApplicationService.Exists(pictureId))
            {
                return NotFound("Picture with id: " + pictureId);
            }

            if (!_tagApplicationService.Exists(tagId))
            {
                return NotFound("Tag with id: " + tagId);
            }

            await ApplicationService.RemovePictureContentTagAsync(new PictureContentTag { PictureId = pictureId, ContentTagId = tagId });

            await ApplicationService.SaveChangesAsync();

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> UploadMarketingPicture(int marketingId, IFormFile file)
        {
            IUrlHelper urlHelper = new UrlHelper(ControllerContext);
            var domain = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";

            var result = await _imageService.UploadMarketingImageFile(file, marketingId, urlHelper, domain);


            if (result != null)
                return Ok(result);
            //return Created("The image was upload.");//TODO: get the relative path and send it back

            return new StatusCodeResult(500);
        }

        #endregion
    }
}