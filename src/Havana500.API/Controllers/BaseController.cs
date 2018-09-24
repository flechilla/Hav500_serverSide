using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        ///     Gets or sets the Id of the current user.
        /// </summary>
        public string CurrentUserId { get; set; }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            CurrentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}