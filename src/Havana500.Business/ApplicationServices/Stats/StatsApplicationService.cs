using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Repositories.Stats;
using Havana500.Domain;

namespace Havana500.Business.ApplicationServices.Stats
{
    public class StatsApplicationService : IStatsApplicationService
    {
        public IStatsRepository Repository { get; }

        public StatsApplicationService(IStatsRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        public int GetTotalArticles()
        {
            return Repository.GetTotalArticles();
        }

        /// <summary>
        ///     Returns the total amount of articles in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalNewArticles(int lastDays = 7)
        {
            return Repository.GetTotalNewArticles();
        }

        /// <summary>
        ///     Returns the total amount of articles in the system
        ///     that are active. this is that the date for starting
        ///     the publication is equal or bigger than Today.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalActiveArticles(int lastDays = 7){
            return Repository.GetTotalActiveArticles(lastDays);
        }

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        public int GetTotalComments()
        {
            return Repository.GetTotalComments();
        }

        /// <summary>
        ///     Returns the total amount of new comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Comments</param>
        /// <returns></returns>
        public int GetTotalNewComments(int lastDays = 7)
        {
            return Repository.GetTotalNewComments(lastDays);
        }

        /// <summary>
        ///     Returns the total amount of approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        public int GetTotalApprovedComments(int lastDays = 7)
        {
            return Repository.GetTotalApprovedComments(lastDays);
        }

        /// <summary>
        ///     Returns the total amount of not approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        public int GetTotalNotApprovedComments(int lastDays = 7)
        {
            return Repository.GetTotalNotApprovedComments(lastDays);
        }

        /// <summary>
        ///     Returns the total amount of media files in the system.
        /// </summary>
        /// <returns></returns>
        public int GetTotalMediaFiles()
        {
            return Repository.GetTotalMediaFiles();
        }

        /// <summary>
        ///     Returns the total amount of new media files in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalNewMediaFiles(int lastDays = 7)
        {
            return Repository.GetTotalNewMediaFiles(lastDays);
        }

        /// <summary>
        ///     Returns the total amount of article's visits in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalArticleVisits(int lastDays = 7)
        {
            return Repository.GetTotalArticleVisits(lastDays);
        }

        /// <summary>
        ///     Returns the total amount of users in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        public int GetTotalUsers(int lastDays = 7)
        {
            return Repository.GetTotalUsers(lastDays);
        }

        /// <summary>
        ///     Returns the Article with more views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        public Article GetArticleWithMoreViews(int lastDays = 7)
        {
            return Repository.GetArticleWithMoreViews(lastDays);
        }

        /// <summary>
        ///     Returns with Article with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public Article GetArticleWithMoreComments(int lastDays = 7)
        {
            return Repository.GetArticleWithMoreComments(lastDays);
        }

        /// <summary>
        ///     Returns the Section with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        public Domain.Section GetSectionNameWithMoreComments(int lastDays = 7)
        {
            return Repository.GetSectionNameWithMoreComments(lastDays);
        }

        /// <summary>
        ///     Returns the Section with more Views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public Domain.Section GetSectionNameWithMoreViews(int lastDays = 7)
        {
            return Repository.GetSectionNameWithMoreViews(lastDays);
        }
    }
}
