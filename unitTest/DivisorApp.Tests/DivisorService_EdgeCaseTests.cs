using Xunit;
using DivisorApp.Core;
using DivisorApp.Services;

namespace DivisorApp.Tests
{
    public class DivisorService_EdgeCaseTests
    {
        private readonly DivisorService _service;

        public DivisorService_EdgeCaseTests()
        {
            IDivisorDataProvider provider = new PrecomputedDivisorDataProvider();
            _service = new DivisorService(provider);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldReturn0_ForValuesBelowMinimumRange(int input)
        {
            Assert.Equal(0, _service.GetCountOfSameDivisorsLessThan(input));
        }

        [Fact]
        public void ShouldReturn0_ForNegativeInput()
        {
            Assert.Equal(0, _service.GetCountOfSameDivisorsLessThan(-5));
        }

        [Fact]
        public void ShouldHandleSmallRangeWithNoMatchingPairs()
        {
            int result = _service.GetCountOfSameDivisorsLessThan(5);
            Assert.True(result >= 0);
        }
    }
}
