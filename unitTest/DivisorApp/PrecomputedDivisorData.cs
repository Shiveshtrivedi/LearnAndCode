namespace DivisorApp.Core
{
    public static class PrecomputedDivisorData
    {
        public static readonly int[] PrefixSameDivisors = new int[DivisorCalculator.Max];

        static PrecomputedDivisorData()
        {
            ComputePrefixData();
        }

        private static void ComputePrefixData()
        {
            for (int i = 2; i < DivisorCalculator.Max - 1; i++)
            {
                PrefixSameDivisors[i] = PrefixSameDivisors[i - 1];
                if (DivisorCalculator.DivisorCounts[i] == DivisorCalculator.DivisorCounts[i + 1])
                {
                    PrefixSameDivisors[i]++;
                }
            }

            for (int i = DivisorCalculator.Max - 1; i < DivisorCalculator.Max; i++)
            {
                PrefixSameDivisors[i] = PrefixSameDivisors[i - 1];
            }
        }
    }
}
