using WayfinderProject.Domain;
using WayfinderProject.Domain.Models.MemoryArchive;
using WayfinderProject.Domain.Models.MoogleShop;

namespace WayfinderProject.Services
{
    public class InventoryService
    {
        private HttpClient _httpClient;
        private IEnumerable<Inventory> _inventory;

        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _inventory = GenerateInventory().GetAwaiter().GetResult();
        }

        private async Task<IEnumerable<Inventory>> GenerateInventory()
        {
            return new List<Inventory>();
        }

        public IEnumerable<Inventory> GetInventory()
            => _inventory;
    }
}
