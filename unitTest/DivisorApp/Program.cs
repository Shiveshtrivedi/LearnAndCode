using DivisorApp.Services;

namespace DivisorApp
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter number of test cases:");
            if (!int.TryParse(Console.ReadLine(), out int numberTestCases) || numberTestCases < 1)
            {
                Console.WriteLine("Invalid number of test cases.");
                return;
            }

            for (int testCaseIndex = 0; testCaseIndex < numberTestCases; testCaseIndex++)
            {
                Console.WriteLine("Enter k:");
                if (int.TryParse(Console.ReadLine(), out int upperLimit))
                {
                    int result = DivisorService.GetCountOfSameDivisorsLessThan(upperLimit);
                    Console.WriteLine($"Result: {result}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Skipping this test case.");
                }
            }

        }
    }
}
