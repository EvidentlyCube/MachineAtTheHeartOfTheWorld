using Microsoft.Xna.Framework;

namespace IrregularMachine.Extensions {
    public static class RectangleExtension {
        public static float GetRatio(this Rectangle rect) {
            return (float) rect.Width / rect.Height;
        }
    }
}