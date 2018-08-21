using System;
using Xunit;
using Havana500.DataAccess.UnitOfWork;
using Havana500.Domain;
using Microsoft.EntityFrameworkCore;
using Havana500.Test.Common.Services;
using Havana500.DataAccess.Contexts;
using Havana500.DataAccess.Repositories;

namespace Havana500.DataAccess.Tests
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void GetCommentRespositoryFromUnitOfWork()
        {
            var context = (new DbContextResolver()).SetContext(DbContextResolver.DbContextProvider.SqliteInMemory) as Havana500DbContext;
            var unitOfWork = new SqlUnitOfWork(context);

            var commentRepository = unitOfWork.GetRepositoryForType(typeof(Comment)) as CommentsRepository;

            Assert.True(commentRepository.GetType() == typeof(CommentsRepository));
        }

        [Fact]
        public void AddNewComment()
        {
            var contextResolver = new DbContextResolver();
            try
            {
                var context = contextResolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;
                var unitOfWork = new SqlUnitOfWork(context);

                var commentRepository = unitOfWork.GetRepositoryForType(typeof(Comment)) as CommentsRepository;

                var comment = new Comment()
                {
                    Body = "Test Comment body",
                    ApplicationUserId = "userId"
                };

                commentRepository.Add(comment);

                unitOfWork.SaveChanges();

                var comment1 = commentRepository.SingleOrDefault((c) => c.Body.Contains("Test"));

                Assert.NotNull(comment1);
                
            }
            finally
            {
                contextResolver.Dispose();
            }
        }
    }
}
