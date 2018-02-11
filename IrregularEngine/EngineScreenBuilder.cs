using System;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine {
    public class EngineScreenBuilder {
        public static EngineScreenBuilder New(int width, int height) {
            return new EngineScreenBuilder(new EngineScreen(width, height));
        }

        private EngineScreen _screen;

        private EngineScreenBuilder(EngineScreen screen) {
            _screen = screen;
        }

        public EngineScreenBuilder SetTileType(int x, int y, EngineGlyphType type) {
            if (x < 0) throw new ArgumentOutOfRangeException(nameof(x), x, "Cannot be less than 0");
            if (y < 0) throw new ArgumentOutOfRangeException(nameof(y), y, "Cannot be less than 0");
            if (x >= _screen.Width) throw new ArgumentOutOfRangeException(nameof(x), x, $"Cannot be more than screen width ({_screen.Width})");
            if (y >= _screen.Height) throw new ArgumentOutOfRangeException(nameof(y), y, $"Cannot be more than screen height ({_screen.Height})");

            _screen.Tiles[x, y] = new EngineTile(type);

            return this;
        }

        public EngineScreen Finalize() {
            return _screen;
        }
    }
}