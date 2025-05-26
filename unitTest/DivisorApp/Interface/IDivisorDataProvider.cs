namespace DivisorApp.Core
{
    public interface IDivisorDataProvider
    {
        int GetPrefixSameDivisorCount(int limit);
    }
}
