using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.DataAccess.Seeds
{
    public static class SeedUtils
    {
        public static DateTime GenerateRandomDate(int minYear = 2015)
        {
            var random = new Random(DateTime.Now.Millisecond);

            var initialDate = new DateTime(2005, 11, 14);

            var amountOfYear = random.Next(0, 13);

            var output = initialDate.AddDays(random.Next(0, 365 * amountOfYear));

            return output;
        }
    }
}
