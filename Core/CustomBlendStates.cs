using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Core {
    public static class CustomBlendStates {
        public static readonly BlendState RegularWithAlpha;
        
        static CustomBlendStates() {
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