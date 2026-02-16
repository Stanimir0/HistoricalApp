namespace HistoricalApp.Models
{
    public class ShopItem
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public string IconEmoji { get; set; } = "🎨";
        public string Category { get; set; } = "Cosmetic";
        public string Rarity { get; set; } = "Common"; // Common, Rare, Epic, Legendary
        public bool IsPurchased { get; set; } = false;
    }
}
