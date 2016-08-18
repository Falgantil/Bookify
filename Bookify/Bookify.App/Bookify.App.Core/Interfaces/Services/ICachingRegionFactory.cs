namespace Bookify.App.Core.Interfaces.Services
{
    public interface ICachingRegionFactory
    {
        ICachingRegion<T> CreateRegion<T>(string regionName);
    }
}