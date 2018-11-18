using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Havana500.API.Models.PictureViewModels;
using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Domain.Models.Media;
using Havana500.Models.PictureViewModels;
using Havana500.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api
{
    [Produces("application/json")]
    public class GalleryController : BaseApiController<IPicturesApplicationService, Picture, int, BasePictureViewModel,
        BasePictureViewModel, BasePictureViewModel, IndexPictureViewModel
    >
    {
        private readonly ITagApplicationService _tagApplicationService;
        private readonly ImageService _imageService;

        public GalleryController(IPicturesApplicationService appService, IMapper mapper,
            ITagApplicationService tagApplicationService, ImageService imageService) : base(appService, mapper)
        {
            _tagApplicationService = tagApplicationService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryImages(int count)
        {
            PictureType pictureType = PictureType.Gallery;

            var result = await ApplicationService.GetByTypeASync(pictureType, count);

            var resultVM = Mapper.Map<IEnumerable<BasePictureViewModel>>(result);

            return Ok(resultVM);
        }
    }
}