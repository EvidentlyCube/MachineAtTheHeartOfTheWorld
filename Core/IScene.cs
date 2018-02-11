using Microsoft.Xna.Framework.Graphics;

namespace IrregularMachine.Core {
    public interface IScene {
        void Update();
        void Draw(SpriteBatch batch);

        void OnSet();
        void OnUnset();
    }
}