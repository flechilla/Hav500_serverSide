using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.API.Controllers.Api
{

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
        public IActionResult SetLanguage(string lang)
        {
            
          Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

                return Ok();
        }
    }
}