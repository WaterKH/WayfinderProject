namespace WayfinderProject.Domain.Models.Filters.MoogleShop
{
    public class InventoryCriteria : MoogleShopCriteria
    {
        public List<string> Inventory { get; set; } = new();
        public List<string> Costs { get; set; } = new();
        public List<string> Currencies { get; set; } = new();

        public override bool IsActive => Inventory.Count > 0 || Categories.Count > 0 || 
                                         Costs.Count > 0 || Currencies.Count > 0 || Games.Count > 0 ||
                                         !string.IsNullOrEmpty(SearchTerm);
    }
}
