using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Core {
    public class TextureAtlas {
        private Dictionary<string, TextureRegion2D> _textureMap;

        public TextureRegion2D this[string name] => _textureMap[name];
        
        public TextureAtlas(Texture2D texture, string[] mappingRows) {
            _textureMap = new Dictionary<string, TextureRegion2D>(mappingRows.Length);
            
            foreach (var mappingRow in mappingRows) {
                var pieces = mappingRow.Split('|');
                if (pieces.Length != 5) {
                    throw new ArgumentException($"Mappings contains row that does not have 5 elements '{mappingRow}'", nameof(mappingRows));
                }
                
                _textureMap[pieces[0]] = new TextureRegion2D(
                    texture,
                    int.Parse(pieces[1]),
                    int.Parse(pieces[2]),
                    int.Parse(pieces[3]),
                    int.Parse(pieces[4])
                );
            }
        }
    }
}