namespace WayfinderProject.Domain.Models.Filters.MoogleShop
{
    public class MoogleShopCriteria : FilterCriteria
    {
        public List<string> Categories { get; set; } = new();

        public virtual bool IsActive => Categories.Count > 0 || Games.Count > 0;
    }
}
