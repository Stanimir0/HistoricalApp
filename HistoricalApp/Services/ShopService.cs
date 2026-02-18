using Firebase.Database;
using Firebase.Database.Query;
using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public class ShopService
    {
        private static List<ShopItem>? _shopItems;

        public static List<ShopItem> GetShopItems()
        {
            if (_shopItems == null)
            {
                InitializeShopItems();
            }
            return _shopItems!;
        }

        private static void InitializeShopItems()
        {
            _shopItems = new List<ShopItem>
            {
                // ============ BORDERS ============
                
                // Common Border
                new ShopItem
                {
                    Id = "border_simple",
                    Name = "Simple Border",
                    Description = "Clean and minimal border",
                    Price = 100,
                    IconEmoji = "⬜",
                    Category = "Border",
                    Rarity = "Common"
                },
                
                // Rare Borders
                new ShopItem
                {
                    Id = "border_gold",
                    Name = "Golden Border",
                    Description = "Elegant golden profile border",
                    Price = 300,
                    IconEmoji = "🥇",
                    Category = "Border",
                    Rarity = "Rare"
                },
                new ShopItem
                {
                    Id = "border_ancient",
                    Name = "Ancient Stone Border",
                    Description = "Carved stone aesthetic",
                    Price = 400,
                    IconEmoji = "🪨",
                    Category = "Border",
                    Rarity = "Rare"
                },
                
                // Epic Borders
                new ShopItem
                {
                    Id = "border_ice",
                    Name = "Ice Border",
                    Description = "Frosty ice crystal border",
                    Price = 600,
                    IconEmoji = "❄️",
                    Category = "Border",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "border_fire",
                    Name = "Fire Border",
                    Description = "Blazing fire effect border",
                    Price = 800,
                    IconEmoji = "🔥",
                    Category = "Border",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "border_royal",
                    Name = "Royal Border",
                    Description = "Fit for royalty",
                    Price = 1000,
                    IconEmoji = "👑",
                    Category = "Border",
                    Rarity = "Epic"
                },
                
                // Legendary Border
                new ShopItem
                {
                    Id = "border_diamond",
                    Name = "Diamond Border",
                    Description = "Sparkling diamond profile border",
                    Price = 2500,
                    IconEmoji = "💎",
                    Category = "Border",
                    Rarity = "Legendary"
                },

                // ============ BADGES ============
                
                // Common Badges
                new ShopItem
                {
                    Id = "badge_star",
                    Name = "Star Badge",
                    Description = "Shine bright like a star",
                    Price = 50,
                    IconEmoji = "⭐",
                    Category = "Badge",
                    Rarity = "Common"
                },
                new ShopItem
                {
                    Id = "badge_bronze",
                    Name = "Bronze Badge",
                    Description = "Your first achievement",
                    Price = 100,
                    IconEmoji = "🥉",
                    Category = "Badge",
                    Rarity = "Common"
                },
                new ShopItem
                {
                    Id = "badge_scholar",
                    Name = "Scholar Badge",
                    Description = "Show your knowledge",
                    Price = 150,
                    IconEmoji = "🎓",
                    Category = "Badge",
                    Rarity = "Common"
                },
                
                // Rare Badges
                new ShopItem
                {
                    Id = "badge_warrior",
                    Name = "Warrior Badge",
                    Description = "For the battle champions",
                    Price = 350,
                    IconEmoji = "⚔️",
                    Category = "Badge",
                    Rarity = "Rare"
                },
                new ShopItem
                {
                    Id = "badge_pharaoh",
                    Name = "Pharaoh Badge",
                    Description = "Rule like ancient kings",
                    Price = 250,
                    IconEmoji = "🏛️",
                    Category = "Badge",
                    Rarity = "Rare"
                },
                new ShopItem
                {
                    Id = "badge_knight",
                    Name = "Knight Badge",
                    Description = "Chivalry and honor",
                    Price = 300,
                    IconEmoji = "🛡️",
                    Category = "Badge",
                    Rarity = "Rare"
                },
                new ShopItem
                {
                    Id = "badge_explorer",
                    Name = "Explorer Badge",
                    Description = "Discover new worlds",
                    Price = 200,
                    IconEmoji = "🧭",
                    Category = "Badge",
                    Rarity = "Rare"
                },
                
                // Legendary Badge
                new ShopItem
                {
                    Id = "badge_crown",
                    Name = "Crown Badge",
                    Description = "Royal crown for the elite",
                    Price = 2000,
                    IconEmoji = "👑",
                    Category = "Badge",
                    Rarity = "Legendary"
                },

                // ============ FRAMES ============
                
                // Common Frames
                new ShopItem
                {
                    Id = "frame_wooden",
                    Name = "Wooden Frame",
                    Description = "Simple wooden avatar frame",
                    Price = 50,
                    IconEmoji = "🟫",
                    Category = "Frame",
                    Rarity = "Common"
                },
                new ShopItem
                {
                    Id = "frame_bronze",
                    Name = "Bronze Frame",
                    Description = "Classic bronze avatar frame",
                    Price = 100,
                    IconEmoji = "🟤",
                    Category = "Frame",
                    Rarity = "Common"
                },
                
                // Rare Frame
                new ShopItem
                {
                    Id = "frame_silver",
                    Name = "Silver Frame",
                    Description = "Sleek silver avatar frame",
                    Price = 300,
                    IconEmoji = "⚪",
                    Category = "Frame",
                    Rarity = "Rare"
                },
                
                // Epic Frame
                new ShopItem
                {
                    Id = "frame_rainbow",
                    Name = "Rainbow Frame",
                    Description = "Colorful rainbow avatar frame",
                    Price = 1200,
                    IconEmoji = "🌈",
                    Category = "Frame",
                    Rarity = "Epic"
                },

                // ============ THEMES (History-Related) ============
                
                // Rare Themes
                new ShopItem
                {
                    Id = "theme_simple",
                    Name = "Simple Theme",
                    Description = "Clean and minimal color scheme",
                    Price = 200,
                    IconEmoji = "⚫",
                    Category = "Theme",
                    Rarity = "Rare"
                },
                new ShopItem
                {
                    Id = "theme_ancient",
                    Name = "Ancient Egypt",
                    Description = "Pharaoh-inspired golden theme",
                    Price = 400,
                    IconEmoji = "🏛️",
                    Category = "Theme",
                    Rarity = "Rare"
                },
                new ShopItem
                {
                    Id = "theme_medieval",
                    Name = "Medieval Kingdom",
                    Description = "Castle and knight aesthetic",
                    Price = 500,
                    IconEmoji = "🏰",
                    Category = "Theme",
                    Rarity = "Rare"
                },
                
                // Epic Themes
                new ShopItem
                {
                    Id = "theme_sunset",
                    Name = "Sunset Theme",
                    Description = "Warm sunset color scheme",
                    Price = 650,
                    IconEmoji = "🌅",
                    Category = "Theme",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "theme_ocean",
                    Name = "Ocean Theme",
                    Description = "Cool ocean color scheme",
                    Price = 650,
                    IconEmoji = "🌊",
                    Category = "Theme",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "theme_forest",
                    Name = "Forest Theme",
                    Description = "Natural forest color scheme",
                    Price = 650,
                    IconEmoji = "🌲",
                    Category = "Theme",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "theme_renaissance",
                    Name = "Renaissance",
                    Description = "Art and enlightenment vibes",
                    Price = 700,
                    IconEmoji = "🎨",
                    Category = "Theme",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "theme_wartime",
                    Name = "Wartime Era",
                    Description = "Military camouflage aesthetic",
                    Price = 800,
                    IconEmoji = "🎖️",
                    Category = "Theme",
                    Rarity = "Epic"
                },
                
                // Legendary Theme
                new ShopItem
                {
                    Id = "theme_viking",
                    Name = "Viking Age",
                    Description = "Norse warrior theme",
                    Price = 1500,
                    IconEmoji = "⚔️",
                    Category = "Theme",
                    Rarity = "Legendary"
                },

                // ============ POWERUPS (Consumable Hints) ============
                new ShopItem
                {
                    Id = "hint_fifty_fifty",
                    Name = "50/50 Hint",
                    Description = "Removes 2 wrong answers",
                    Price = 500,
                    IconEmoji = "🎯",
                    Category = "Powerup",
                    Rarity = "Epic"
                },
                new ShopItem
                {
                    Id = "hint_double_points",
                    Name = "Double Points",
                    Description = "2x points for one quiz",
                    Price = 500,
                    IconEmoji = "✨",
                    Category = "Powerup",
                    Rarity = "Epic"
                }
            };
        }

        public static List<ShopItem> GetItemsByCategory(string category)
        {
            var items = GetShopItems();
            return items.Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static List<ShopItem> GetItemsByRarity(string rarity)
        {
            var items = GetShopItems();
            return items.Where(i => i.Rarity.Equals(rarity, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static List<string> GetCategories()
        {
            var items = GetShopItems();
            return items.Select(i => i.Category).Distinct().ToList();
        }

        public static List<string> GetRarities()
        {
            return new List<string> { "Common", "Rare", "Epic", "Legendary" };
        }
    }
}
