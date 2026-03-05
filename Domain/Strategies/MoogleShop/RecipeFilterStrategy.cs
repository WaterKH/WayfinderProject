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
            if (criteria is not RecipeCriteria recipeCriteria || !recipeCriteria.IsActive)
            {
                return data;
            }

            return data.Where(recipe =>
                !Utilities.FilterFailed(recipeCriteria.Recipes, [recipe.Name]) &&
                !Utilities.FilterFailed(recipeCriteria.Categories, [recipe.Category]) &&
                !Utilities.FilterFailed(recipeCriteria.Games, [recipe.Game]) &&
                (
                    string.IsNullOrEmpty(recipeCriteria.SearchTerm) || 
                    recipe.SubData.Any(item => item.Name.Contains(recipeCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    recipe.UnlockConditionDescription.Contains(recipeCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase)
                )
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
