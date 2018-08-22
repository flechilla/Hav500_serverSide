﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Havana500.Domain;
using Havana500.Test.Common.Services;
using Havana500.DataAccess.UnitOfWork;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Articles;
using Havana500.DataAccess.Contexts;

namespace Havana500.DataAccess.Tests
{
    public class ArticleRepositoryTests
    {
        [Fact]
        public void IncrementViews()
        {
            var section = new Section()
            {
                IsMainSection = true,
                Name = "Test Section"
            };

            var article = new Article()
            {
                Title = "Test article",
                Section = section
            };
            var contextResolver = new DbContextResolver();
            try
            {
                var context = contextResolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;
                var unitOfWork = new SqlUnitOfWork(context);

                var articlesRepository = unitOfWork.GetRepositoryForType(typeof(Article)) as ArticlesRepository;

                Assert.True(articlesRepository.GetType() == typeof(ArticlesRepository));

                articlesRepository.Add(article);

                unitOfWork.SaveChanges();

                articlesRepository.AddView(article.Id);

                unitOfWork.Dispose();

                context = contextResolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;
                unitOfWork = new SqlUnitOfWork(context);

                articlesRepository = unitOfWork.GetRepositoryForType(typeof(Article)) as ArticlesRepository;

                var dbArticle = articlesRepository.SingleOrDefault(article.Id);

                Assert.Equal<int>(1, dbArticle.Views);
            }
            finally
            {
                contextResolver.Dispose();
            }
        }

        [Fact]
        public async void IncrementViewsAsync()
        {
            var section = new Section()
            {
                IsMainSection = true,
                Name = "Test Section"
            };

            var article = new Article()
            {
                Title = "Test article",
                Section = section
            };
            var contextResolver = new DbContextResolver();
            try
            {
                var context = contextResolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;
                var unitOfWork = new SqlUnitOfWork(context);

                var articlesRepository = unitOfWork.GetRepositoryForType(typeof(Article)) as ArticlesRepository;

                Assert.True(articlesRepository.GetType() == typeof(ArticlesRepository));

                articlesRepository.Add(article);

                unitOfWork.SaveChanges();

                await articlesRepository.AddViewAsync(article.Id);

                unitOfWork.Dispose();

                context = contextResolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as Havana500DbContext;
                unitOfWork = new SqlUnitOfWork(context);

                articlesRepository = unitOfWork.GetRepositoryForType(typeof(Article)) as ArticlesRepository;

                var dbArticle = articlesRepository.SingleOrDefault(article.Id);

                Assert.Equal<int>(1, dbArticle.Views);
            }
            finally
            {
                contextResolver.Dispose();
            }
        }
    }
}
