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

namespace Havana500.Business.Tests.ApplicationServiceTests
{
    public class PicturesAppServiceTests : IBaseApplicationServiceTest<IPicturesApplicationService, Picture, int>
    {

        public IPicturesApplicationService GetInstance(Havana500DbContext context)
        {
            return new PicturesApplicationService(new PicturesRepository(context));
        }

        [Fact]
        public void GetAllByTypeTests()
        {
            var pictures = new List<Picture>(20);
            var resolver = new DbContextResolver();

            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.ArticleMainPicture
                };

                pictures.Add(picture);
            }
            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.Gallery
                };

                pictures.Add(picture);
            }

            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var appService = GetInstance(context);

                appService.AddRange(pictures);

                appService.SaveChanges();

                var dbPictures = appService.GetAllByType(PictureType.Gallery);

                Assert.Equal(10, dbPictures.Count());
            }
            finally
            {
                resolver.Dispose();
            }
        }

        [Fact]
        public async void GetAllByTypeAsyncTests()
        {
            var pictures = new List<Picture>(20);
            var resolver = new DbContextResolver();

            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.ArticleMainPicture
                };

                pictures.Add(picture);
            }
            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.Gallery
                };

                pictures.Add(picture);
            }

            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var appService = GetInstance(context);

                appService.AddRange(pictures);

                appService.SaveChanges();

                var dbPictures = await appService.GetAllByTypeAsync(PictureType.Gallery);

                Assert.Equal(10, dbPictures.Count());
            }
            finally
            {
                resolver.Dispose();
            }
        }

        [Fact]
        public void GetAllByTypeWithDataTests()
        {
            var pictures = new List<Picture>(20);
            var resolver = new DbContextResolver();

            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.ArticleMainPicture
                };

                pictures.Add(picture);
            }
            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.Gallery,
                    MediaStorage = new MediaStorage()
                    {
                        Data = new byte[100]
                    }
                };

                pictures.Add(picture);
            }

            try
            {
                var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                var appService = GetInstance(context);

                appService.AddRange(pictures);

                appService.SaveChanges();

                context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                appService = GetInstance(context);

                var dbPictures = appService.GetAllByTypeWithData(PictureType.Gallery);

                Assert.Equal(10, dbPictures.Count());
                foreach (var item in dbPictures)
                {
                    Assert.NotNull(item.MediaStorage);
                }
            }
            finally
            {
                resolver.Dispose();
            }
        }

        [Fact]
        public async void GetAllByTypeWithDataAsyncTests()
        {
            var pictures = new List<Picture>(20);
            var resolver = new DbContextResolver();

            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.ArticleMainPicture
                };

                pictures.Add(picture);
            }
            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.Gallery,
                    MediaStorage = new MediaStorage()
                    {
                        Data = new byte[100]
                    }
                };

                pictures.Add(picture);
            }

            try
            {
                var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                var appService = GetInstance(context);

                appService.AddRange(pictures);

                appService.SaveChanges();

                context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;

                appService = GetInstance(context);

                var dbPictures = await appService.GetAllByTypeWithDataAsync(PictureType.Gallery);

                Assert.Equal(10, dbPictures.Count());
                foreach (var item in dbPictures)
                {
                    Assert.NotNull(item.MediaStorage);
                }
            }
            finally
            {
                resolver.Dispose();
            }
        }

        [Fact]
        public async void GetByTypeAsyncTests()
        {
            var pictures = new List<Picture>(20);
            var resolver = new DbContextResolver();

            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.ArticleMainPicture
                };

                pictures.Add(picture);
            }
            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.Gallery
                };

                pictures.Add(picture);
            }

            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var appService = GetInstance(context);

                appService.AddRange(pictures);

                appService.SaveChanges();

                var dbPictures = await appService.GetByTypeASync(PictureType.Gallery, 5);

                Assert.Equal(5, dbPictures.Count());
            }
            finally
            {
                resolver.Dispose();
            }
        }

        [Fact]
        public async void GetByTypeTests()
        {
            var pictures = new List<Picture>(20);
            var resolver = new DbContextResolver();

            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.ArticleMainPicture
                };

                pictures.Add(picture);
            }
            for (int i = 0; i < 10; i++)
            {
                var picture = new Picture()
                {
                    PictureType = PictureType.Gallery
                };

                pictures.Add(picture);
            }

            try
            {
                var context = resolver.SetContext() as Havana500DbContext;

                var appService = GetInstance(context);

                appService.AddRange(pictures);

                appService.SaveChanges();

                var dbPictures = appService.GetByType(PictureType.Gallery, 5);

                Assert.Equal(5, dbPictures.Count());
            }
            finally
            {
                resolver.Dispose();
            }
        }
    }
}
