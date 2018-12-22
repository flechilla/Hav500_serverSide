using System;
using System.Globalization;
using Havana500.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.API.Controllers.Api
{
    [Route("api/v1/[controller]/[action]")]
    public class CookiesController : Controller
    {
    private const string _cookiesAcceptedKey = "CookiesAccepted";

        [HttpPost]
        public IActionResult AcceptCookies() 
        {
            Response.Cookies.Append(_cookiesAcceptedKey, true.ToString());
            return Ok();
        }

        [HttpPost]
        public IActionResult SetLanguage([FromBody]LangViewModel lang)
        {
            Console.WriteLine($"Lang param: {lang}");
          Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang.Lang)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

                return Ok();
        }
    }
}