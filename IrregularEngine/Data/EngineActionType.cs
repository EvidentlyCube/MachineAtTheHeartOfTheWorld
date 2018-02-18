// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

namespace IrregularMachine.IrregularEngine.Data {
    public enum EngineActionType {
        Special,
        MoveN,
        MoveNE,
        MoveE,
        MoveSE,
        MoveS,
        MoveSW,
        MoveW,
        MoveNW
    }
    
    public static class EngineActionTypeUtils {
        public static List<Tuple<EngineActionType, int>> GetSquashedActions(List<EngineActionType> actions) {
            var list = new List<Tuple<EngineActionType, int>>();

            EngineActionType? lastType = null;
            int count = 0;
            foreach (var actionType in actions) {
                if (lastType.HasValue && lastType.Value != actionType) {
                    list.Add(new Tuple<EngineActionType, int>(lastType.Value, count));
                    lastType = actionType;
                    count = 0;
                } else if (!lastType.HasValue) {
                    lastType = actionType;
                }

                count++;
            }

            if (lastType.HasValue) {
                list.Add(new Tuple<EngineActionType, int>(lastType.Value, count));                
            }

            return list;
        }
    }
}