using Xunit;
using DivisorApp.Services;

namespace DivisorApp.Tests
{
    public class DivisorService_PerformanceTests
    {
        [Fact]
        public void ShouldHandleLargeInput()
        {
            int result = DivisorService.GetCountOfSameDivisorsLessThan(10_000_000);
            Assert.True(result >= 0);
        }
    }
}
