using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Filters.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop;
using WayfinderProject.Domain.Models.MoogleShop.SubData;

namespace WayfinderProject.Services.MoogleShop
{
    public class RecipeService :
        BaseDataService<
            Recipe,
            RecipeCriteria,
            Material,
            MaterialWrapper,
            RecipeWrapper<Recipe>>
    {
        protected override List<string> CategoryPriority => new()
        {
            "Synthesis Items", "Games", "Categories"
        };

        public RecipeService(HttpClient httpClient, IDataFilterStrategy<Recipe, MaterialWrapper> filterStrategy)
            : base(httpClient, filterStrategy) { }

        protected override IEnumerable<Recipe> MapWrapperToData(RecipeWrapper<Recipe> wrapper)
        {
            return wrapper.WrappedData.SelectMany(kvp =>
                kvp.Value.Select(s => { s.Game = kvp.Key; return s; }));
        }
    }
}
