using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine {
    public class EngineScreen {
        public readonly EngineTile[,] Tiles;
        public readonly int Width;
        public readonly int Height;
        

        public EngineScreen(int width, int height) {
            Width = width;
            Height = height;
            Tiles = new EngineTile[Width, Height];
            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {
                    Tiles[x,y] = new EngineTile(EngineGlyphType.Nothing);
                }
            }
        }
    }
}