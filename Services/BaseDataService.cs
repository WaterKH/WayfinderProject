using WayfinderProject.Domain;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models;
using WayfinderProject.Domain.Models.Filters;

namespace WayfinderProject.Services
{
    public abstract class BaseDataService<T, TCriteria, TWrapper> : IBaseDataService
        
        where TCriteria : FilterCriteria, new()
        where TWrapper : new()
    {
        protected readonly HttpClient HttpClient;
        protected IEnumerable<T> Data = new List<T>();
        protected readonly IDataFilterStrategy<T> FilterStrategy;

        public bool IsLoaded { get; protected set; }
        public FilterState State { get; protected set; } = new();
        protected virtual List<string> CategoryPriority => new();

        private Random random = new();

        protected BaseDataService(HttpClient httpClient, IDataFilterStrategy<T> filterStrategy)
        {
            HttpClient = httpClient;
            FilterStrategy = filterStrategy;
        }

        public async Task InitializeAsync(string path)
        {
            if (IsLoaded) return;

            var wrapper = await Task.Run(async () => await Utilities.LoadJsonAsync<TWrapper>(HttpClient, path));
            Data = MapWrapperToData(wrapper);

            RefreshAvailableMetadata();

            IsLoaded = true;
        }

        protected abstract IEnumerable<T> MapWrapperToData(TWrapper wrapper);

        protected IEnumerable<T> GetData(TCriteria criteria)
            => FilterStrategy.Filter(Data, criteria);

        public IEnumerable<T> GetAllData()
            => Data;

        public TCriteria GetCurrentCriteria()
        {
            var criteria = new TCriteria();
            var properties = typeof(TCriteria).GetProperties();

            foreach (var selected in State.Selected)
            {
                var prop = properties.FirstOrDefault(p =>
                    p.Name.Equals(selected.Key, StringComparison.OrdinalIgnoreCase));

                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(criteria, selected.Value);
                }
            }

            criteria.SearchTerm = State.SearchTerm;

            return criteria;
        }

        public void UpdateFilter(string filterId, string value)
        {
            if (!State.Selected.ContainsKey(filterId)) State.Selected[filterId] = new();

            if (State.Selected[filterId].Contains(value))
                State.Selected[filterId].Remove(value);
            else
                State.Selected[filterId].Add(value);

            RefreshAvailableMetadata();
        }

        public void ClearFilters()
        {
            foreach (var key in State.Selected.Keys)
            {
                State.Selected[key].Clear();
            }

            foreach (var key in State.Initial.Keys)
            {
                State.Available[key] = new List<string>(State.Initial[key]);
            }
        }

        public virtual void RefreshAvailableMetadata()
        {
            if (Data == null || !Data.Any()) return;

            State = FilterStrategy.RefreshAvailableOptions(Data, State);
        }

        public IEnumerable<object> GetFilteredResults()
        {
            var criteria = GetCurrentCriteria();

            var filteredData = FilterStrategy.Filter(this.Data, criteria);

            // TODO: Sort or Limit here

            return filteredData.ToList().Cast<object>();
        }

        public object GetRandomResult(string seed = "")
        {
            if (!string.IsNullOrEmpty(seed))
                random = new(Utilities.Hash(seed));
            else
                random = new();

            int randomNumber = random.Next(0, Data.Count());
            var randomResult = this.Data.ToList()[randomNumber];

            return randomResult!;
        }

