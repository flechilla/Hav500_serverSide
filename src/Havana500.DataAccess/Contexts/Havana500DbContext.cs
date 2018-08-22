using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Havana500.Domain;
using Havana500.Domain.Models.Media;

namespace Havana500.DataAccess.Contexts
{
    public class Havana500DbContext : IdentityDbContext, ISqlDbContext
    {
        public Havana500DbContext(DbContextOptions<Havana500DbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

           // builder.Entity<Comment>().Property(x => x.ApplicationUserId).IsRequired();
                          
        }

 

        /// <summary>
        ///     Gets or sets the <see cref="Comment"/> of the platform.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleContentTag> ArticlesContentTags{ get; set; }

        public DbSet<Picture> PIctures { get; set; }

        public DbSet<MediaStorage> MediaStorages { get; set; }

    }
}
