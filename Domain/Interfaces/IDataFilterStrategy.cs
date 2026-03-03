using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.MemoryArchive;

namespace WayfinderProject.Domain.Interfaces
{
    public interface IDataFilterStrategy<T, TSubWrapper> where T : IFilterable
    {
        IEnumerable<T> Filter(IEnumerable<T> data, FilterCriteria criteria);
        FilterState RefreshAvailableOptions(IEnumerable<T> allData, IDictionary<string, TSubWrapper> subData, FilterState state);
    }
}
