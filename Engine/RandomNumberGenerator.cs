using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Engine
{
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomnumber = new byte[1];
            _generator.GetBytes(randomnumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomnumber[0]);

            //Using Math.Max and subtracting 0.0000001
            // to ensure multipler will always be between 0.0 and 9.9

            double multipler = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001);

            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multipler * range);

            return (int)(minimumValue + randomValueInRange);
        }
    }
}
