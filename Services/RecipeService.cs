using WayfinderProject.Domain;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MoogleShop;

namespace WayfinderProject.Services
{
    public class RecipeService
    {
        private HttpClient _httpClient;
        private IEnumerable<Recipe> _recipes;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _recipes = GenerateRecipes().GetAwaiter().GetResult();
        }

        private async Task<IEnumerable<Recipe>> GenerateRecipes()
        {
            return new List<Recipe>();
        }

        public IEnumerable<Recipe> GetRecipes()
            => _recipes;
    }
}
