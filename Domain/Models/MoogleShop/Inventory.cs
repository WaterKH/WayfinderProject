using System.Text.Json.Serialization;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.Attributes;
using WayfinderProject.Domain.Models.MoogleShop.SubData;

namespace WayfinderProject.Domain.Models.MoogleShop
{
    public class InventoryWrapper<T> : BaseWrapper<T>
    {
        [JsonPropertyName("Inventory")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class Inventory : BaseMoogleShopData<EnemyDrop>, IFilterable
    {
        public string Description { get; set; } = string.Empty;
        public string AdditionalInformation { get; set; } = string.Empty;
        public int Cost { get; set; } = 0;
        public string Currency { get; set; } = string.Empty;

        [DisplayInTable(headerName: "Cost", iconPath: "worlds_gray.png", order: 4, colorClass: "orange")]
        public string CombinedCostCurrency { get => Cost > 0 ? $"{Cost} {Currency}" : ""; }

        public bool ContainsText(string term, IEnumerable<object> data)
        {
            var items = data.Cast<Material>();

            return Description.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    AdditionalInformation.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    items.Any(item =>
                        item.Name.Contains(term, StringComparison.OrdinalIgnoreCase)
                    );
        }
    }
}