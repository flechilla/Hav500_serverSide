using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Havana500.Business.ApplicationServices.Stats;
using Havana500.DataAccess.Repositories.Stats;
using Havana500.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Havana500.Controllers.Api.Admin
{
    [Produces("application/json")]
    [Route("api/Stats")]
    [Area("Admin")]
    public class StatsController : Controller
    {
        private readonly IStatsApplicationService _applicationService;

        public StatsController(IStatsApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalArticles")]
        public IActionResult GetTotalArticles()
        {
            var result = _applicationService.GetTotalArticles();

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of articles in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        [HttpGet("GetTotalNewArticles")]
        public IActionResult GetTotalNewArticles(int lastDays = 7)
        {
            var result = _applicationService.GetTotalNewArticles(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalComments")]
        public IActionResult GetTotalComments()
        {
            var result = _applicationService.GetTotalComments();

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of new comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Comments</param>
        /// <returns></returns>
        [HttpGet("GetTotalNewComments")]
        public IActionResult GetTotalNewComments(int lastDays = 7)
        {
            var result = _applicationService.GetTotalNewComments(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        [HttpGet("GetTotalApprovedComments")]
        public IActionResult GetTotalApprovedComments(int lastDays = 7)
        {
            var result = _applicationService.GetTotalApprovedComments(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of not approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        [HttpGet("GetTotalNotApprovedComments")]
        public IActionResult GetTotalNotApprovedComments(int lastDays = 7)
        {
              var result = _applicationService.GetTotalNotApprovedComments(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of media files in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalMediaFiles")]
        public IActionResult GetTotalMediaFiles()
        {
              var result = _applicationService.GetTotalMediaFiles();

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of new media files in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        [HttpGet("GetTotalNewMediaFiles")]
        public IActionResult GetTotalNewMediaFiles(int lastDays = 7)
        {
              var result = _applicationService.GetTotalNewMediaFiles(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of article's visits in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        [HttpGet("GetTotalArticleVisits")]
        public IActionResult GetTotalArticleVisits(int lastDays = 7)
        {
              var result = _applicationService.GetTotalArticleVisits(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the total amount of users in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        [HttpGet("GetTotalUsers")]
        public IActionResult GetTotalUsers(int lastDays = 7)
        {
              var result = _applicationService.GetTotalUsers(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the Article with more views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        [HttpGet("GetArticleWithMoreViews")]
        public IActionResult GetArticleWithMoreViews(int lastDays = 7)
        {
              var result = _applicationService.GetArticleWithMoreViews(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns with Article with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        [HttpGet("GetArticleWithMoreComments")]
        public IActionResult GetArticleWithMoreComments(int lastDays = 7)
        {
              var result = _applicationService.GetArticleWithMoreComments(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the Section with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        [HttpGet("GetSectionNameWithMoreComments")]
        public IActionResult GetSectionNameWithMoreComments(int lastDays = 7)
        {
              var result = _applicationService.GetSectionNameWithMoreComments(lastDays);

            return Ok(result);
        }

        /// <summary>
        ///     Returns the Section with more Views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        [HttpGet("GetSectionNameWithMoreViews")]
        public IActionResult GetSectionNameWithMoreViews(int lastDays = 7)
        {
              var result = _applicationService.GetSectionNameWithMoreViews(lastDays);

            return Ok(result);
        }
    }
}