using System;
using System.Collections.Generic;
using System.IO;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Core {
    public static class Gfx {
        public static Texture2D txt_InMemory { get; private set; }
        public static Texture2D bg_Backgorund1 { get; private set; }
        public static Texture2D bg_Monitor { get; private set; }
        
        public static Dictionary<EngineGlyphType, Texture2D> GlyphTextures { get; private set; }
        public static void Load(GraphicsDevice graphicsDevice, ContentManager content) {
            Logger.Init("Loading GFX assets");
            txt_InMemory = LoadTexture(graphicsDevice, content, "txt_InMemory");
            bg_Backgorund1 = LoadTexture(graphicsDevice, content, "bg_1");
            bg_Monitor = LoadTexture(graphicsDevice, content, "bg_monitor");
            GlyphTextures = new Dictionary<EngineGlyphType, Texture2D>();
            
            foreach (EngineGlyphType value in Enum.GetValues(typeof(EngineGlyphType))) {
                if (value == EngineGlyphType.Nothing) {
                    continue;
                }

                var enumValue = (int) value;
                GlyphTextures[value] = LoadTexture(graphicsDevice, content, $"glyph_{enumValue}");
            }
        }

        private static Texture2D LoadTexture(GraphicsDevice graphicsDevice, ContentManager content, string assetName) {
            Logger.Init($"Loading texture: {assetName}");
            var pathsToCheck = new[] {
                Path.Combine(content.RootDirectory, assetName) + ".png",
                Path.Combine(content.RootDirectory, assetName) + ".jpg",
                Path.Combine(content.RootDirectory, assetName) + ".jpeg"
            };

            foreach (var path in pathsToCheck) {
                if (File.Exists(path)) {
                    try {
                        Logger.Init($"Trying to load texture from stream: {path}");
                        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                            return Texture2D.FromStream(graphicsDevice, stream);
                        }
                    }
                    catch (Exception e) {
                        Logger.Error($"Loading texture failed because of {e.Message}");
                    }
                }
            }

            Logger.Init($"Loading XNB texture");
            return content.Load<Texture2D>(assetName);
        }
    }
}