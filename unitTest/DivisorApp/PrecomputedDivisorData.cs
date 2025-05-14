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
            for (int index = 2; index < DivisorCalculator.Max - 1; index++)
            {
                PrefixSameDivisors[index] = PrefixSameDivisors[index - 1];
                if (DivisorCalculator.DivisorCounts[index] == DivisorCalculator.DivisorCounts[index + 1])
                {
                    PrefixSameDivisors[index]++;
                }
            }

            for (int index = DivisorCalculator.Max - 1; index < DivisorCalculator.Max; index++)
            {
                PrefixSameDivisors[index] = PrefixSameDivisors[index - 1];
            }
        }
    }
}
