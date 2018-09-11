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
    public class ContentTagSeeds : ISeed<Havana500DbContext>
    {
        public void AddOrUpdate(Havana500DbContext context, int amountOfObjects = 20)
        {
            if (context.ContentTags.Any())
                return;

            var contentTagGenerator = new Faker<ContentTag>()
                .RuleFor(c => c.Name, (f, c) => f.Random.Word());

            var tags = contentTagGenerator.Generate(30);

            context.ContentTags.AddRange(tags);

            
        }

        public int OrderToByApplied => 2;
    }
}
