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
        private const string _cookiesIsLegalAgeKey = "IsLegalAge";
        private const string _cookiesAcceptedKey = "CookiesAccepted";

        [HttpGet]
        public IActionResult AcceptCookies()
        {
            Response.Cookies.Append(_cookiesAcceptedKey, true.ToString(),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                });
            return Ok();
        }

        [HttpPost]
        public IActionResult SetAge([FromBody]AgeViewModel age)
        {
            Response.Cookies.Append(
                  _cookiesIsLegalAgeKey,
                  age.Age.ToString(),
                  new CookieOptions
                  {
                      Expires = DateTimeOffset.UtcNow.AddYears(1),
                      IsEssential = true,
                  }
                  );
            return Ok();
        }

        [HttpPost]
        public IActionResult SetLanguage([FromBody]LangViewModel lang)
        {
            Response.Cookies.Append(
                  CookieRequestCultureProvider.DefaultCookieName,
                  CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang.Lang)),
                  new CookieOptions
                  {
                      Expires = DateTimeOffset.UtcNow.AddYears(1),
                      IsEssential = true,
                  }
                  );
            return Ok();
        }
    }
}