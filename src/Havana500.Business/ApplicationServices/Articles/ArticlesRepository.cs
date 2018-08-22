﻿using Havana500.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.Domain;
using System.Linq;
using System.Threading.Tasks;
using Havana500.DataAccess.Repositories;
using Havana500.DataAccess.Repositories.Articles;

namespace Havana500.Business.ApplicationServices.Articles
{
    public class ArticlesRepository : BaseApplicationService<Article, int>, IArticlesApplicationService
    {
        protected ArticlesRepository(IArticlesRepository repository) : base(repository)
        {
        }

        public new IArticlesRepository Repository { get { return base.Repository as IArticlesRepository; } }

        public int AddView(int articleId)
        {
            return Repository.AddView(articleId);
        }

        public Task<int> AddViewAsync(int articleId)
        {
            return Repository.AddViewAsync(articleId);
        }
    }
}
