using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.UnitOfWork;
using Havana500.Domain;

namespace Havana500.DataAccess.Repositories.Stats
{
    public class StatsRepository : IStatsRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        public int GetTotalArticles()
        {
            var query = @"SELECT COUNT(Id)
                FROM Articles";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of articles in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalNewArticles(int lastDays = 7)
        {
            var query = $@"SELECT COUNT(Id)
FROM Articles AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of comments in the system.
        /// </summary>
        /// <returns></returns>
        public int GetTotalComments()
        {
            var query = @"SELECT COUNT(Id)
                FROM Comments";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of new comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Comments</param>
        /// <returns></returns>
        public int GetTotalNewComments(int lastDays = 7)
        {
            var query = $@"SELECT COUNT(Id)
FROM Comments AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        public int GetTotalApprovedComments(int lastDays = 7)
        {
            var query = $@"SELECT COUNT(Id)
FROM Comments AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays} AND IsApproved = 1";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of not approved comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the comments</param>
        /// <returns></returns>
        public int GetTotalNotApprovedComments(int lastDays = 7)
        {
            var query = $@"SELECT COUNT(Id)
FROM Comments AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays} AND IsApproved = 0";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of media files in the system.
        /// </summary>
        /// <returns></returns>
        public int GetTotalMediaFiles()
        {
            var query = @"SELECT COUNT(Id)
                FROM Pictures";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of new media files in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalNewMediaFiles(int lastDays = 7)
        {
            var query = $@"SELECT COUNT(Id)
FROM Pictures AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of article's visits in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public int GetTotalArticleVisits(int lastDays = 7)
        {
            var query = $@"SELECT SUM(Views)
FROM Articles AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the total amount of users in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        public int GetTotalUsers(int lastDays = 7)
        {
            var query = $@"SELECT COUNT(Id)
                    FROM AspNetUsers AS A
                    WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}";

            var result = _unitOfWork.QueryFirst<int>(query);

            return result;
        }

        /// <summary>
        ///     Returns the Article with more views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        public Article GetArticleWithMoreViews(int lastDays = 7)
        {
            var query = $@"SELECT TOP 1 * 
FROM Articles AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}
ORDER BY A.Views DESC";

            var result = _unitOfWork.QueryFirst<Article>(query);

            return result;
        }

        /// <summary>
        ///     Returns with Article with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public Article GetArticleWithMoreComments(int lastDays = 7)
        {
            var query = $@"SELECT TOP 1 * 
FROM Articles AS A
WHERE DATEDIFF(DAY, A.CreatedAt, GETDATE())<{lastDays}
ORDER BY A.AmountOfComments DESC";

            var result = _unitOfWork.QueryFirst<Article>(query);

            return result;
        }

        /// <summary>
        ///     Returns the Section with more comments in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate</param>
        /// <returns></returns>
        public Section GetSectionNameWithMoreComments(int lastDays = 7)
        {
            var query = $@"SELECT TOP 1 S.* 
FROM Sections AS S
WHERE DATEDIFF(DAY, S.CreatedAt, GETDATE())<{lastDays}
ORDER BY S.AmountOfComments DESC";

            var result = _unitOfWork.QueryFirst<Section>(query);

            return result;
        }

        /// <summary>
        ///     Returns the Section with more Views in the system.
        /// </summary>
        /// <param name="lastDays">The amount of days to calculate the new Articles</param>
        /// <returns></returns>
        public Section GetSectionNameWithMoreViews(int lastDays = 7)
        {
            var query = $@"SELECT TOP 1 S.* 
FROM Sections AS S
WHERE DATEDIFF(DAY, S.CreatedAt, GETDATE())<{lastDays}
ORDER BY S.Views DESC";

            var result = _unitOfWork.QueryFirst<Section>(query);

            return result;
        }
    }
}
