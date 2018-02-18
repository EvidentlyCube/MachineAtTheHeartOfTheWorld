using System;
using System.Collections.Generic;
using System.IO;
using IrregularMachine.BitmapTexts;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Core {
    public static class Gfx {
        public static Texture2D txt_InMemory { get; private set; }
        public static Texture2D bg_Backgorund { get; private set; }
        public static Texture2D sheet_font { get; private set; }
        public static Texture2D bg_InputFrame { get; private set; }
        public static Texture2D Controls { get; private set; }
        public static Texture2D Black { get; private set; }

        public static BitmapFont BitmapFont { get; private set; }
        
        public static TextureAtlas sheet_Ingame { get; private set; }
        public static TextureAtlas sheet_Particles { get; private set; }

        private static ContentManager _contentManager;

        public static Texture2D LoadIntroTexture(int index) {
            Logger.Init($"Loading intro texture '{index}'");
            return LoadTexture(_contentManager, $"intro_{index + 1}");
        }
        
        public static void Load(ContentManager content) {
            Logger.Init("Loading GFX assets");

            _contentManager = content;
            
            txt_InMemory = LoadTexture(content, "txt_InMemory");
            bg_Backgorund = LoadTexture(content, "bg_1");
            bg_InputFrame = LoadTexture(content, "bg_inputframe");
            Controls = LoadTexture(content, "Controls");
            Black = LoadTexture(content, "Black");
            sheet_font = LoadTexture(content, "sheet_font");
            BitmapFont = BitmapFontLoaderBmFontText.LoadFont(new FileInfo("Content/sheet_font.fnt"), sheet_font);
            sheet_Ingame = new TextureAtlas(
                LoadTexture(content, "sheet_glyphs"),
                File.ReadAllLines("Content/sheet_glyphs.txt")
            );
            sheet_Particles = new TextureAtlas(
                LoadTexture(content, "sheet_particles"),
                File.ReadAllLines("Content/sheet_particles.txt")
            );
        }

        private static Texture2D LoadTexture(ContentManager content, string assetName) {
            Logger.Init($"Loading texture: {assetName}");
            
            return content.Load<Texture2D>(assetName);
        }
    }
}