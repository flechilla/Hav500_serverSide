using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.Business.ApplicationServices.Comments;
using Havana500.Business.ApplicationServices.MarketingContent;
using Havana500.Business.ApplicationServices.Section;
using Havana500.Business.ApplicationServices.Stats;
using Havana500.Business.ApplicationServices.Tag;
using Havana500.Business.ApplicationServices.Pictures;

namespace Havana500.Business.Extensions
{
    /// <summary>
    ///     Contains the functionalities to add the services 
    ///     that are implemented in the DataAcces layer.
    /// </summary>
    /// <remarks>
    ///     When a service for the DI will be used, this won't
    ///     be necessary.
    /// </remarks>
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessServices(this IServiceCollection service)
        {
            service.AddScoped<ICommentsApplicationService, CommentsApplicationService>();
            service.AddScoped<IArticlesApplicationService, ArticlesApplicationService>();
            service.AddScoped<IStatsApplicationService, StatsApplicationService>();
            service.AddScoped<ISectionsApplicationService, SectionsApplicationService>();
            service.AddScoped<ITagApplicationService, TagApplicationService>();
            service.AddScoped<IPicturesApplicationService, PicturesApplicationService>();
            service.AddScoped<IMarketingContentApplicationService, MarketingContentApplicationService>();

        }
    }
}
