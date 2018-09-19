using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain;

namespace Havana500.DataAccess.Repositories.Stats
{
    /// <summary>
    ///     Declares the methods that are used to work with the Stats.
    /// </summary>
    public interface IStatsRepository
    {
        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        int GetTotalArticles();

        /// <summary>
        ///     Returns the total amount of articles in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        int GetTotalNewArticles(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of articles in the system
        ///     that are active. this is that the date for starting
        ///     the publication is equal or bigger than Today.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        int GetTotalActiveArticles(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        int GetTotalComments();

        /// <summary>
        ///     Returns the total amount of new comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Comments</param>
        /// <returns></returns>
        int GetTotalNewComments(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        int GetTotalApprovedComments(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of not approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        int GetTotalNotApprovedComments(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of media files in the system.
        /// </summary>
        /// <returns></returns>
        int GetTotalMediaFiles();

        /// <summary>
        ///     Returns the total amount of new media files in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        int GetTotalNewMediaFiles(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of article's visits in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        int GetTotalArticleVisits(int lastDays = 7);

        /// <summary>
        ///     Returns the total amount of users in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        int GetTotalUsers(int lastDays = 7);

        /// <summary>
        ///     Returns the Article with more views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        Article GetArticleWithMoreViews(int lastDays = 7);

        /// <summary>
        ///     Returns with Article with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        Article GetArticleWithMoreComments(int lastDays = 7);

        /// <summary>
        ///     Returns the Section with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        Domain.Section GetSectionNameWithMoreComments(int lastDays = 7);

        /// <summary>
        ///     Returns the Section with more Views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        Domain.Section GetSectionNameWithMoreViews(int lastDays = 7);

                 /// <summary>
        ///     Returns a list of articles ordered by the amount of Views.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        IEnumerable<Article> GetTrendingArticles(int lastDays = 7);
    }
}
