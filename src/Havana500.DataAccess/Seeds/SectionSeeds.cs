using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Havana500.DataAccess.Contexts;
using Havana500.Domain;
using Microsoft.EntityFrameworkCore.Internal;
using SeedEngine.Core;

namespace Havana500.DataAccess.Seeds
{
    public class SectionSeeds : ISeed<Havana500DbContext>
    {
        public void AddOrUpdate(Havana500DbContext context, int amountOfObjects = 20)
        {
            if (EnumerableExtensions.Any(context.Sections))
                return;

            var sections = new List<Section>()
            {
                new Section()
                {
                    IsMainSection = true, 
                    Name = "Eventos",
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                },
                new Section()
                {
                    IsMainSection = true,
                    Name = "Experiencias",
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                },
                new Section(){
                    IsMainSection = true,
                    Name = "Galeria",
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                },
                new Section(){
                    IsMainSection = true,
                    Name = "Entretenimiento",
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                },
            };

            context.Sections.AddRange(sections);
            context.SaveChanges();

            var enterSection = context.Sections.FirstOrDefault(s => s.Name == "Entretenimiento");

            var subSections = new List<Section>()
            {
                new Section()
                {
                    IsMainSection = false,
                    Name = "Literatura",,
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                    ParentSectionId = enterSection.Id
                },
                new Section()
                {
                    IsMainSection = false,
                    Name = "Deportes",,
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                    ParentSectionId = enterSection.Id
                },
                new Section()
                {
                    IsMainSection = false,
                    Name = "Cine",,
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                    ParentSectionId = enterSection.Id
                },
                new Section()
                {
                    IsMainSection = false,
                    Name = "Cultura",,
                    CreatedAt = DateTime.Now, 
                    ModifiedAt = DateTime.Now,
                    CreatedBy = "Seeding", 
                    ModifiedBy = "Seeding"
                    ParentSectionId = enterSection.Id
                }
            };

            context.Sections.AddRange(subSections);
            context.SaveChanges();

        }

        public int OrderToByApplied => 2;
    }
}
