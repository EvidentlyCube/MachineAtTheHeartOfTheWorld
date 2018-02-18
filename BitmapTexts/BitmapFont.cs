using System.Collections.Generic;
using System.Linq;

namespace IrregularMachine.BitmapTexts {
    public sealed class BitmapFont {
        private readonly Dictionary<char, BitmapFontCharacter> _characters;

        public readonly int LineHeight;

        public BitmapFont(int lineHeight) {
            LineHeight = lineHeight;
            _characters = new Dictionary<char, BitmapFontCharacter>();
        }

        public bool HasCharacter(char character) {
            return _characters.ContainsKey(character);
        }

        public BitmapFontCharacter GetCharacter(char character) {
            return _characters.ContainsKey(character) ? _characters[character] : null;
        }

        public void AddCharacter(BitmapFontCharacter bitmapFontCharacter) {
            _characters.Add(bitmapFontCharacter.CharacterChar, bitmapFontCharacter);
        }
    }
}