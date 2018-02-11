using System.Diagnostics;
using System.Text.RegularExpressions;

namespace IrregularMachine.IrregularEngine.Serializer {
    public static class EngineTilesUnserializer {
        public static EngineScreen UnserializeScreen(string levelString) {
            var cleanRegexp = new Regex("\\s");

            var width = int.Parse(levelString[0].ToString());
            var height = int.Parse(levelString[1].ToString());
            var tilesCount = width * height;

            levelString = cleanRegexp.Replace(levelString.Substring(2), "").ToUpper();
            Debug.Assert(levelString.Length == tilesCount * 2,
                $"Level string should be exactly '{tilesCount * 2}' characters long but is '{levelString.Length}'");

            var screenBuilder = EngineScreenBuilder.New(width, height);

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    var tileString = levelString.Substring(x * 2 + y * width * 2, 2);
                    Debug.Assert(
                        EngineSerializerGlyphs.StringToGlyphMap.ContainsKey(tileString),
                        $"Level contains unknown glyph '{tileString}'"
                    );

                    var glyph = EngineSerializerGlyphs.StringToGlyphMap[tileString];
                    screenBuilder.SetTileType(x, y, glyph);
                }
            }

            return screenBuilder.Finalize();
        }
    }
}