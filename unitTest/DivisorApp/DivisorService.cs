using DivisorApp.Core;

namespace DivisorApp.Services
{
    public static class DivisorService
    {
        public static int GetCountOfSameDivisorsLessThan(int upperLimit)
        {
            if (upperLimit < 3) return 0;
            return PrecomputedDivisorData.PrefixSameDivisors[upperLimit - 1];
        }
    }
}
