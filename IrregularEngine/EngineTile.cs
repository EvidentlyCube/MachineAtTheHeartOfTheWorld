using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine {
    public class EngineTile {
        public readonly EngineGlyphType Type;
        public int TimesAccessed;

        public EngineTile(EngineGlyphType type) {
            Type = type;
        }

        public EngineTile Clone() {
            return new EngineTile(Type) {
                TimesAccessed = TimesAccessed
            };
        }
    }
}