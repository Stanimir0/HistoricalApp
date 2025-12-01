using Microsoft.Maui.Controls;

namespace HistoricalApp.Helpers
{
    public static class AnimationHelper
    {
        public static async Task AnimateButtonPress(View view)
        {
            if (view == null) return;

            await view.ScaleTo(0.96, 70, Easing.CubicOut);
            await view.ScaleTo(1, 70, Easing.CubicIn);
        }

        public static async Task FadeIn(View root)
        {
            if (root == null) return;

            root.Opacity = 0;
            await root.FadeTo(1, 250, Easing.CubicIn);
        }

        public static async Task SlideUpFadeIn(View root)
        {
            if (root == null) return;

            root.Opacity = 0;
            root.TranslationY = 30;

            await Task.WhenAll(
                root.FadeTo(1, 250, Easing.CubicOut),
                root.TranslateTo(0, 0, 250, Easing.CubicOut)
            );
        }

        public static async Task Shake(View view)
        {
            if (view == null) return;

            uint speed = 50;

            await view.TranslateTo(-10, 0, speed);
            await view.TranslateTo(10, 0, speed);
            await view.TranslateTo(-8, 0, speed);
            await view.TranslateTo(8, 0, speed);
            await view.TranslateTo(0, 0, speed);
        }

        public static async Task GlowCorrect(View view)
        {
            if (view == null) return;

            var originalColor = view.BackgroundColor;

            view.BackgroundColor = Color.FromArgb("#2ecc71"); // green
            await Task.Delay(350);
            view.BackgroundColor = originalColor;
        }

        public static async Task GlowWrong(View view)
        {
            if (view == null) return;

            var originalColor = view.BackgroundColor;

            view.BackgroundColor = Color.FromArgb("#e74c3c"); // red
            await Task.Delay(350);
            view.BackgroundColor = originalColor;
        }
    }
}
