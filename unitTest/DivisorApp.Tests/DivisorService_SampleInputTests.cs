using Xunit;
using DivisorApp.Core;
using DivisorApp.Services;

namespace DivisorApp.Tests
{
    public class DivisorService_SampleInputTests
    {
        private readonly DivisorService _service;

        public DivisorService_SampleInputTests()
        {
            IDivisorDataProvider provider = new PrecomputedDivisorDataProvider();
            _service = new DivisorService(provider);
        }

        [Fact]
        public void ShouldReturn1_ForUpperLimit3()
        {
            Assert.Equal(1, _service.GetCountOfSameDivisorsLessThan(3));
        }

        [Fact]
        public void ShouldReturn2_ForUpperLimit15()
        {
            Assert.Equal(2, _service.GetCountOfSameDivisorsLessThan(15));
        }

        [Fact]
        public void ShouldReturn15_ForUpperLimit100()
        {
            Assert.Equal(15, _service.GetCountOfSameDivisorsLessThan(100));
        }
    }
}
