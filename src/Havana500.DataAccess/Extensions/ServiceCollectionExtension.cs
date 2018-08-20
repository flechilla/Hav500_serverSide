﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Havana500.DataAccess.Contexts;
using Havana500.DataAccess.Repositories.Base;
using Havana500.DataAccess.Repositories.Comments;
using Havana500.Domain;

namespace Havana500.DataAccess.Extensions
{
    /// <summary>
    ///     Contians the functionalities to add the services 
    ///     that are implemented in the DataAcces layer.
    /// </summary>
    /// <remarks>
    ///     When a service for the DI will be used, this won't
    ///     be neccessary.
    /// </remarks>
    public static class ServiceCollectionExtension
    {
        public static void AddDataAccessServices(this IServiceCollection service)
        {
            service.AddScoped<ICommentsRepository, CommentsRepository>();

            service.AddScoped<ISqlDbContext, Havana500DbContext>();
        }
    }
}
