using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Services;

namespace WayfinderProject.Domain.Factories
{
    public class DataServiceFactory(IServiceProvider serviceProvider)
    {
        public async Task<TService?> CreateAsync<TService, T, TCriteria, TWrapper>(string path)
            where TService : BaseDataService<T, TCriteria, TWrapper>
            where TCriteria : FilterCriteria, new()
            where TWrapper : new()
        {
            var service = serviceProvider.GetService<TService>();

            if (service == null) return default;

            await service.InitializeAsync(path);
            return service;
        }

        public async Task<TService?> CreateAsync<TService, T, TCriteria, TSubData, TSubDataWrapper, TWrapper>(string path)
            where TService : SubBaseDataService<T, TCriteria, TSubData, TSubDataWrapper, TWrapper>
            where T : BaseData<TSubData>, IFilterable
            where TCriteria : FilterCriteria, new()
            where TSubDataWrapper : BaseSubWrapper<TSubData>, new()
            where TWrapper : new()
        {
            var service = serviceProvider.GetService<TService>();

            if (service == null) return default;

            await service.InitializeAsync(path);
            return service;
        }
    }
}
