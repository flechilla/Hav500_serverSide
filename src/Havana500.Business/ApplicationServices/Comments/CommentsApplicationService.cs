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
    public class CommentsApplicationService : BaseApplicationService<Havana500.Domain.Comment,int>, ICommentsApplicationService
    {
        public CommentsApplicationService(ICommentsRepository repository) : base(repository)
        {
        }

        public void AddComment(Comment comments, Discriminator discriminator = Discriminator.Article)
        {
            comments.ParentDiscriminator = discriminator;
            this.Add(comments);
        }

        public IQueryable<Domain.Comment> ReadAll(int idparent, Discriminator discriminator = Discriminator.Article)
        {
            var repository = (Repository as ICommentsRepository);
            return repository.ReadAll((idparent, discriminator));
        }

        public IQueryable<Comment> ReadAll(int idparent, int Count, Discriminator discriminator = Discriminator.Article)
        {
            var repository = (Repository as ICommentsRepository);
            return repository.ReadAll((idparent, discriminator),Count);
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int idparent, Discriminator discriminator = Discriminator.Article)
        {
            var repository = (Repository as ICommentsRepository);
            return await repository.ReadAllAsync((idparent, discriminator));
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int idparent, int Count, Discriminator discriminator = Discriminator.Article)
        {
            var repository = (Repository as ICommentsRepository);
            return await repository.ReadAllAsync((idparent, discriminator),Count);
        }
    }
}
