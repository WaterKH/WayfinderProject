using System.Text.Json.Serialization;
using WayfinderProject.Domain.Interfaces;
using WayfinderProject.Domain.Models.MemoryArchive.SubData;
using WayfinderProject.Domain.Models.MoogleShop.SubData;

namespace WayfinderProject.Domain.Models.MoogleShop
{
    public class RecipeWrapper<T> : BaseWrapper<T>
    {
        [JsonPropertyName("Recipes")]
        public override Dictionary<string, List<T>> WrappedData { get; set; } = new();
    }

    public class Recipe : BaseMoogleShopData<Material>, IFilterable
    {
        public string UnlockConditionDescription { get; set; } = string.Empty;

        public bool ContainsText(string term, IEnumerable<object> data)
        {
            var materials = data.Cast<Material>();

            return UnlockConditionDescription.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    materials.Any(material =>
                        material.Name.Contains(term, StringComparison.OrdinalIgnoreCase)
                    );
        }
    }
}