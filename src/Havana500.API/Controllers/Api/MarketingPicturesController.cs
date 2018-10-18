using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Havana500.API.Models.PictureViewModels;
using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Domain.Models.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api
{
    [Produces("application/json")]
    public class MarketingPicturesController : BaseApiController<IPicturesApplicationService, Picture, int,
        BasePictureViewModel, BasePictureViewModel, BasePictureViewModel, BasePictureViewModel>
    {
        public MarketingPicturesController(IPicturesApplicationService appService, IMapper mapper) : base(appService,
            mapper)
        {
        }

        [HttpGet()]
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

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTemporaryArticle()
        {
            var empty = new BasePictureViewModel();
            return await base.Post(empty);
        }
    }
}