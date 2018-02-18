using System.Text;

namespace IrregularMachine.IrregularEngine.Serializer {
    public static class EngineTilesSerializer {
        public static string SerializeScreen(EngineScreen screen) {
            var result = new StringBuilder(screen.Width * screen.Height * 4);

            result.Append(screen.Width);
            result.Append(screen.Height);
            result.Append("\n");
            for (var y = 0; y < screen.Height; y++) {
                for (var x = 0; x < screen.Width; x++) {
                    var tileString = screen.Tiles[x, y];
                    result.Append(EngineSerializerGlyphs.GlyphToStringMap[tileString.Type]);
                    result.Append(" ");
                }

                result.Append("\n");
            }

            return result.ToString();
        }
    }
}