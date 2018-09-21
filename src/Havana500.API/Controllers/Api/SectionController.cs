using AutoMapper;
using Havana500.Business.ApplicationServices.Section;
using Havana500.Domain;
using Havana500.Models.SectionViewModel;

namespace Havana500.Controllers.Api
{
    public class SectionController : BaseApiController<ISectionsApplicationService, Section, int, SectionBaseViewModel, SectionCreateViewModel, SectionCreateViewModel, SectionIndexViewModel>
    {
        public SectionController(ISectionsApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }
    }
}