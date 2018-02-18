using Microsoft.Xna.Framework;

namespace IrregularMachine.Core {
    public static class S {
        public const int ViewportWidth = 1920;
        public const int ViewportHeight = 1080;
        public const float ViewportRatio = (float)ViewportWidth / ViewportHeight;
        public const int GlyphAreaWidth = 864;
        public const int GlyphAreaHeight = 720;
        public const int GlyphAreaMonitorOffsetX = 531;
        public const int GlyphAreaMonitorOffsetY = 124;

        public const int InputFrameX = 491;
        public const int InputFrameY = 872;

        public const int ActionGlassLeftmostX = 528;
        public const int ActionGlassLeftmostY = 903;
        public const int ActionGlassSpacing = 6;

        public static readonly Rectangle ActionInputClippingRectangle =
            new Rectangle(InputFrameX + 10, InputFrameY + 10, 916, 141);

        public const int GlyphImageEdge = 144;
        public const int GlyphCogSize = 188;
        public const int GlyphCogOffset = (GlyphCogSize - GlyphImageEdge) / 2;

        public const int MaxActionsInLevel = 9;
    }
}