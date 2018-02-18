using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.BitmapTexts
{
    public static class BitmapFontLoaderBmFontText
    {
        public static BitmapFont LoadFont(FileInfo fileInfo, Texture2D texture)
        {
            return LoadFont(File.ReadAllText(fileInfo.FullName, Encoding.UTF8), texture, texture.Bounds);
        }

        private static BitmapFont LoadFont(string fontDataText, Texture2D texture, Rectangle textureBounds)
        {
            var lines = fontDataText.Split('\n');
            BitmapFont font = new BitmapFont(
                int.Parse(ExtractFirstString(lines, "common", "lineHeight"))
            );


            foreach (var line in ExtractLines(lines, "char"))
            {
                var character = (char)ExtractInt(line, "id");
                var x = ExtractInt(line, "x");
                var y = ExtractInt(line, "y");
                var width = ExtractInt(line, "width");
                var height = ExtractInt(line, "height");
                var offsetX = ExtractInt(line, "xoffset");
                var offsetY = ExtractInt(line, "yoffset");
                var advance = ExtractInt(line, "xadvance");

                var region = new TextureRegion2D(
                    texture,
                    textureBounds.X + x,
                    textureBounds.Y + y,
                    char.IsWhiteSpace(character) ? 0 : width,
                    height
                );

                font.AddCharacter(new BitmapFontCharacter(region, width, height, advance, new Point(offsetX, offsetY), character));
            }

            return font;
        }

        private static string ExtractFirstString(string[] lines, string lineName, string fieldName)
        {
            return ExtractString(ExtractFirstLine(lines, lineName), fieldName);
        }

        private static int ExtractInt(string line, string fieldName)
        {
            return int.Parse(ExtractString(line, fieldName));
        }
        private static string ExtractString(string line, string fieldName)
        {
            var match = Regex.Match(line, $"{fieldName}=\"?(.+?)\"? ");

            if (!match.Success || match.Groups.Count != 2)
            {
                match = Regex.Match(line, $"{fieldName}=(.+?) ");

                if (!match.Success || match.Groups.Count != 2)
                {
                    throw new ArgumentException($"Cannot find field {fieldName} in line {line}");
                }
            }

            return match.Groups[1].Value;
        }

        private static string ExtractFirstLine(IEnumerable<string> lines, string lineName)
        {
            return lines.First(x => x.StartsWith(lineName, StringComparison.InvariantCultureIgnoreCase));
        }

        private static string[] ExtractLines(string[] lines, string lineName)
        {
            return lines.Where(x => x.StartsWith(lineName+" ", StringComparison.InvariantCultureIgnoreCase)).ToArray();
        }
    }
}