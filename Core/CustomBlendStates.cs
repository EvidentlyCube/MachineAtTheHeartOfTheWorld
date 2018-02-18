using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Core {
    public static class CustomBlendStates {
        public static readonly BlendState Multiply;
        public static readonly BlendState RegularWithAlpha;
        
        static CustomBlendStates() {
            Multiply = new BlendState {
                ColorSourceBlend = Blend.SourceAlpha,
                ColorDestinationBlend = Blend.One,
                ColorBlendFunction = BlendFunction.ReverseSubtract,
                AlphaSourceBlend = Blend.SourceAlpha,
                AlphaDestinationBlend = Blend.One,
                AlphaBlendFunction = BlendFunction.ReverseSubtract
            };

            RegularWithAlpha = new BlendState {
                ColorBlendFunction = BlendFunction.Add,
                AlphaBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.SourceAlpha,
                ColorDestinationBlend = Blend.InverseSourceAlpha,
                AlphaSourceBlend = Blend.One,
                AlphaDestinationBlend = Blend.InverseSourceAlpha
            };
        }
    }
}