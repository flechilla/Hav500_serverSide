using System;
using System.Collections.Generic;
using System.Text;
using Havana500.DataAccess.Contexts;
using Havana500.Domain.Models.Media;
using SeedEngine.Core;

namespace Havana500.DataAccess.Seeds
{
    public class MarketingPictureSeeds : ISeed<Havana500DbContext>
    {
        public void AddOrUpdate(Havana500DbContext context, int amountOfObjects = 20)
        {
            var marketingImages = new List<Picture>(20);

            for (int i = 0; i < 10; i++)
            {
                marketingImages.Add(new Picture()
                {
                    RelativePath = "localhost:5000/marketingImages/testMarketingImage.jpg",
                    PictureType = PictureType.FirstLevelMarketing,
                    IsNew = true,
                    IsActive = true,
                    MimeType = "image/jpeg",
                    PictureExtension = "jpeg",
                    SeoFilename = "SeedImage"
                });
                marketingImages.Add(new Picture()
                {
                    RelativePath = "localhost:5000/marketingImages/testMarketingImage.jpg",
                    PictureType = PictureType.SecondaryLevelMarketing,
                    IsNew = true,
                    IsActive = true,
                    MimeType = "image/jpeg",
                    PictureExtension = "jpeg",
                    SeoFilename = "SeedImage"
                });
                marketingImages.Add(new Picture()
                {
                    RelativePath = "localhost:5000/marketingImages/testMarketingImage.jpg",
                    PictureType = PictureType.TertiaryLevelMarketing,
                    IsNew = true,
                    IsActive = true,
                    MimeType = "image/jpeg",
                    PictureExtension = "jpeg",
                    SeoFilename = "SeedImage"
                });
            }
            context.PIctures.AddRange(marketingImages);
            context.SaveChanges();
        }

        public int OrderToByApplied => 5;
    }
}
