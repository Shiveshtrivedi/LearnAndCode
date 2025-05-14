namespace DivisorApp.Core
{
    public static class DivisorCalculator
    {
        public const int Max = 10_000_001;
        public static readonly int[] DivisorCounts = new int[Max];

        static DivisorCalculator()
        {
            ComputeDivisors();
        }

        private static void ComputeDivisors()
        {
            for (int divisor = 1; divisor < Max; divisor++)
            {
                for (int multiple = divisor; multiple < Max; multiple += divisor)
                {
                    DivisorCounts[multiple]++;
                }
            }
        }
    }
}
