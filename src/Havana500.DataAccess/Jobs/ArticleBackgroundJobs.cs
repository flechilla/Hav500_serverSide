using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Hangfire.Client;
using Havana500.DataAccess.Contexts;
using Havana500.DataAccess.UnitOfWork;
using Havana500.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Havana500.DataAccess.Jobs
{
    /// <summary>
    ///     Contains the methods that are used to run backgraound Jobs
    ///     for the <see cref="Article"/> entity.
    /// </summary>
    public class ArticleBackgroundJobs
    {
        private readonly IServiceContainer _container;
        private SqlUnitOfWork _unitOfWork;

        public ArticleBackgroundJobs(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as SqlUnitOfWork;
        }

        public async Task UpdateWeightColumn()
        {
         

            var query = @"
                EXEC usp_updateArticlesWeight;               
            ";
            int result = -1;
            var connection = _unitOfWork.OpenConnection(out bool closeManually);
            try
            {
                await connection.ExecuteAsync(query);
            }
            finally
            {
                if(closeManually)
                    connection.Close();
                _unitOfWork.Dispose();
            }
        }
    }
}
