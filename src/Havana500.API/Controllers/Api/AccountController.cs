using System;
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
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Havana500.Services;
using Havana500.Business.ApplicationServices.Pictures;

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
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private IPicturesApplicationService _pictureService;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="signinManager">Sign In Manager</param>
        /// <param name="jwtOptions">The JWT configuration options</param>
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            IOptions<JWTConfig> jwtOptions,
            IEmailSender emailSender,
            IMapper mapper,
            IPicturesApplicationService pictureService)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _jwtOptions = jwtOptions;
            _emailSender = emailSender;
            _mapper = mapper;
            _pictureService = pictureService;
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
                return BadRequest("The params can't be null");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(userId);
            }
            var result = user.UserName == code;

            if (result)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }

            return Ok(new {
                user = _mapper.Map<ApplicationUser, BaseUserViewMode>(user),
                result = result
            });
        }

        /// <summary>
        ///  Gets the basic information about a User.
        /// </summary>
        /// <returns>A JSON with the User's basic information.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var claims = User.Claims;
            if (!User.HasClaim(c => c.Type == "email"))
                return NotFound();
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
                return NotFound(userEmail);
            var userAvatarHRef = _pictureService.SingleOrDefaultAsync(p => p.UserId == user.Id && p.PictureType == Domain.Models.Media.PictureType.Avatar);
            return Ok(new
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Role = user.Role,
                UserImageHref = userAvatarHRef
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

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="newObject">The new entity</param>
        /// <returns>The created entity</returns>
        /// <response code="201">When the entity was successfully created</response>
        /// <response code="400">When the entity model was not in a correct state and validation failed</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> Post([FromBody, Required]BaseUserViewMode newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user;
            try
            {
                user = _mapper.Map<BaseUserViewMode, ApplicationUser>(newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            user.UserName = $"{user.FirstName}{user.LastName}_{Guid.NewGuid().ToString()}";
            user.Id = null;

            var creationResult = await _userManager.CreateAsync(user);

            if (!creationResult.Succeeded)
                return BadRequest(creationResult.Errors);

            var referer = Request.Headers.FirstOrDefault(h => h.Key == "Referer").Value[0];
            await SendEmailToUser(user, referer);

            return CreatedAtAction("Post", new { id = user.Id }, _mapper.Map<ApplicationUser, UserIndexViewModel>(user));
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="id">The id of the entity to update</param>
        /// <param name="updatedUser">The updated entity</param>
        /// <returns>The updated entity</returns>
        /// <response code="200">When the entity was successfully updated</response>
        /// <response code="400">When the entity model was not in a correct state and validation failed</response>
        /// <response code="404">When the entity to update was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(string id, [FromBody]UserUpdateViewModel updatedUser)
        {
            var originalUser = await _userManager.FindByIdAsync(id);

            if (originalUser == null)
                return NotFound(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map(updatedUser, originalUser);

            var result = await _userManager.UpdateAsync(entity);
            var addPasswordResult = await _userManager.AddPasswordAsync(originalUser, updatedUser.Password);

            if (!result.Succeeded || !addPasswordResult.Succeeded)
            {
                return BadRequest(new
                {
                    updateUserError = result.Errors,
                    addPasswordErrors = addPasswordResult.Errors
                });
            }

            return Ok(_mapper.Map<ApplicationUser, UserIndexViewModel>(entity));
        }

        #region Helpers
        private async Task SendEmailToUser(ApplicationUser user, string referer)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, referer);
             var callbackUrl = $"{referer}emailCallback?userId={user.Id}&code={user.UserName}";
            Console.WriteLine($"Generated Code: {callbackUrl}");
            var email = user.Email;
            var userFullName = $"{user.FirstName} {user.LastName}";
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl, userFullName, user.Role);
        }
        #endregion

    }
}