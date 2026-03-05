using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop.SubData;

namespace WayfinderProject.Domain.Strategies.MoogleShop
{
    public class InventoryFilterStrategy : IDataFilterStrategy<Inventory, EnemyDropWrapper>
    {
        public IEnumerable<Inventory> Filter(IEnumerable<Inventory> data, FilterCriteria criteria)
        {
            if (criteria is not InventoryCriteria sceneCriteria || !sceneCriteria.IsActive)
            {
                return data;
            }

            return data.Where(scene =>
                !Utilities.FilterFailed(sceneCriteria.Inventory, [scene.Name]) &&
                !Utilities.FilterFailed(sceneCriteria.Categories, [scene.Category]) &&
                !Utilities.FilterFailed(sceneCriteria.Currencies, [scene.Currency]) &&
                !Utilities.FilterFailed(sceneCriteria.Costs, [scene.Cost.ToString()]) &&
                (sceneCriteria.Games.Count == 0 || sceneCriteria.Games.Contains(scene.Game))
            );
        }

        public FilterState RefreshAvailableOptions(
            IEnumerable<Inventory> allData,
            IDictionary<string, EnemyDropWrapper> subData,
            FilterState state)
        {
            var rules = new List<FilterRule<Inventory>>
            {
                new() { Id = "Categories", Selector = s => [s.Category] },
                new() { Id = "Costs", Selector = s => [s.Cost.ToString()] },
                new() { Id = "Currencies", Selector = s => [s.Currency] },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Inventory Items", Selector = s => [s.Name] }
            };

            return Utilities.Refresh<Inventory, EnemyDrop, EnemyDropWrapper>(allData, subData, state, rules);
        }
    }
}
