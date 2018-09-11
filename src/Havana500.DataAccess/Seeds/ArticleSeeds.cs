using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using SeedEngine.Core;

namespace Havana500.DataAccess.Seeds
{
    public class ArticleSeeds : ISeed<Havana500DbContext>
    {
        public void AddOrUpdate(Havana500DbContext context, int amountOfObjects = 20)
        {
            if (context.Articles.Any())
                return;
            var sections = context.Sections.FirstOrDefault(s => s.Name == "Entretenimiento")?.SubSections;

            if(sections == null)
                throw new Exception("There is not section with the name 'Entretenimiento'");

            foreach (var section in sections)
            {
                section.Articles = GenerateArticles(context);
            }
        }

        private List<Article> GenerateArticles(Havana500DbContext context, int bodyMinParagraphs = 5, int bodyMaxParagraphs = 20)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var amountOfArticles = random.Next(50, 1000);

            var tags = context.ContentTags.ToList();

            var articleGenerator = new Faker<Article>()
                .RuleFor(a => a.Body, (f, a) => f.Lorem.Paragraphs(bodyMinParagraphs, bodyMaxParagraphs))
                .RuleFor(a => a.EditorWeight, random.Next(1, 4))
                .RuleFor(a => a.Title, (f, a) => f.Lorem.Sentence())
                .RuleFor(a => a.MetaTitle, (f, a) => a.Title)
                .RuleFor(a => a.MetaDescription, (f, a) => f.Lorem.Sentence())
                .RuleFor(a => a.MetaKeywords, (f, a) => f.Lorem.Words().ToString())
                .RuleFor(a => a.Views, (f, a) => f.Random.Int(0, 10000000))
                .RuleFor(a => a.StartDateUtc, (f, a) => SeedUtils.GenerateRandomDate())
                .RuleFor(a => a.EndDateUtc, (f, a) => SeedUtils.GenerateRandomDate(a.StartDateUtc.Year))
                .RuleFor(a => a.ReadingTime, (f, a) => f.Random.Int(3, 20))
                .RuleFor(a => a.ArticleContentTags, 
                            (f, a) => f.Random.ListItems(tags, f.Random.Int(2, 10)).
                            Select(ct=>new ArticleContentTag()
                                {
                                    Article = a, 
                                    ContentTag = ct
                                }));


            var articles = articleGenerator.Generate(amountOfArticles);

            foreach (var article in articles)
            {
                article.Comments = GenerateComments(article.StartDateUtc);
            }

            return articles;
        }

        private List<Comment> GenerateComments(DateTime articleStartDate)
        {
            var random = new Random(DateTime.Now.Millisecond);

            var commentGenerator = new Faker<Comment>()
                .RuleFor(c => c.Body, (f, c) => f.Lorem.Sentence(random.Next(1, 100)))
                .RuleFor(c => c.Likes, (f, c) => f.Random.Int(0, 10000))
                .RuleFor(c => c.Likes, (f, c) => f.Random.Int(0, 5000))
                .RuleFor(c => c.CreatedAt, (f, c) => articleStartDate.AddDays(f.Random.Int(0, 777)))
                .RuleFor(c=>c.IsApproved, (f, c)=>f.Random.Bool())
                .RuleFor(c => c.UserName, (f, c) => f.Internet.UserName())
                .RuleFor(c => c.UserEmail, (f, c) => f.Internet.Email(c.UserName))
                .RuleFor(c => c.IpAddress, (f, c) => f.Internet.Ip());

            return commentGenerator.Generate(random.Next(0, 1000));
        }

        public int OrderToByApplied => 3;
    }
}
