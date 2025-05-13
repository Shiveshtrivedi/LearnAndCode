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
            for (int i = 1; i < Max; i++)
            {
                for (int j = i; j < Max; j += i)
                {
                    DivisorCounts[j]++;
                }
            }
        }
    }
}
