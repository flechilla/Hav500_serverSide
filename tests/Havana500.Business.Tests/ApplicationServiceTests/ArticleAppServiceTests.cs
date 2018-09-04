using Havana500.Business.ApplicationServices.Pictures;
using Havana500.Domain.Models.Media;
using Havana500.Test.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;
using Havana500.DataAccess.Repositories.Pictures;
using Xunit;
using Havana500.Test.Common.Services;
using System.Linq;
using Havana500.Domain;
using Havana500.Business.ApplicationServices.Articles;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.Business.Tests.ApplicationServiceTests
{
    public class ArticleAppServiceTests : IBaseApplicationServiceTest<IArticlesApplicationService, Article, int>
    {
        public IArticlesApplicationService GetInstance(Havana500DbContext context)
        {
            return new ArticlesApplicationService(new ArticlesRepository(context));
        }

        [Fact]
        public async void GetArticleCommets()
        {
            var comments = new List<Comment>();
            for (int i = 0; i < 100; i++)
            {
                var comment = new Comment()
                {
                    Body = "Test comment #" + i
                };
                comments.Add(comment);
            }

            var section = new Section()
            {
                Name = "TestSection"
            };

            var article = new Article()
            {
                Comments = comments,
                Section = section
            };

            var resolver = new DbContextResolver();

            try
            {
                var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                var appService = GetInstance(context);

                appService.Add(article);

                appService.SaveChanges();

                var dbComments = await appService.GetComments(article.Id, 1, 20);

                Assert.Equal(20, dbComments.Count());
                Assert.Equal(20, dbComments.First().Id);
            }
            finally
            {
                resolver.Dispose();
            }
        }
    }
}
