using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;

namespace Havana500.DataAccess.Jobs
{
    /// <summary>
    ///     Contains the methods that are used to run backgraound Jobs
    ///     for the <see cref="Article"/> entity.
    /// </summary>
    public static class ArticleBackgroundJobs
    {
        public static async Task UpdateWeightColumn(Havana500DbContext context)
        {
            var unitOfWork = new UnitOfWork.SqlUnitOfWork(context);

            var query = @"
USE Havana500;
GO

EXEC usp_updateArticlesWeight;               
            ";
            int result = -1;
            try
            {
                await unitOfWork.QueryFirstAsync<int>(query);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }
    }
}
