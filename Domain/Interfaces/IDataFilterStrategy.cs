using WayfinderProject.Domain.Models.Filters;

namespace WayfinderProject.Domain.Interfaces
{
    public interface IDataFilterStrategy<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> data, FilterCriteria criteria);
        FilterState RefreshAvailableOptions(IEnumerable<T> allData, FilterState state);
    }

    public interface IDataFilterStrategy<T, TSubWrapper> where T : IFilterable
    {
        IEnumerable<T> Filter(IEnumerable<T> data, FilterCriteria criteria);
        FilterState RefreshAvailableOptions(IEnumerable<T> allData, IDictionary<string, TSubWrapper> subData, FilterState state);
    }
}
