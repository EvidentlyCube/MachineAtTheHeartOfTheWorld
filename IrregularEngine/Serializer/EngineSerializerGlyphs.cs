using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IrregularMachine.IrregularEngine.Data;

namespace IrregularMachine.IrregularEngine.Serializer {
    public static class EngineSerializerGlyphs {
        public const string Nothing = "..";
        public const string ActionSpecial = "A5";
        public const string ActionMoveN = "A8";
        public const string ActionMoveNE = "A9";
        public const string ActionMoveE = "A6";
        public const string ActionMoveSE = "A3";
        public const string ActionMoveS = "A2";
        public const string ActionMoveSW = "A1";
        public const string ActionMoveW = "A4";
        public const string ActionMoveNW = "A7";
        public const string NumeralOne = "D1";
        public const string NumeralTwo = "D2";
        public const string NumeralFour = "D4";
        public const string NumeralEight = "D8";
        public const string SpecialCameleonN = "C8";
        public const string SpecialCameleonNE = "C9";
        public const string SpecialCameleonE = "C6";
        public const string SpecialCameleonSE = "C3";
        public const string SpecialCameleonS = "C2";
        public const string SpecialCameleonSW = "C1";
        public const string SpecialCameleonW = "C4";
        public const string SpecialCameleonNW = "C7";
        public const string OperationMultiplier = "O*";
        public const string OperationSubtract = "O-";
        public const string ModifierInvert = "O!";
        public const string ModifierIgnore = "OI";
        public const string SlicerAStart = "(1";
        public const string SlicerAEnd = "1)";
        public const string SlicerBStart = "(2";
        public const string SlicerBEnd = "2)";
        public const string SlicerCStart = "(3";
        public const string SlicerCEnd = "3)";
        public const string BombRange1 = "B1";
        public const string BombRange2 = "B2";
        public const string BombRange3 = "B3";

        public static readonly IReadOnlyDictionary<string, EngineGlyphType> StringToGlyphMap;
        public static readonly IReadOnlyDictionary<EngineGlyphType, string> GlyphToStringMap;

        static EngineSerializerGlyphs() {
            var glyphToStringMap = new Dictionary<EngineGlyphType, String>();
            var stringToGlyphMap = new Dictionary<string, EngineGlyphType>();
            stringToGlyphMap[Nothing] = EngineGlyphType.Nothing;
            stringToGlyphMap[ActionSpecial] = EngineGlyphType.ActionSpecial;
            stringToGlyphMap[ActionMoveN] = EngineGlyphType.ActionMoveN;
            stringToGlyphMap[ActionMoveNE] = EngineGlyphType.ActionMoveNE;
            stringToGlyphMap[ActionMoveE] = EngineGlyphType.ActionMoveE;
            stringToGlyphMap[ActionMoveSE] = EngineGlyphType.ActionMoveSE;
            stringToGlyphMap[ActionMoveS] = EngineGlyphType.ActionMoveS;
            stringToGlyphMap[ActionMoveSW] = EngineGlyphType.ActionMoveSW;
            stringToGlyphMap[ActionMoveW] = EngineGlyphType.ActionMoveW;
            stringToGlyphMap[ActionMoveNW] = EngineGlyphType.ActionMoveNW;
            stringToGlyphMap[NumeralOne] = EngineGlyphType.NumeralOne;
            stringToGlyphMap[NumeralTwo] = EngineGlyphType.NumeralTwo;
            stringToGlyphMap[NumeralFour] = EngineGlyphType.NumeralFour;
            stringToGlyphMap[NumeralEight] = EngineGlyphType.NumeralEight;
            stringToGlyphMap[SpecialCameleonN] = EngineGlyphType.SpecialCameleonN;
            stringToGlyphMap[SpecialCameleonNE] = EngineGlyphType.SpecialCameleonNE;
            stringToGlyphMap[SpecialCameleonE] = EngineGlyphType.SpecialCameleonE;
            stringToGlyphMap[SpecialCameleonSE] = EngineGlyphType.SpecialCameleonSE;
            stringToGlyphMap[SpecialCameleonS] = EngineGlyphType.SpecialCameleonS;
            stringToGlyphMap[SpecialCameleonSW] = EngineGlyphType.SpecialCameleonSW;
            stringToGlyphMap[SpecialCameleonW] = EngineGlyphType.SpecialCameleonW;
            stringToGlyphMap[SpecialCameleonNW] = EngineGlyphType.SpecialCameleonNW;
            stringToGlyphMap[OperationMultiplier] = EngineGlyphType.OperationMultiplier;
            stringToGlyphMap[OperationSubtract] = EngineGlyphType.OperationSubtract;
            stringToGlyphMap[ModifierInvert] = EngineGlyphType.ModifierInvert;
            stringToGlyphMap[ModifierIgnore] = EngineGlyphType.ModifierIgnore;
            stringToGlyphMap[SlicerAStart] = EngineGlyphType.SlicerAStart;
            stringToGlyphMap[SlicerAEnd] = EngineGlyphType.SlicerAEnd;
            stringToGlyphMap[SlicerBStart] = EngineGlyphType.SlicerBStart;
            stringToGlyphMap[SlicerBEnd] = EngineGlyphType.SlicerBEnd;
            stringToGlyphMap[SlicerCStart] = EngineGlyphType.SlicerCStart;
            stringToGlyphMap[SlicerCEnd] = EngineGlyphType.SlicerCEnd;
            stringToGlyphMap[BombRange1] = EngineGlyphType.BombRange1;
            stringToGlyphMap[BombRange2] = EngineGlyphType.BombRange2;
            stringToGlyphMap[BombRange3] = EngineGlyphType.BombRange3;
            
            foreach (var key in stringToGlyphMap.Keys) {
                var glyph = stringToGlyphMap[key];
                glyphToStringMap[glyph] = key;
            }
            
            StringToGlyphMap = stringToGlyphMap;
            GlyphToStringMap = glyphToStringMap;
        }
    }
}