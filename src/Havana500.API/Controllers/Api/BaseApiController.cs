using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class BaseApiController : Controller
    {

        public string CurrentUserId { get; set; }
        public string CurrentUserNickName { get; set; }

        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            CurrentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            CurrentUserNickName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }

    }
}