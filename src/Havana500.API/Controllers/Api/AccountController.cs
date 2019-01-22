﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Havana500.Config;
using Havana500.Domain;
using Havana500.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Havana500.Models.SystemUsers;
using Havana500.Models;

namespace Havana500.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    [Area("api")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<JWTConfig> _jwtOptions;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="signinManager">Sign In Manager</param>
        /// <param name="jwtOptions">The JWT configuration options</param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, IOptions<JWTConfig> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _jwtOptions = jwtOptions;
        }

        /// <summary>
        /// Authenticates a user by providing a valid JWT
        /// </summary>
        /// <param name="loginViewModel">The login view model</param>
        /// <returns>A generated token</returns>
        [Route("authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login");
                    return BadRequest(ModelState);
                }
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Confirm your email first");
                return BadRequest(ModelState);
            }
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, isPersistent: true, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return BadRequest(ModelState);
            }

            return Ok(new { token = await GenerateToken(user), id = user.Id, username = user.UserName, email = user.Email });
        }

        
        /// <summary>
        /// Registers a user in the platform
        /// </summary>
        /// <param name="registerViewModel">The register information</param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { Email = registerViewModel.Email, UserName = registerViewModel.UserName, EmailConfirmed = true, LockoutEnabled = false };

                var creationResult = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (creationResult.Succeeded)
                {
                    // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    await _signInManager.PasswordSignInAsync(user, registerViewModel.Password, isPersistent: true, lockoutOnFailure: false);
                    return Ok(new { token = await GenerateToken(user), id = user.Id, username = user.UserName, email = user.Email });
                }
                else
                    return BadRequest(creationResult.Errors);
            }
            else return BadRequest(ModelState);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

       /// <summary>
       ///  Gets the basic information about a User.
       /// </summary>
       /// <returns>A JSON with the User's basic information.</returns>
       [HttpGet("GetUserInfo")]
       [Authorize]
       public async Task<IActionResult> GetUserInfo()
       {
           if (!User.HasClaim(c => c.Type == "email"))
               return NotFound();
           var userEmail = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
           var user = await _userManager.FindByEmailAsync(userEmail);

           if (user == null)
               return NotFound(userEmail);
           var userRoles = await  _userManager.GetRolesAsync(user);
           return Ok(new
           {
               Email = user.Email,
               UserName = user.UserName,
               Id = user.Id,
               UserRoles = userRoles
           });
       }

        /// <summary>
        ///     Sets the preferred language by the user.
        /// </summary>
        /// <param name="neutralCulture">The culture to be used</param>
        /// <remarks>The possible values are: en, es, fr.</remarks>
        /// <returns>A 200 repsonse with a cookie that contains the data about the new culture</returns>
        [HttpPost("setlanguage")]
        [Authorize]
        public IActionResult SetLanguage(string neutralCulture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(neutralCulture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return Ok($"The language has been set to {neutralCulture}");
        }

        private async Task<string> GenerateToken(ApplicationUser user, int? lastCompanyId = null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret));
            var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Id)
                };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.ValidIssuer,
                audience: _jwtOptions.Value.ValidAudience,
                claims: claims.ToArray(),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        ///     Get the elements with pagination and sorting
        /// </summary>
        /// <param name="pageNumber">The number of the current page</param>
        /// <param name="pageSize">The amount of elements per page</param>
        /// <param name="columnNameForSorting">The name of the column for sorting</param>
        /// <param name="sortingType">The type of sorting, possible values: ASC and DESC</param>
        /// <param name="columnsToReturn">The name of the columns to return</param>
        /// <param name="tableToQuery">The name of the table to query. If not present the name of the controller is taken</param>
        /// <response code="200">When the entity is found by its id</response>
        [HttpGet()]
        public virtual IActionResult GetWithPaginationAndFilter(int pageNumber, int pageSize, string columnNameForSorting, string sortingType, string columnsToReturn = "*", string tableToQuery = null)
        {

            //var tableName = string.IsNullOrEmpty(tableToQuery) ? this.ControllerContext.ActionDescriptor.ControllerName : tableToQuery;

            //var result = ApplicationService.Get(pageNumber, pageSize, columnNameForSorting, sortingType, columnsToReturn, out var length, tableName);

            //var resultViewModel = new PaginationViewModel<TIndexViewModel>
            //{
            //    Length = length,
            //    Entities = Mapper.Map<IEnumerable<TIndexViewModel>>(result)
            //};

            //return Ok(resultViewModel);

            var users = _userManager
                .Users
                .OrderBy(u => u.Id)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(u => new UserIndexViewModel()
                {
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed, 
                    FirstName = u.FirstName, 
                    LastName = u.LastName,
                    Id = u.Id, 
                    PhoneNumber = u.PhoneNumber, 
                    UserName = u.UserName,
                    Role = u.Role, 
                    UserImageHRef = u.UserImageHRef
                })
                .ToArray();

            var length = _userManager.Users.Count();

            var result = new PaginationViewModel<UserIndexViewModel>()
            {
                Length = length,
                Entities = users
            };

            return Ok(result);
        }

    }
}