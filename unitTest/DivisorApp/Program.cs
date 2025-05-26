using System;
using DivisorApp.Core;
using DivisorApp.Services;

namespace DivisorApp
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Enter number of test cases:");
                if (!int.TryParse(Console.ReadLine(), out int numberTestCases) || numberTestCases < 1)
                {
                    Console.WriteLine("Invalid number of test cases.");
                    return;
                }

                IDivisorDataProvider dataProvider = new PrecomputedDivisorDataProvider();
                var divisorService = new DivisorService(dataProvider);

                for (int testCaseIndex = 0; testCaseIndex < numberTestCases; testCaseIndex++)
                {
                    Console.WriteLine("Enter k:");
                    if (int.TryParse(Console.ReadLine(), out int upperLimit))
                    {
                        int result = divisorService.GetCountOfSameDivisorsLessThan(upperLimit);
                        Console.WriteLine($"Result: {result}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Skipping this test case.");
                    }
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
