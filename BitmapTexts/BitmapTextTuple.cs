using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.BitmapTexts {
    public sealed class BitmapTextTuple {
        private static readonly List<BitmapTextTuple> Pool;

        static BitmapTextTuple() {
            Pool = new List<BitmapTextTuple>(1024);

            FeedPool(Pool.Capacity);
        }

        public static BitmapTextTuple GetOne(BitmapFontCharacter character, Vector2 localPosition, Color? color = null) {
            if (Pool.Count == 0) {
                FeedPool(64);
            }

            var lastIndex = Pool.Count - 1;
            var item = Pool[lastIndex];
            Pool.RemoveAt(lastIndex);

            item.FontCharacter = character;
            item.LocalPosition = localPosition;
            item.GlobalPosition = localPosition;
            item.Color = color ?? Color.White;

            return item;
        }

        private static void FeedPool(int count) {
            while (count-- > 0) {
                Pool.Add(new BitmapTextTuple());
            }
        }

        public BitmapFontCharacter FontCharacter;
        public Vector2 LocalPosition;
        public Vector2 GlobalPosition;
        public Color Color;

        public BitmapTextTuple() {
            Color = Color.White;
            LocalPosition = new Vector2(0, 0);
            GlobalPosition = new Vector2(0, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(SpriteBatch renderBatch) {
            renderBatch.Draw(FontCharacter.Texture, GlobalPosition, Color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(SpriteBatch renderBatch, float scale) {
            renderBatch.Draw(FontCharacter.Texture, GlobalPosition, Color, 0, Vector2.Zero, new Vector2(scale, scale), SpriteEffects.None, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReleaseToPool() {
            Pool.Add(this);
        }
    }
}