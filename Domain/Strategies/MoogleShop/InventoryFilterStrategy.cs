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
            if (criteria is not InventoryCriteria inventoryCriteria || !inventoryCriteria.IsActive)
            {
                return data;
            }

            return data.Where(inventory =>
                !Utilities.FilterFailed(inventoryCriteria.Inventory, [inventory.Name]) &&
                !Utilities.FilterFailed(inventoryCriteria.Categories, [inventory.Category]) &&
                !Utilities.FilterFailed(inventoryCriteria.Currencies, [inventory.Currency]) &&
                !Utilities.FilterFailed(inventoryCriteria.Costs, [inventory.Cost.ToString()]) &&
                !Utilities.FilterFailed(inventoryCriteria.Games, [inventory.Game]) &&
                (
                    string.IsNullOrEmpty(inventoryCriteria.SearchTerm) ||
                    inventory.SubData.Any(item => item.AdditionalInformation.Contains(inventoryCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                    item.EnemyName.Contains(inventoryCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    inventory.Description.Contains(inventoryCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    inventory.AdditionalInformation.Contains(inventoryCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase)
                )
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
