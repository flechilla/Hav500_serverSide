using System;
using System.Collections.Generic;
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
            if (context.Sections.Any())
                return;

            var sections = new List<Section>()
            {
                new Section()
                {
                    IsMainSection = true, 
                    Name = "Eventos"
                },
                new Section()
                {
                    IsMainSection = true,
                    Name = "Experiencias"
                },
                new Section(){
                    IsMainSection = true,
                    Name = "Galeria"
                },
                new Section(){
                    IsMainSection = true,
                    Name = "Entretenimiento",
                    SubSections = new List<Section>()
                    {
                        new Section()
                        {
                            IsMainSection = false, 
                            Name = "Literatura"
                        }, 
                        new Section()
                        {
                            IsMainSection = false, 
                            Name = "Deportes"
                        },
                        new Section()
                        {
                            IsMainSection = false,
                            Name = "Cine"
                        },
                        new Section()
                        {
                            IsMainSection = false,
                            Name = "Cultura"
                        }
                    }
                },
            };

            context.Sections.AddRange(sections);
        }

        public int OrderToByApplied => 2;
    }
}
