using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Havana500.Business.Base;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using Havana500.Test.Common.Abstract;
using Havana500.Test.Common.Services;
using Xunit;
using Havana500.Business.ApplicationServices.Comments;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.Business.Tests.ApplicationServiceTests
{
    /// <summary>
    ///     This class contains the uniTests for the <see cref="BaseApplicationService{TEntity,TKey}"/>
    /// </summary>
    public class BaseApplicationServiceTests : IBaseApplicationServiceTest<CommentsApplicationService, Comment, int>
    {
        //the bottom index of the cycle that creates the objects
        private const int _cycleStart = 0;
        //the upper index of the cycle that creates the objects
        private const int _cycleEnd = 10;

        /// <summary>
        ///     Create an instance of the actual implementation of the interface
        ///      <see cref="IBaseApplicationService{TEntity,TKey}"/> for testing its operations
        /// </summary>
        /// <param name="context">The DBcontext for be used in the unit tests</param>
        /// <returns>An instance of the class <see cref="CommentsApplicationService"/></returns>
        public CommentsApplicationService GetInstance(Havana500DbContext context)
        {
            return new CommentsApplicationService(new CommentsRepository(context, new ArticlesRepository(context)));
        }



        /// <summary>
        ///     Tests the operations:
        ///     - Add(), SaveChanges() and SingleOrDefault()
        /// </summary>
        [Fact]
        public void Test_Add()
        {
            var resolver = new DbContextResolver();

            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var user = new ApplicationUser();
                var comment = new Comment() { Body = "Comment body for testing" };

                var appService = GetInstance(context);

                appService.Add(comment);
                appService.SaveChanges();

                var comment1 = appService.SingleOrDefault(t => t.Body == "Comment body for testing");

                Assert.NotNull(comment1);
            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - AddAsync(), SaveChangesAsync() and SingleOrDefaultAsync()
        /// </summary>
        [Fact]
        public async Task Test_AddAsync()
        {
            var resolver = new DbContextResolver();

            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var user = new ApplicationUser();
                var comment = new Comment() { Body = "Comment body for testing" };

                var appService = GetInstance(context);

                await appService.AddAsync(comment);
                appService.SaveChanges();

                var comment1 = appService.SingleOrDefault(t => t.Body == "Comment body for testing");

                Assert.NotNull(comment1);
            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - AddRange()
        /// </summary>
        [Fact]
        public void Test_AddRange()
        {
            var resolver = new DbContextResolver();
            try
            {
                var context = resolver.SetContext() as Havana500DbContext;
                var user = new ApplicationUser() { };
                var appService = GetInstance(context);

                var comments = new List<Comment>(10);

                for (int i = 1; i < 11; i++)
                {
                    comments.Add(new Comment() { Body = "Comment body for testing" + i });
                }

                appService.AddRange(comments);
                appService.SaveChanges();

                for (int i = 1; i < 11; i++)
                {
                    var comment = appService.SingleOrDefault(i);

                    Assert.NotNull(comment);
                    Assert.Equal(comment.Body, $"Comment body for testing{i}");
                }
            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - AddRangeAsync(), - Exist(Tkey id)
        /// </summary>
        [Fact]
        public async Task Test_AddRangeAsync_ExistId()
        {
            var resolver = new DbContextResolver();
            try
            {
                var context = resolver.SetContext() as Havana500DbContext;
                var user = new ApplicationUser() { };
                var appService = GetInstance(context);

                var comments = new List<Comment>(10);

                for (int i = 1; i < 11; i++)
                {
                    comments.Add(new Comment() { Body = "Comment body for testing" + i });
                }

                await appService.AddRangeAsync(comments);
                appService.SaveChanges();

                for (int i = 1; i < 11; i++)
                {
                    var comment = appService.SingleOrDefault(i);

                    Assert.NotNull(comment);
                    Assert.Equal(comment.Body, $"Comment body for testing{i}");
                }
            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Exist(TEntity obj)
        /// </summary>
        //[Fact]
        public async Task Test_ExistObj()//internal bug in the EFCore
        {
            var resolver = new DbContextResolver();
            try
            {
                var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                var user = new ApplicationUser() { };

                var appService = GetInstance(context);

                var comments = new List<Comment>(10);

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    comments.Add(new Comment() { Body = "Comment body for testing" + i });
                }

                await appService.AddRangeAsync(comments);
                appService.SaveChanges();

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    var commentExist = appService.Exists(comments[i]);

                    Assert.True(commentExist, $"There is an object with Id == Comment{i} and Name == Comment{i}" + i);
                }

                var notRealCommentExist = appService.Exists(new Comment() { Body = "Not in Db comment" });

                Assert.False(notRealCommentExist, "The tested object doesn't exist");

            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Exist(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ExistFunc()
        {
            var resolver = new DbContextResolver();
            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var user = new ApplicationUser() { };

                var appService = GetInstance(context);

                var comments = new List<Comment>(10);

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    comments.Add(new Comment() { Body = "Comment body for testing" + i });
                }

                await appService.AddRangeAsync(comments);
                appService.SaveChanges();

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    var commentExist = appService.Exists(c => c.Body == comments[i].Body);

                    Assert.True(commentExist);
                }

                var notRealCommentExist = appService.Exists(c=> c == new Comment() { Body = "Not in Db comment" });

                Assert.False(notRealCommentExist, "The tested object doesn't exist");
            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ExistAsync(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ExistAsyncFunc()
        {
            var resolver = new DbContextResolver();
            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var user = new ApplicationUser() { };

                var appService = GetInstance(context);

                var comments = new List<Comment>(10);

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    comments.Add(new Comment() { Body = "Comment body for testing" + i });
                }

                await appService.AddRangeAsync(comments);
                appService.SaveChanges();

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    var commentExist = await appService.ExistsAsync(c => c.Body == comments[i].Body);

                    Assert.True(commentExist);
                }

                var notRealCommentExist = await appService.ExistsAsync(c => c == new Comment() { Body = "Not in Db comment" });

                Assert.False(notRealCommentExist, "The tested object doesn't exist");
            }
            finally
            {
                resolver.Dispose();
            }
        }

        ///// <summary>
        /////     Tests the operations:
        /////     - ExistAsync(TEntity obj)
        ///// </summary>
        ////[Fact]
        //public async Task Test_ExistAsyncObj()//internal bug in EfCore
        //{
        //    var resolver = new DbContextResolver();
        //    var context = resolver.SetContext() as Havana500DbContext;

               //    var appService = GetInstance(context);

        //    var Comments = new List<Comment>(10);

        //    for (int i = _cycleStart; i < _cycleEnd; i++)
        //    {
        //        Comments.Add(new Comment {Body = "Comment Body" + i});
        //    }

        //    await appService.AddRangeAsync(Comments);
        //    appService.SaveChanges();

        //    for (int i = _cycleStart; i < _cycleEnd; i++)
        //    {
        //        var obj = appService.SingleOrDefault(x => x.Body == "Comment" + i);
        //        var CommentExist = await appService.ExistsAsync(obj);

        //        Assert.True(CommentExist, $"There is an object with Body == Comment{i} " + i);
        //    }

        //    var notRealCommentExist = appService.Exists(new Comment { Id = 1, Body = "NotInDB",Body = "Comment Body", ApplicationUser = user, CommentHole = CommentHole});

        //    Assert.False(notRealCommentExist, "There is no object with id == 1 and Body = NotInDB");

        //    resolver.Dispose();
        //}

        /// <summary>
        ///     Tests the operations:
        ///     - ExistAsync(Tkey id)
        /// </summary>
        [Fact]
        public async Task Test_ExistAsyncId()
        {
            var resolver = new DbContextResolver();
            try
            {
                var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                var user = new ApplicationUser() { };

                var appService = GetInstance(context);

                var comments = new List<Comment>(10);

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    comments.Add(new Comment() { Body = "Comment body for testing" + i });
                }

                await appService.AddRangeAsync(comments);
                appService.SaveChanges();

                for (int i = _cycleStart; i < _cycleEnd; i++)
                {
                    var commentExist = await appService.ExistsAsync(i);

                    Assert.True(commentExist);
                }

                var notRealCommentExist = await appService.ExistsAsync(20);

                Assert.False(notRealCommentExist, "The tested object doesn't exist");
            }
            finally
            {
                resolver.Dispose();
            }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ReadAll(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ReadAllFilter()
        {
            var resolver = new DbContextResolver();
            try{
            var context = resolver.SetContext() as Havana500DbContext;


            var appService = GetInstance(context);

            var comments = new List<Comment>(10);

            for (int i = _cycleStart; i < _cycleEnd; i++)
            {
                comments.Add(new Comment(){Body = "Body for testing" + i});
            }

            await appService.AddRangeAsync(comments);
            appService.SaveChanges();

            var returnedElements = appService.ReadAll(comment => comment.Body.Contains("Body"));

            Assert.NotEmpty(returnedElements);

            var returnedElementsList = new List<Comment>(returnedElements);
            Assert.Equal(comments, returnedElements);
            }
            finally{
            resolver.Dispose();}
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ReadAllAsync(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ReadAllAsyncFilter()
        {
                var resolver = new DbContextResolver();
            try{
            var context = resolver.SetContext() as Havana500DbContext;


            var appService = GetInstance(context);

            var comments = new List<Comment>(10);

            for (int i = _cycleStart; i < _cycleEnd; i++)
            {
                comments.Add(new Comment(){Body = "Body for testing" + i});
            }

            await appService.AddRangeAsync(comments);
            appService.SaveChanges();

            var returnedElements = await appService.ReadAllAsync(comment => comment.Body.Contains("Body"));

            Assert.NotEmpty(returnedElements);

            var returnedElementsList = new List<Comment>(returnedElements);
            Assert.Equal(comments, returnedElements);
            }
            finally{
            resolver.Dispose();}
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Remove(TKey Id)
        /// </summary>
        [Fact]
        public async Task Test_RemoveID()
        {
           var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment { Body = "Body for testing" + i });
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var idToRemove = 1;

           appService.Remove(idToRemove);
           appService.SaveChanges();

           var removedElementExist = appService.Exists(idToRemove);

           Assert.False(removedElementExist, $"The element with Id '{idToRemove}' was removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Remove(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_RemoveObj()
        {
           var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment { Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var objToRemove = Comments[0];

           appService.Remove(objToRemove);
           appService.SaveChanges();

           var removedElementExist = appService.Exists(x => x.Id == objToRemove.Id);

           Assert.False(removedElementExist, $"The element with Id == '{objToRemove.Id}' was removed from DB");
           }
           finally{
                resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Remove(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_RemoveFilter()
        {
           var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var filterToRemove = new Func<Comment, bool>(comment => comment.Body.Contains("Comment")
           && comment.Id < 5);

           appService.Remove(filterToRemove);
           appService.SaveChanges();

           var removedElementsExist = appService.Exists(filterToRemove);

           Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveAsync(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_RemoveAsyncFilter()
        {
              var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var filterToRemove = new Func<Comment, bool>(comment => comment.Body.Contains("Comment")
           && comment.Id < 5);

           await appService.RemoveAsync(filterToRemove);
           appService.SaveChanges();

           var removedElementsExist = appService.Exists(filterToRemove);

           Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveAsync(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_RemoveAsyncObj()
        {
              var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var objToRemove = Comments[0];

           await appService.RemoveAsync(objToRemove);
           appService.SaveChanges();

           var removedElementExist = appService.Exists(x => x.Id == objToRemove.Id);

           Assert.False(removedElementExist, $"The element with Id == '{objToRemove.Id}' was removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveAsync(TKey Id)
        /// </summary>
        [Fact]
        public async Task Test_RemoveAsyncID()
        {
              var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var idToRemove = Comments[0].Id;

           await appService.RemoveAsync(idToRemove);
           appService.SaveChanges();

           var removedElementExist = appService.Exists(x => x.Id == idToRemove);

           Assert.False(removedElementExist, $"The element with Id == '{idToRemove}' was removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveRange(IEnumerable)
        /// </summary>
        [Fact]
        public async Task Test_RemoveRange()
        {
           var resolver = new DbContextResolver();

           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment { Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var filterToRemove = new Func<Comment, bool>(Comment => Comment.Body.Contains("Comment")
           && Comment.Id < 5);

           var elementsToRemove = Comments.GetRange(0, 5);

           appService.RemoveRange(elementsToRemove);
           appService.SaveChanges();

           var removedElementsExist = appService.Exists(filterToRemove);

           Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveRangeAsync(IEnumerable)
        /// </summary>
        [Fact]
        public async Task Test_RemoveRangeAsync()
        {
     var resolver = new DbContextResolver();

           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment { Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var filterToRemove = new Func<Comment, bool>(Comment => Comment.Body.Contains("Comment")
           && Comment.Id < 5);

           var elementsToRemove = Comments.GetRange(0, 5);

           await appService.RemoveRangeAsync(elementsToRemove);
           appService.SaveChanges();

           var removedElementsExist = appService.Exists(filterToRemove);

           Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - SingleOrDefault(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_SingleOrDefaultFilter()
        {
           var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i });
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var filterToThrowException = new Func<Comment, bool>(Comment => Comment.Body.Contains("Comment")
           && Comment.Id < 5);

           var filterToGetSingleELement = new Func<Comment, bool>(Comment => Comment.Body.Contains("Comment")
           && Comment.Id == 5);

           var filterToGetDefualtELement = new Func<Comment, bool>(Comment => Comment.Body.Contains("Comment")
           && Comment.Id == 20);

           Assert.Throws<InvalidOperationException>(() => { appService.SingleOrDefault(filterToThrowException); });

           var returnedComment = appService.SingleOrDefault(filterToGetSingleELement);
           Assert.NotNull(returnedComment);

           var nullReturnedComment = appService.SingleOrDefault(filterToGetDefualtELement);
           Assert.Null(nullReturnedComment);
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Update(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_Update()
        {
           var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {  Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var CommentToUpdate = Comments[0];

           CommentToUpdate.Body = "UpdatedName";

           appService.Update(CommentToUpdate);
           appService.SaveChanges();

           var returnedElement = appService.SingleOrDefault(CommentToUpdate.Id);

           Assert.Equal(CommentToUpdate, returnedElement);
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - UpdateAsync(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_UpdateAsync()
        {
          var resolver = new DbContextResolver();
           try{
           var context = resolver.SetContext() as Havana500DbContext;

           var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {  Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           var CommentToUpdate = Comments[0];

           CommentToUpdate.Body = "UpdatedName";

           await appService.UpdateAsync(CommentToUpdate);
           appService.SaveChanges();

           var returnedElement = appService.SingleOrDefault(CommentToUpdate.Id);

           Assert.Equal(CommentToUpdate, returnedElement);
           }
           finally{
           resolver.Dispose();
           }
        }

        /// <summary>
        ///     Tests the operations:
        ///     - UpdateRange(IEnumerable{TEntity} objs)
        /// </summary>
        [Fact]
        public async Task Test_UpdateRange()
        {
           var resolver = new DbContextResolver();
           var context = resolver.SetContext() as Havana500DbContext;

                  var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments[i-1].Body = $"UpdatedName{i}";
           }

           appService.UpdateRange(Comments);
           appService.SaveChanges();

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               var existUpdatedElement = appService.Exists(x=>x.Body == $"UpdatedName{i}");
               Assert.True(existUpdatedElement);
           }

           resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - UpdateRangeAsync(IEnumerable{TEntity} objs)
        /// </summary>
        [Fact]
        public async Task Test_UpdateRangeAsync()
        {
           var resolver = new DbContextResolver();
           var context = resolver.SetContext() as Havana500DbContext;

                  var appService = GetInstance(context);

           var Comments = new List<Comment>(10);

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments.Add(new Comment {Body = "Comment Body" + i});
           }

           await appService.AddRangeAsync(Comments);
           appService.SaveChanges();

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               Comments[i-1].Body = $"UpdatedName{i}";
           }

           await appService.UpdateRangeAsync(Comments);
           appService.SaveChanges();

           for (int i = _cycleStart; i < _cycleEnd; i++)
           {
               var existUpdatedElement = appService.Exists(x => x.Body == $"UpdatedName{i}");
               Assert.True(existUpdatedElement);
           }

           resolver.Dispose();
        }
    }
}
