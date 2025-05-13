using DivisorApp.Core;

namespace DivisorApp.Services
{
    public static class DivisorService
    {
        public static int GetCountOfSameDivisorsLessThan(int k)
        {
            if (k < 3) return 0;
            return PrecomputedDivisorData.PrefixSameDivisors[k - 1];
        }
    }
}
