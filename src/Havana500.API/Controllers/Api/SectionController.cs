using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Havana500.Business.ApplicationServices.Section;
using Havana500.Domain;
using Havana500.Models.SectionViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api
{
    public class SectionController : BaseApiController<ISectionsApplicationService, Section, int, SectionBaseViewModel, SectionCreateViewModel, SectionCreateViewModel, SectionIndexViewModel>
    {
        public SectionController(ISectionsApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }
    }
}