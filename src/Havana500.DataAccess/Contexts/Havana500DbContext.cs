using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Havana500.Domain;
using Havana500.Domain.Models.Media;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

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

            builder.Entity<ArticleContentTag>()
                .HasKey(t => new { t.ArticleId, t.ContentTagId });

            builder.Entity<ArticleContentTag>()
                .HasOne(a => a.Article)
                .WithMany(a => a.ArticleContentTags)
                .HasForeignKey(a => a.ArticleId);

            builder.Entity<ArticleContentTag>()
                .HasOne(a => a.ContentTag)
                .WithMany(a => a.ArticleContentTags)
                .HasForeignKey(a => a.ContentTagId);

            builder.Entity<MarketingContentTag>()
                .HasKey(m => new { m.MarketingContentId, m.ContentTagId });

            builder.Entity<MarketingContentTag>()
                .HasOne(m => m.MarketingContent)
                .WithMany(m => m.MarketingContentTags)
                .HasForeignKey(m => m.MarketingContentId);

            builder.Entity<MarketingContentTag>()
                .HasOne(c => c.ContentTag)
                .WithMany(c => c.MarketingContentTags)
                .HasForeignKey(c => c.ContentTagId);

            builder.Entity<PictureContentTag>()
                .HasKey(m => new { m.PictureId, m.ContentTagId });

            builder.Entity<PictureContentTag>()
                .HasOne(m => m.Picture)
                .WithMany(m => m.PicturesContentTags)
                .HasForeignKey(m => m.PictureId);

            builder.Entity<PictureContentTag>()
                .HasOne(c => c.ContentTag)
                .WithMany(c => c.PicturesContentTags)
                .HasForeignKey(c => c.ContentTagId);


            //builder.Entity<Section>()
            //    .HasMany(s => s.SubSections)
            //    .WithOne(s => s.ParentSection)
            //    .HasForeignKey(s => s.ParentSectionId)
            //    .OnDelete(DeleteBehavior.Restrict);TODO: To fix the relation

        }



        /// <summary>
        ///     Gets or sets the <see cref="Comment"/> of the platform.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ContentTag> ContentTags { get; set; }

        public DbSet<ArticleContentTag> ArticleContentTag { get; set; }

        public DbSet<MarketingContent> MarketingContents { get; set; }

        public DbSet<MarketingContentTag> MarketingContentTags { get; set; }

        public DbSet<Picture> PIctures { get; set; }

        public DbSet<MediaStorage> MediaStorages { get; set; }

    }
}
