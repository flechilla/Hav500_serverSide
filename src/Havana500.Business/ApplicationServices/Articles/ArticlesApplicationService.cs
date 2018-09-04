using Havana500.Business.Base;
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
    public class ArticlesApplicationService : BaseApplicationService<Article, int>, IArticlesApplicationService
    {
        public ArticlesApplicationService(IArticlesRepository repository) : base(repository)
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

        /// <summary>
        ///     Gets the comments related to the <see cref="Article"/> with the 
        ///     given <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article that is parent of the comments.</param>
        /// <param name="currentPage">The currentPage of comments. This can be seen as the amount of pulls from the client.</param>
        /// <param name="amountOfComments">The amount of comments to return.</param>
        /// <returns></returns>
        public async Task<ICollection<Comment>> GetComments(int articleId, int currentPage, int amountOfComments)
        {
            return await Repository.GetComments(articleId, currentPage, amountOfComments);
        }
    }
}
