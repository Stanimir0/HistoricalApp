using System.Globalization;

namespace HistoricalApp.Helpers
{
    public class PeriodToColorConverter : IValueConverter, IMarkupExtension
    {
        public string TargetPeriod { get; set; } = string.Empty;
        public string SelectedColor { get; set; } = "#33f1c40f";
        public string UnselectedColor { get; set; } = "#1A1A1A";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string selectedPeriod && selectedPeriod.Equals(TargetPeriod, StringComparison.OrdinalIgnoreCase))
            {
                return Color.FromArgb(SelectedColor);
            }
            return Color.FromArgb(UnselectedColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class BoolToColorConverter : IValueConverter, IMarkupExtension
    {
        public string TrueColor { get; set; } = "#4CAF50";
        public string FalseColor { get; set; } = "#F44336";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return Color.FromArgb(boolValue ? TrueColor : FalseColor);
            }
            return Color.FromArgb(FalseColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class BoolToTextConverter : IValueConverter, IMarkupExtension
    {
        public string TrueText { get; set; } = "True";
        public string FalseText { get; set; } = "False";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueText : FalseText;
            }
            return FalseText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class InverseBoolConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return true;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    // Rarity to Color Converter (for shop item rarity tiers)
    public class RarityToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string rarity)
            {
                return rarity switch
                {
                    "Common" => Color.FromArgb("#9E9E9E"),    // Gray
                    "Rare" => Color.FromArgb("#2196F3"),      // Blue
                    "Epic" => Color.FromArgb("#9C27B0"),      // Purple
                    "Legendary" => Color.FromArgb("#FFD700"), // Gold
                    "Mythic" => Color.FromArgb("#F44336"),    // Red (future)
                    _ => Color.FromArgb("#9E9E9E")            // Default: Gray
                };
            }
            return Color.FromArgb("#9E9E9E");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
