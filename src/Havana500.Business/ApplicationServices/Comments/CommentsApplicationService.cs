using Havana500.Business.ApplicationServices.Comments;
using Havana500.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Repositories;
using Havana500.Domain;
using System.Linq;
using System.Threading.Tasks;
using Havana500.Domain.Enums;

namespace Havana500.Business.ApplicationServices.Comments
{
    public class CommentsApplicationService : BaseApplicationService<Comment,int>, ICommentsApplicationService
    {
     
        protected new ICommentsRepository Repository => base.Repository as ICommentsRepository;

        public CommentsApplicationService(ICommentsRepository repository) : base(repository)
        {
        }

        public void AddComment(Comment comments)
        {
            //comments.ParentDiscriminator = discriminator;
            this.Add(comments);
        }

        public IQueryable<Domain.Comment> ReadAll(int articleId)
        {
            return Repository.ReadAll(articleId);
        }

        public IQueryable<Comment> ReadAll(int articleId, int Count)
        {
            return Repository.ReadAll(articleId, Count);
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int articleId)
        {
            return await Repository.ReadAllAsync(articleId);
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int articleId, int Count)
        {
            return await Repository.ReadAllAsync(articleId, Count);
        }
    }
}
