using System.Text.Json.Serialization;
using WayfinderProject.Domain.Interfaces;
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

        public bool ContainsText(string term, IEnumerable<object> data)
        {
            throw new NotImplementedException();
        }
    }
}