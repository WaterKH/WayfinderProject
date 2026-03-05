using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop.SubData;

namespace WayfinderProject.Services.MoogleShop
{
    public class InventoryService :
        BaseDataService<
            Inventory,
            InventoryCriteria,
            EnemyDrop,
            EnemyDropWrapper,
            InventoryWrapper<Inventory>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Inventory Items", "Games", "Category", "Costs", "Currencies"
        };

        public InventoryService(HttpClient httpClient, IDataFilterStrategy<Inventory, EnemyDropWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Inventory> MapWrapperToData(InventoryWrapper<Inventory> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
