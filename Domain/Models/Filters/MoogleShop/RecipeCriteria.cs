namespace WayfinderProject.Domain.Models.Filters.MoogleShop
{
    public class RecipeCriteria : MoogleShopCriteria
    {
        public List<string> Recipes { get; set; } = new();

        public override bool IsActive => Recipes.Count > 0 || Categories.Count > 0 || Games.Count > 0;
    }
}
