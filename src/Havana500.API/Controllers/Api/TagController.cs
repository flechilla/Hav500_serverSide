using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Domain;
using Havana500.Models.TagViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api
{
    public class TagController : BaseApiController<ITagApplicationService, ContentTag, int, TagBaseViewModel, TagBaseViewModel, TagBaseViewModel, TagBaseViewModel>
    {
        public TagController(ITagApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }
    }
}