
using HistoricalApp.Models;

namespace HistoricalApp.Services
{
    public class ThemeService
    {
        private static ThemeService _instance;
        public static ThemeService Instance => _instance ??= new ThemeService();

        private readonly Dictionary<string, ThemePalette> _themes = new();

        public ThemeService()
        {
            InitializeThemes();
        }

        public void ApplyTheme(string themeId)
        {
            if (string.IsNullOrEmpty(themeId) || !_themes.ContainsKey(themeId))
            {
                // Revert to default
                ApplyDefaultTheme();
                return;
            }

            var palette = _themes[themeId];
            ApplyPalette(palette);
        }

        private void ApplyPalette(ThemePalette palette)
        {
            var res = Application.Current.Resources;

            if (palette.Primary != null) res["Primary"] = palette.Primary;
            if (palette.PrimaryDark != null) res["PrimaryDark"] = palette.PrimaryDark;
            if (palette.PrimaryLight != null) res["PrimaryLight"] = palette.PrimaryLight;
            
            if (palette.PrimaryGradientStart != null) res["PrimaryGradientStart"] = palette.PrimaryGradientStart;
            if (palette.PrimaryGradientEnd != null) res["PrimaryGradientEnd"] = palette.PrimaryGradientEnd;

            if (palette.BackgroundMain != null) res["BackgroundMain"] = palette.BackgroundMain;
            if (palette.BackgroundGradientStart != null) res["BackgroundGradientStart"] = palette.BackgroundGradientStart;
            if (palette.BackgroundGradientEnd != null) res["BackgroundGradientEnd"] = palette.BackgroundGradientEnd;

            if (palette.GlassSurface != null) res["GlassSurface"] = palette.GlassSurface;
            if (palette.GlassBorder != null) res["GlassBorder"] = palette.GlassBorder;
        }

        private void ApplyDefaultTheme()
        {
            // Reset to default Midnight Gold (values from Colors.xaml)
            var res = Application.Current.Resources;
            
            res["Primary"] = Color.FromArgb("#D4AF37");
            res["PrimaryDark"] = Color.FromArgb("#B4942D");
            res["PrimaryLight"] = Color.FromArgb("#F3E5AB");
            
            res["PrimaryGradientStart"] = Color.FromArgb("#D4AF37");
            res["PrimaryGradientEnd"] = Color.FromArgb("#8A6E0F");
            
            res["BackgroundMain"] = Color.FromArgb("#05080F");
            res["BackgroundGradientStart"] = Color.FromArgb("#0F172A");
            res["BackgroundGradientEnd"] = Color.FromArgb("#020617");
            
            res["GlassSurface"] = Color.FromArgb("#1AFFFFFF");
            res["GlassBorder"] = Color.FromArgb("#33FFFFFF");
        }

