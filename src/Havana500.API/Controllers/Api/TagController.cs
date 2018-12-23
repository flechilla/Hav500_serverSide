using System.Threading;
using AutoMapper;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Domain;
using Havana500.Models.TagViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Havana500.Controllers.Api
{
    public class TagController : BaseApiController<ITagApplicationService, ContentTag, int, TagBaseViewModel, TagBaseViewModel, TagBaseViewModel, TagIndexViewModel>
    {
        public TagController(ITagApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }

        public override IActionResult GetAll()
        {
            var lang = Thread.CurrentThread.CurrentCulture.Name;
            var result = this.ApplicationService.
                ReadAll(tag => tag.LanguageCulture == lang) 
                            .Select(t => new TagBaseViewModel()
                            {
                                Name = t.Name,
                                Id = t.Id
                            }).ToArray();

            return Ok(result);
        }
    }
}