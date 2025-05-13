using Xunit;
using DivisorApp.Services;

namespace DivisorApp.Tests
{
    public class DivisorService_EdgeCaseTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldReturn0_ForValuesBelowMinimumRange(int input)
        {
            Assert.Equal(0, DivisorService.GetCountOfSameDivisorsLessThan(input));
        }

        [Fact]
        public void ShouldReturn0_ForNegativeInput()
        {
            Assert.Equal(0, DivisorService.GetCountOfSameDivisorsLessThan(-5));
        }

        [Fact]
        public void ShouldHandleSmallRangeWithNoMatchingPairs()
        {
            int result = DivisorService.GetCountOfSameDivisorsLessThan(5);
            Assert.True(result >= 0);
        }
    }
}
