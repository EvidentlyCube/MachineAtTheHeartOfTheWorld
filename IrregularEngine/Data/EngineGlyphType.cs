// ReSharper disable InconsistentNaming

using System;

namespace IrregularMachine.IrregularEngine.Data {
    public enum EngineGlyphType {
        Nothing = 0,

        ActionSpecial = 10,
        ActionMoveN = 11,
        ActionMoveNE = 12,
        ActionMoveE = 13,
        ActionMoveSE = 14,
        ActionMoveS = 15,
        ActionMoveSW = 16,
        ActionMoveW = 17,
        ActionMoveNW = 18,

        NumeralOne = 20,    
        NumeralTwo = 21,
        NumeralFour = 22,
        NumeralEight = 23,

        SpecialCameleonN = 30,
        SpecialCameleonNE = 31,
        SpecialCameleonE = 32,
        SpecialCameleonSE = 33,
        SpecialCameleonS = 34,
        SpecialCameleonSW = 35,
        SpecialCameleonW = 36,
        SpecialCameleonNW = 37,

        OperationMultiplier = 40,
        OperationSubtract = 41,

        ModifierInvert = 50,
        ModifierIgnore = 51,

        SlicerAStart = 60,
        SlicerAEnd = 61,
        SlicerBStart = 62,
        SlicerBEnd = 63,
        SlicerCStart = 64,
        SlicerCEnd = 65,
        
        Bomb = 70
    }
    
    
    public static class EngineGlyphTypeExtension {
        public static bool IsMoveAction(this EngineGlyphType element) {
            return element == EngineGlyphType.ActionMoveN
                   || element == EngineGlyphType.ActionMoveNE
                   || element == EngineGlyphType.ActionMoveE
                   || element == EngineGlyphType.ActionMoveSE
                   || element == EngineGlyphType.ActionMoveS
                   || element == EngineGlyphType.ActionMoveSW
                   || element == EngineGlyphType.ActionMoveW
                   || element == EngineGlyphType.ActionMoveNW;
        }
        public static bool IsCameleon(this EngineGlyphType element) {
            return element == EngineGlyphType.SpecialCameleonN
                   || element == EngineGlyphType.SpecialCameleonNE
                   || element == EngineGlyphType.SpecialCameleonE
                   || element == EngineGlyphType.SpecialCameleonSE
                   || element == EngineGlyphType.SpecialCameleonS
                   || element == EngineGlyphType.SpecialCameleonSW
                   || element == EngineGlyphType.SpecialCameleonW
                   || element == EngineGlyphType.SpecialCameleonNW;
        }
        public static bool IsAction(this EngineGlyphType element) {
            return element.IsMoveAction() || element == EngineGlyphType.ActionSpecial;
        }
        
        public static bool IsBomb(this EngineGlyphType element) {
            return element == EngineGlyphType.Bomb;
        }
        
        public static bool IsNumeral(this EngineGlyphType element) {
            return element == EngineGlyphType.NumeralOne || element == EngineGlyphType.NumeralTwo || element == EngineGlyphType.NumeralFour || element == EngineGlyphType.NumeralEight;
        }
        
        public static bool IsModifier(this EngineGlyphType element) {
            return element == EngineGlyphType.ModifierIgnore || element == EngineGlyphType.ModifierInvert;
        }
        
        public static EngineDirection GetDirection(this EngineGlyphType element) {
            switch (element) {
                case EngineGlyphType.ActionMoveN:
                    return EngineDirection.North;
                case EngineGlyphType.ActionMoveNE:
                    return EngineDirection.NorthEast;
                case EngineGlyphType.ActionMoveE:
                    return EngineDirection.East;
                case EngineGlyphType.ActionMoveSE:
                    return EngineDirection.SouthEast;
                case EngineGlyphType.ActionMoveS:
                    return EngineDirection.South;
                case EngineGlyphType.ActionMoveSW:
                    return EngineDirection.SouthWest;
                case EngineGlyphType.ActionMoveW:
                    return EngineDirection.West;
                case EngineGlyphType.ActionMoveNW:
                    return EngineDirection.NorthWest;
                case EngineGlyphType.SpecialCameleonN:
                    return EngineDirection.North;
                case EngineGlyphType.SpecialCameleonNE:
                    return EngineDirection.NorthEast;
                case EngineGlyphType.SpecialCameleonE:
                    return EngineDirection.East;
                case EngineGlyphType.SpecialCameleonSE:
                    return EngineDirection.SouthEast;
                case EngineGlyphType.SpecialCameleonS:
                    return EngineDirection.South;
                case EngineGlyphType.SpecialCameleonSW:
                    return EngineDirection.SouthWest;
                case EngineGlyphType.SpecialCameleonW:
                    return EngineDirection.West;
                case EngineGlyphType.SpecialCameleonNW:
                    return EngineDirection.NorthWest;
                default:
                    throw new ArgumentOutOfRangeException(nameof(element), element, null);
            }
        }
    }
}