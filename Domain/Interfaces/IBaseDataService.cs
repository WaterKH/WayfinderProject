using WayfinderProject.Domain.Models.Filters;

namespace WayfinderProject.Domain.Interfaces
{
    public interface IBaseDataService
    {
        FilterState State { get; }
        bool IsLoaded { get; }

        Task InitializeAsync(string path);
        void UpdateFilter(string filterId, string value);
        void ClearFilters();
        void RefreshAvailableMetadata();

        IEnumerable<object> GetFilteredResults();
        object GetRandomResult(string seed = "");
        IEnumerable<string> GetOrderedCategoryNames();

    }
}
