namespace WayfinderProject.Domain.Models.MoogleShop
{
    public class RecipeMaterial
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public Inventory Inventory { get; set; } = new();
    }
}