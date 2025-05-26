namespace DivisorApp.Core
{
    public class PrecomputedDivisorDataProvider : IDivisorDataProvider
    {
        private static readonly int[] PrefixSameDivisors = new int[DivisorCalculator.Max];

        static PrecomputedDivisorDataProvider()
        {
            ComputePrefixData();
        }

        private static void ComputePrefixData()
        {
            for (int currentNumber = 2; currentNumber < DivisorCalculator.Max - 1; currentNumber++)
            {
                PrefixSameDivisors[currentNumber] = PrefixSameDivisors[currentNumber - 1];
                if (DivisorCalculator.DivisorCounts[currentNumber] == DivisorCalculator.DivisorCounts[currentNumber + 1])
                {
                    PrefixSameDivisors[currentNumber]++;
                }
            }

            PrefixSameDivisors[DivisorCalculator.Max - 1] = PrefixSameDivisors[DivisorCalculator.Max - 2];
        }

        public int GetPrefixSameDivisorCount(int limit)
        {
            if (limit < 0 || limit >= DivisorCalculator.Max)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), $"Limit must be between 0 and {DivisorCalculator.Max - 1}.");
            }

            return PrefixSameDivisors[limit];
        }
    }
}