        private void InitializeThemes()
        {
            // Ancient Egypt (Gold/Sand/Lapis)
            _themes.Add("theme_ancient", new ThemePalette
            {
                Primary = Color.FromArgb("#E6C200"), // Bright Gold
                PrimaryDark = Color.FromArgb("#B39800"),
                PrimaryLight = Color.FromArgb("#FFF0a3"),
                
                PrimaryGradientStart = Color.FromArgb("#E6C200"),
                PrimaryGradientEnd = Color.FromArgb("#C49000"),
                
                BackgroundMain = Color.FromArgb("#1A1408"), // Dark Sand
                BackgroundGradientStart = Color.FromArgb("#2C220E"),
                BackgroundGradientEnd = Color.FromArgb("#140F05"),

                GlassSurface = Color.FromArgb("#26E6C200"), // Gold Tint
                GlassBorder = Color.FromArgb("#4DE6C200")
            });

            // Medieval (Silver/Stone/Red)
            _themes.Add("theme_medieval", new ThemePalette
            {
                Primary = Color.FromArgb("#C0C0C0"), // Silver
                PrimaryDark = Color.FromArgb("#808080"),
                PrimaryLight = Color.FromArgb("#E0E0E0"),
                
                PrimaryGradientStart = Color.FromArgb("#C0C0C0"),
                PrimaryGradientEnd = Color.FromArgb("#757575"),
                
                BackgroundMain = Color.FromArgb("#1C1C1C"), // Dark Stone
                BackgroundGradientStart = Color.FromArgb("#2E2E2E"),
                BackgroundGradientEnd = Color.FromArgb("#0F0F0F"),

                GlassSurface = Color.FromArgb("#1AFFFFFF"),
                GlassBorder = Color.FromArgb("#33C0C0C0")
            });

            // Ocean (Blue/Teal)
            _themes.Add("theme_ocean", new ThemePalette
            {
                Primary = Color.FromArgb("#00E5FF"), // Cyan
                PrimaryDark = Color.FromArgb("#00B8D4"),
                PrimaryLight = Color.FromArgb("#84FFFF"),
                
                PrimaryGradientStart = Color.FromArgb("#00E5FF"),
                PrimaryGradientEnd = Color.FromArgb("#0097A7"),
                
                BackgroundMain = Color.FromArgb("#001014"), // Deep Sea
                BackgroundGradientStart = Color.FromArgb("#00252E"),
                BackgroundGradientEnd = Color.FromArgb("#00080A"),

                GlassSurface = Color.FromArgb("#1A00E5FF"),
                GlassBorder = Color.FromArgb("#3300E5FF")
            });

            // Forest (Green/Emerald)
            _themes.Add("theme_forest", new ThemePalette
            {
                Primary = Color.FromArgb("#00E676"), // Bright Green
                PrimaryDark = Color.FromArgb("#00C853"),
                PrimaryLight = Color.FromArgb("#69F0AE"),
                
                PrimaryGradientStart = Color.FromArgb("#00E676"),
                PrimaryGradientEnd = Color.FromArgb("#00A040"),
                
                BackgroundMain = Color.FromArgb("#051408"), // Dark Forest
                BackgroundGradientStart = Color.FromArgb("#0F2E16"),
                BackgroundGradientEnd = Color.FromArgb("#020A04"),

                GlassSurface = Color.FromArgb("#1A00E676"),
                GlassBorder = Color.FromArgb("#3300E676")
            });
            
             // Sunset (Orange/Pink)
            _themes.Add("theme_sunset", new ThemePalette
            {
                Primary = Color.FromArgb("#FF4081"), // Pink
                PrimaryDark = Color.FromArgb("#F50057"),
                PrimaryLight = Color.FromArgb("#FF80AB"),
                
                PrimaryGradientStart = Color.FromArgb("#FF9100"), // Orange
                PrimaryGradientEnd = Color.FromArgb("#C51162"),   // Pink
                
                BackgroundMain = Color.FromArgb("#1A050B"), // Dark Warm
                BackgroundGradientStart = Color.FromArgb("#2E0F16"),
                BackgroundGradientEnd = Color.FromArgb("#0F0205"),

                GlassSurface = Color.FromArgb("#1AFF4081"),
                GlassBorder = Color.FromArgb("#33FF9100")
            });

            // Viking (Ice Blue/Grey/Fur)
            _themes.Add("theme_viking", new ThemePalette
            {
                Primary = Color.FromArgb("#81D4FA"), // Ice Blue
                PrimaryDark = Color.FromArgb("#29B6F6"),
                PrimaryLight = Color.FromArgb("#B3E5FC"),
                
                PrimaryGradientStart = Color.FromArgb("#81D4FA"),
                PrimaryGradientEnd = Color.FromArgb("#4FC3F7"),
                
                BackgroundMain = Color.FromArgb("#111822"), // Nordic Night
                BackgroundGradientStart = Color.FromArgb("#1F2C3D"),
                BackgroundGradientEnd = Color.FromArgb("#090C11"),

                GlassSurface = Color.FromArgb("#1A81D4FA"),
                GlassBorder = Color.FromArgb("#3381D4FA")
            });
            
            // Wartime (Olive/Camo)
            _themes.Add("theme_wartime", new ThemePalette
            {
                Primary = Color.FromArgb("#8BC34A"), // Light Olive
                PrimaryDark = Color.FromArgb("#689F38"),
                PrimaryLight = Color.FromArgb("#DCEDC8"),
                
                PrimaryGradientStart = Color.FromArgb("#8BC34A"),
                PrimaryGradientEnd = Color.FromArgb("#558B2F"),
                
                BackgroundMain = Color.FromArgb("#141810"), // Dark Camo
                BackgroundGradientStart = Color.FromArgb("#22281C"),
                BackgroundGradientEnd = Color.FromArgb("#0A0C08"),

                GlassSurface = Color.FromArgb("#1A8BC34A"),
                GlassBorder = Color.FromArgb("#338BC34A")
            });
            
             // Renaissance (Purple/Gold)
            _themes.Add("theme_renaissance", new ThemePalette
            {
                Primary = Color.FromArgb("#EA80FC"), // Light Purple
                PrimaryDark = Color.FromArgb("#AA00FF"),
                PrimaryLight = Color.FromArgb("#F3E5AB"), // Gold accent
                
                PrimaryGradientStart = Color.FromArgb("#D500F9"),
                PrimaryGradientEnd = Color.FromArgb("#4A148C"),
                
                BackgroundMain = Color.FromArgb("#150A1A"), // Deep Purple
                BackgroundGradientStart = Color.FromArgb("#291530"),
                BackgroundGradientEnd = Color.FromArgb("#0A040D"),

                GlassSurface = Color.FromArgb("#1AEA80FC"),
                GlassBorder = Color.FromArgb("#33D500F9")
            });
            
             // Simple (Monochrome)
            _themes.Add("theme_simple", new ThemePalette
            {
                Primary = Color.FromArgb("#FFFFFF"), 
                PrimaryDark = Color.FromArgb("#CCCCCC"),
                PrimaryLight = Color.FromArgb("#EEEEEE"),
                
                PrimaryGradientStart = Color.FromArgb("#FFFFFF"),
                PrimaryGradientEnd = Color.FromArgb("#AAAAAA"),
                
                BackgroundMain = Color.FromArgb("#121212"),
                BackgroundGradientStart = Color.FromArgb("#1E1E1E"),
                BackgroundGradientEnd = Color.FromArgb("#000000"),

                GlassSurface = Color.FromArgb("#1AFFFFFF"),
                GlassBorder = Color.FromArgb("#33FFFFFF")
            });
        }

        private class ThemePalette
        {
            public Color Primary { get; set; }
            public Color PrimaryDark { get; set; }
            public Color PrimaryLight { get; set; }
            public Color PrimaryGradientStart { get; set; }
            public Color PrimaryGradientEnd { get; set; }
            public Color BackgroundMain { get; set; }
            public Color BackgroundGradientStart { get; set; }
            public Color BackgroundGradientEnd { get; set; }
            public Color GlassSurface { get; set; }
            public Color GlassBorder { get; set; }
        }
    }
}
