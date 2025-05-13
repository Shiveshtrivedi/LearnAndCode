using Xunit;
using DivisorApp.Services;

namespace DivisorApp.Tests
{
    public class DivisorService_SampleInputTests
    {
        [Fact]
        public void ShouldReturn1_ForUpperLimit3()
        {
            Assert.Equal(1, DivisorService.GetCountOfSameDivisorsLessThan(3));
        }

        [Fact]
        public void ShouldReturn2_ForUpperLimit15()
        {
            Assert.Equal(2, DivisorService.GetCountOfSameDivisorsLessThan(15));
        }

        [Fact]
        public void ShouldReturn15_ForUpperLimit100()
        {
            Assert.Equal(15, DivisorService.GetCountOfSameDivisorsLessThan(100));
        }
    }
}
