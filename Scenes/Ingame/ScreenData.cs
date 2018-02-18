using System;
using IrregularMachine.IrregularEngine;

namespace IrregularMachine.Scenes.Ingame {
    public class ScreenData {
        public readonly EngineScreen EgineScreen;
        public readonly IngameScreenMessage[] Messages;

        public ScreenData(EngineScreen egineScreen, params IngameScreenMessage[] messages) {
            EgineScreen = egineScreen ?? throw new ArgumentNullException(nameof(egineScreen));
            Messages = messages;
        }
    }
}