using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Domain;
using Havana500.Models.ArticleViewModels;
using AutoMapper;

namespace Havana500.Controllers.Api
{
    public class ArticleController : BaseApiController<
        IArticlesApplicationService,
        Article,
        int,
        ArticleBaseViewModel,
        ArticleCreateViewModel,
        ArticleCreateViewModel,
        ArticleIndexViewModel>
    {
        public ArticleController(IArticlesApplicationService appService, IMapper mapper) : base(appService, mapper)
        {
        }
    }
}