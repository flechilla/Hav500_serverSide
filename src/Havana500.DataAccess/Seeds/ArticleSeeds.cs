using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using SeedEngine.Core;
using Havana500.Domain.Models.Media;

namespace Havana500.DataAccess.Seeds
{
    public class ArticleSeeds : ISeed<Havana500DbContext>
    {
        private const int MIN_AMOUNT_OF_ARTICLES_PER_SECTION = 10;
        private const int MAX_AMOUNT_OF_ARTICLES_PER_SECTION = 100;

        private const int MIN_AMOUNT_OF_COMMENTS_PER_ARTICLE = 0;
        private const int MAX_AMOUNT_OF_COMMENTS_PER_ARTICLE = 100;


        public void AddOrUpdate(Havana500DbContext context, int amountOfObjects = 20)
        {
            if (context.Articles.Any())
                return;
            var enterSectionId = context.Sections.FirstOrDefault(s => s.Name == "Entretenimiento").Id;
            var sections = context.Sections.Where(s => s.ParentSectionId == enterSectionId).ToList();

            if (sections == null)
                throw new Exception("There is not section with the name 'Entretenimiento'");

            foreach (var section in sections)
            {
                var localArticles = GenerateArticles(context);
                section.Articles = localArticles;
                section.AmountOfArticles = localArticles.Count();
                section.Views = localArticles.Sum(a => a.Views);
            }
            context.Sections.UpdateRange(sections);
            context.SaveChanges();

            var articles = context.Articles.ToArray();

            foreach (var article in articles)
            {
                article.Comments = GenerateComments(article.StartDateUtc, article.Id);
                article.AmountOfComments = article.Comments.Count();
                article.NotApprovedCommentCount = article.Comments.Count(a => a.IsApproved == false);
                article.ApprovedCommentCount = article.Comments.Count(a => a.IsApproved);
                article.Body = $"<p>{article.Body}</p>";
            }

            context.Articles.UpdateRange(articles);
            context.SaveChanges();

            var tags = context.ContentTags.ToList();

            foreach(var tag in tags)
            {
                var count = context.ArticleContentTag.Count(act => act.ContentTagId == tag.Id);
                tag.AmountOfContent = count;
            }
            context.ContentTags.UpdateRange(tags);
            context.SaveChanges();
        }

        private List<Article> GenerateArticles(Havana500DbContext context, int bodyMinParagraphs = 5, int bodyMaxParagraphs = 20)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var amountOfArticles = random.Next(MIN_AMOUNT_OF_ARTICLES_PER_SECTION, MAX_AMOUNT_OF_ARTICLES_PER_SECTION);

            var tags = context.ContentTags.ToList();
            var langs = new string[] { "es", "en", "fr" };

            var articleGenerator = new Faker<Article>()
                .RuleFor(a => a.Body, (f, a) => f.Lorem.Paragraphs(bodyMinParagraphs, bodyMaxParagraphs, "</p><p>"))
                .RuleFor(a => a.EditorWeight, (new Random(DateTime.Now.Millisecond)).Next(1, 4))
                .RuleFor(a => a.Title, (f, a) => f.Lorem.Sentence())
                .RuleFor(a => a.MetaTitle, (f, a) => a.Title)
                .RuleFor(a => a.MetaDescription, (f, a) => f.Lorem.Sentence())
                .RuleFor(a => a.MetaKeywords, (f, a) => f.Lorem.Words().Aggregate((w, w1)=> w+" "+w1))
                .RuleFor(a => a.Views, (f, a) => f.Random.Int(0, 10000))
                .RuleFor(a => a.AllowAnonymousComments, (f, a) => f.Random.Bool(0.7f))
                .RuleFor(a => a.AllowComments, (f, a) => f.Random.Bool(0.9f))
                .RuleFor(a => a.StartDateUtc, (f, a) => SeedUtils.GenerateRandomDate())
                .RuleFor(a => a.CreatedAt, (f, a) => DateTime.Now)
                .RuleFor(a => a.ModifiedAt, (f, a) => DateTime.Now)
                .RuleFor(a => a.CreatedBy, "Seeding")
                .RuleFor(a => a.ModifiedBy, "Seeding")
                .RuleFor(a => a.EndDateUtc, (f, a) => SeedUtils.GenerateRandomDate(a.StartDateUtc.Year))
                .RuleFor(a => a.ReadingTime, (f, a) => f.Random.Int(3, 20))
                .RuleFor(a=>a.LanguageCulture, (f, a) => f.Random.ArrayElement(langs))
                .RuleFor(a=>a.MainPicture, new Picture
                {
                    HRef = "http://localhost:5000/images/article-header.png",
                    PictureType = PictureType.ArticleMainPicture,
                    SeoFilename = "Seed Image"
                })
                .RuleFor(a => a.ArticleContentTags, 
                            (f, a) => f.Random.ListItems(tags, f.Random.Int(2, 10)).
                            Select(ct=>new ArticleContentTag()
                                {
                                    Article = a, 
                                    ContentTag = ct
                                }).ToList());


            var articles = articleGenerator.Generate(amountOfArticles);
            return articles;
        }

        private List<Comment> GenerateComments(DateTime articleStartDate, int articleId)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var amountOfComments = random.Next(MIN_AMOUNT_OF_COMMENTS_PER_ARTICLE, MAX_AMOUNT_OF_COMMENTS_PER_ARTICLE);

            var commentGenerator = new Faker<Comment>()
                .RuleFor(c => c.Body, (f, c) => f.Lorem.Sentence(random.Next(1, 100)))
                .RuleFor(c => c.Likes, (f, c) => f.Random.Int(0, 10000))
                .RuleFor(c => c.Dislikes, (f, c) => f.Random.Int(0, 5000))
                .RuleFor(c => c.CreatedAt, (f, c) => articleStartDate.AddDays(f.Random.Int(0, 777)))
                .RuleFor(c => c.ModifiedAt, (f, c) => c.CreatedAt)
                .RuleFor(c=>c.IsApproved, (f, c)=>f.Random.Bool())
                .RuleFor(c => c.UserName, (f, c) => f.Internet.UserName())
                .RuleFor(c => c.UserEmail, (f, c) => f.Internet.Email(c.UserName))
                .RuleFor(c => c.IpAddress, (f, c) => f.Internet.Ip())
                .RuleFor(c=> c.ArticleId, articleId);

            

            return commentGenerator.Generate(amountOfComments);
        }

        public int OrderToByApplied => 4;
    }
}
