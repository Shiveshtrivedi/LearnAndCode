using DivisorApp.Core;

namespace DivisorApp.Services
{
    public class DivisorService
    {
        private readonly IDivisorDataProvider _dataProvider;

        public DivisorService(IDivisorDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int GetCountOfSameDivisorsLessThan(int upperLimit)
        {
            if (upperLimit < 3) return 0;
            return _dataProvider.GetPrefixSameDivisorCount(upperLimit - 1);
        }
    }
}
