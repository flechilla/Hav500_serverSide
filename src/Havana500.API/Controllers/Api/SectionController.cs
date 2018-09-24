using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Havana500.Business.ApplicationServices.Section;
using Havana500.Domain;
using Havana500.Models.SectionViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api
{
    public class SectionController : BaseApiController<ISectionsApplicationService, Section, int, SectionBaseViewModel, SectionCreateViewModel, SectionCreateViewModel, SectionIndexViewModel>
    {
        public SectionController(ISectionsApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }

         #region ADMIN AREA
        [Area("Admin")]
        public override async Task<IActionResult> Post([FromBody, Required]SectionCreateViewModel newSection){
            return await base.Post(newSection);
        }

        [Area("Admin")]
        public override async Task<IActionResult> Put(int sectionId, [FromBody, Required]SectionCreateViewModel newSection){
            return await base.Put(sectionId, newSection);
        }

        [Area("Admin")]
        public override async Task<IActionResult> Delete(int sectionId){
            return await base.Delete(sectionId);
        }
        #endregion
    }
}