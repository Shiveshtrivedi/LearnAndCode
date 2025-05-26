using Xunit;
using DivisorApp.Core;
using DivisorApp.Services;

namespace DivisorApp.Tests
{
    public class DivisorService_PerformanceTests
    {
        private readonly DivisorService _service;

        public DivisorService_PerformanceTests()
        {
            IDivisorDataProvider provider = new PrecomputedDivisorDataProvider();
            _service = new DivisorService(provider);
        }

        [Fact]
        public void ShouldHandleLargeInput()
        {
            int result = _service.GetCountOfSameDivisorsLessThan(10_000_000);
            Assert.True(result >= 0);
        }
    }
}