        public IEnumerable<string> GetOrderedCategoryNames()
        {
            if (State?.Available == null) return Enumerable.Empty<string>();

            return State.Available.Keys
                .OrderBy(key => {
                    int index = CategoryPriority.IndexOf(key);
                    return index == -1 ? int.MaxValue : index;
                })
                .ToList();
        }
    }

    public abstract class BaseDataService<T, TCriteria, TSubData, TSubWrapper, TWrapper> : IBaseDataService
        where T : BaseData<TSubData>, IFilterable
        where TCriteria : FilterCriteria, new()
        where TSubWrapper : BaseSubWrapper<TSubData>, new()
        where TWrapper : new()
    {
        protected readonly HttpClient HttpClient;
        protected IEnumerable<T> Data = new List<T>();
        protected IDictionary<string, TSubWrapper> SubDataWrappers = new Dictionary<string, TSubWrapper>();
        protected readonly IDataFilterStrategy<T, TSubWrapper> FilterStrategy;

        public bool IsLoaded { get; protected set; }
        public FilterState State { get; protected set; } = new();
        protected virtual List<string> CategoryPriority => new();

        private Random random = new();

        protected BaseDataService(HttpClient httpClient, IDataFilterStrategy<T, TSubWrapper> filterStrategy)
        {
            HttpClient = httpClient;
            FilterStrategy = filterStrategy;
        }

        public async Task InitializeAsync(string path)
        {
            if (IsLoaded) return;

            var wrapper = await Task.Run(async () => await Utilities.LoadJsonAsync<TWrapper>(HttpClient, path));
            Data = MapWrapperToData(wrapper);

            RefreshAvailableMetadata();

            SubDataWrappers = await MapSubData();

            foreach (var data in Data)
            {
                if (!SubDataWrappers.TryGetValue(data.Game, out var subDataWrapper)) continue;

                var entries = subDataWrapper.WrappedMemoryArchiveSubData;

                if (entries.TryGetValue(data.Name, out var foundData))
                {
                    data.SubData = foundData;
                }
                else if (entries.TryGetValue("None", out var noneFallback))
                {
                    data.SubData = noneFallback;
                }
            }

            IsLoaded = true;
        }

        protected abstract IEnumerable<T> MapWrapperToData(TWrapper wrapper);

        protected IEnumerable<T> GetData(TCriteria criteria)
            => FilterStrategy.Filter(Data, criteria);

        public IEnumerable<T> GetAllData()
            => Data;

        protected async Task<IDictionary<string, TSubWrapper>> MapSubData()
        {
            if (Data == null) return default!;

            foreach (var data in Data)
            {
                string path = Utilities.GetScriptPath(data, data.Game);

                var subWrapper = await GetSubDataWrapper(data.Name, data.Game, path);

                SubDataWrappers[data.Game] = subWrapper!;
            }

            return SubDataWrappers;
        }

        public async Task<TSubWrapper?> GetSubDataWrapper(string name, string gameName, string path)
        {
            T? data = Data.FirstOrDefault(s => s.Name == name && s.Game == gameName);
            if (data == null) return default!;

            if (!SubDataWrappers.TryGetValue(data.Game, out var subWrapper))
            {
                subWrapper = await Task.Run(async () => await Utilities.LoadJsonAsync<TSubWrapper>(HttpClient, path));
            }

            return subWrapper;
        }

        public TCriteria GetCurrentCriteria()
        {
            var criteria = new TCriteria();
            var properties = typeof(TCriteria).GetProperties();

            foreach (var selected in State.Selected)
            {
                var prop = properties.FirstOrDefault(p =>
                    p.Name.Equals(selected.Key, StringComparison.OrdinalIgnoreCase));

                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(criteria, selected.Value);
                }
            }

            criteria.SearchTerm = State.SearchTerm;

            return criteria;
        }

        public void UpdateFilter(string filterId, string value)
        {
            if (!State.Selected.ContainsKey(filterId)) State.Selected[filterId] = new();

            if (State.Selected[filterId].Contains(value)) 
                State.Selected[filterId].Remove(value);
            else 
                State.Selected[filterId].Add(value);

            RefreshAvailableMetadata();
        }

        public void ClearFilters()
        {
            foreach (var key in State.Selected.Keys)
            {
                State.Selected[key].Clear();
            }

            foreach (var key in State.Initial.Keys)
            {
                State.Available[key] = new List<string>(State.Initial[key]);
            }
        }

        public virtual void RefreshAvailableMetadata()
        {
            if (Data == null || !Data.Any()) return;

            State = FilterStrategy.RefreshAvailableOptions(Data, SubDataWrappers, State);
        }

        public IEnumerable<object> GetFilteredResults()
        {
            var criteria = GetCurrentCriteria();

            var filteredData = FilterStrategy.Filter(this.Data, criteria);

            // TODO: Sort or Limit here

            return filteredData.ToList().Cast<object>();
        }

        public object GetRandomResult(string seed = "")
        {
            if (!string.IsNullOrEmpty(seed))
                random = new(Utilities.Hash(seed));
            else
                random = new();

            int randomNumber = random.Next(0, Data.Count());
            var randomResult = this.Data.ToList()[randomNumber];

            return randomResult;
        }

        public IEnumerable<string> GetOrderedCategoryNames()
        {
            if (State?.Available == null) return Enumerable.Empty<string>();

            return State.Available.Keys
                .OrderBy(key => {
                    int index = CategoryPriority.IndexOf(key);
                    return index == -1 ? int.MaxValue : index;
                })
                .ToList();
        }
    }
}
