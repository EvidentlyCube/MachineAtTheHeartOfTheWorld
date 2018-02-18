using System;
using Microsoft.Xna.Framework.Audio;

namespace IrregularMachine.Scenes.Ingame {
    public class IngameScreenMessage {
        public string Text;
        public SoundEffect VoiceOver;

        public IngameScreenMessage(string text, SoundEffect voiceOver) {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            VoiceOver = voiceOver;
        }
    }
}