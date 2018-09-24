using AutoMapper;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Domain;
using Havana500.Models.TagViewModels;

namespace Havana500.Controllers.Api
{
    public class TagController : BaseApiController<ITagApplicationService, ContentTag, int, TagBaseViewModel, TagBaseViewModel, TagBaseViewModel, TagIndexViewModel>
    {
        public TagController(ITagApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }
    }
}