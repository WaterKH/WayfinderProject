using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters;
using WayfinderProject.Domain.Models.Filters.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop.SubData;

namespace WayfinderProject.Domain.Strategies.MoogleShop
{
    public class RecipeFilterStrategy : IDataFilterStrategy<Recipe, MaterialWrapper>
    {
        public IEnumerable<Recipe> Filter(IEnumerable<Recipe> data, FilterCriteria criteria)
        {
            if (criteria is not RecipeCriteria sceneCriteria || !sceneCriteria.IsActive)
            {
                return data;
            }

            return data.Where(scene =>
                !Utilities.FilterFailed(sceneCriteria.Recipes, [scene.Name]) &&
                !Utilities.FilterFailed(sceneCriteria.Categories, [scene.Category]) &&
                (sceneCriteria.Games.Count == 0 || sceneCriteria.Games.Contains(scene.Game))
            );
        }

        public FilterState RefreshAvailableOptions(
            IEnumerable<Recipe> allData,
            IDictionary<string, MaterialWrapper> subData,
            FilterState state)
        {
            var rules = new List<FilterRule<Recipe>>
            {
                new() { Id = "Categories", Selector = s => [s.Category] },
                new() { Id = "Games", Selector = s => [s.Game] },
                new() { Id = "Recipe Items", Selector = s => [s.Name] }
            };

            return Utilities.Refresh<Recipe, Material, MaterialWrapper>(allData, subData, state, rules);
        }
    }
}
