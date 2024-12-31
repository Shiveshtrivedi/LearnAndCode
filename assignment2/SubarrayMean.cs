using System;

class SubarrayMeanCalculator {

    static (long[], long[], int) GetInputData() {
        var arrayAndQueryCount = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        var arrayElements = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
        var prefixSumArray = CalculatePrefixSum(arrayElements);
        return (arrayElements, prefixSumArray, arrayAndQueryCount[1]);
    }

    static long[] CalculatePrefixSum(long[] arrayElements) {
        long[] prefixSumArray = new long[arrayElements.Length + 1];
        prefixSumArray[0] = 0;
        for (int i = 1; i <= arrayElements.Length; i++) {
            prefixSumArray[i] = prefixSumArray[i - 1] + arrayElements[i - 1];
        }
        return prefixSumArray;
    }

    static void ProcessQueries(int queryCount, long[] prefixSumArray) {
        for (var i = 0; i < queryCount; i++) {
            var queryRange = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            long result = CalculateMean(prefixSumArray, queryRange[0], queryRange[1]);
            Console.WriteLine(result);
        }
    }

    static long CalculateMean(long[] prefixSumArray, int startIndex, int endIndex) {
        long sum = prefixSumArray[endIndex] - prefixSumArray[startIndex - 1];
        int count = endIndex - startIndex + 1;
        return sum / count;
    }

    static void Main(string[] args) {
        var (arrayElements, prefixSumArray, queryCount) = GetInputData();
        ProcessQueries(queryCount, prefixSumArray);
    }
}
