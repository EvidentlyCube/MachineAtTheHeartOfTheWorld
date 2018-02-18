using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.BitmapTexts
{
    public class BitmapFontCharacter
    {
        public readonly TextureRegion2D Texture;
        public readonly int Width;
        public readonly int Height;
        public readonly Point Offset;
        public readonly int Advance;
        public readonly char CharacterChar;

        public bool IsWhitespace => char.IsWhiteSpace(CharacterChar);

        public BitmapFontCharacter(TextureRegion2D texture, int width, int height, int advance, Point offset, int characterCode)
        {
            Texture = texture;
            Width = width;
            Height = height;
            Advance = advance;
            Offset = offset;
            CharacterChar = (char)characterCode;
        }

        public BitmapFontCharacter(TextureRegion2D texture, int width, int height, int advance, Point offset, char character)
            :this(texture, width, height, advance, offset, (int)character)
        {
        }
    }
}