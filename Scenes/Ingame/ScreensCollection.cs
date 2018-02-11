using System.Collections.Generic;
using IrregularMachine.IrregularEngine;
using IrregularMachine.IrregularEngine.Serializer;

namespace IrregularMachine.Scenes.Ingame {
    public static class ScreensCollection {
        public static List<EngineScreen> GetScreens() {
            return new List<EngineScreen> {
                EngineTilesUnserializer.UnserializeScreen(
                    "97"
                    + "a7 a8 a9 .. c7 c8 c9 .. d1"
                    + "a4 a5 a6 .. c4 .. c6 .. d2"
                    + "a1 a2 a3 .. c1 c2 c3 .. d4"
                    + ".. .. .. .. .. .. .. .. d8"
                    + "O* O- O! OI (1 1) .. .. b1"
                    + ".. .. .. .. (2 2) .. .. b2"
                    + ".. .. .. .. (3 3) .. .. b3"
                ),
                
                EngineTilesUnserializer.UnserializeScreen(
                    "53"
                    + "d1 d4 .. o* .."
                    + ".. d4 d2 d1 .."
                    + ".. .. OI A4 A5"
                ),
                
                EngineTilesUnserializer.UnserializeScreen(
                    "91"
                    + "a4 .. .. .. a5 .. .. .. a6"
                ),
                EngineTilesUnserializer.UnserializeScreen(
                    "19"
                    + "a8"
                    + ".."
                    + ".."
                    + ".."
                    + "a5"
                    + ".."
                    + ".."
                    + ".."
                    + "a2"
                ),
                
                EngineTilesUnserializer.UnserializeScreen(
                    "99"
                    + "a7 .. a8 .. a8 .. a8 .. a9"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. a5 .. .. .. .. .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. .. .. a5 .. .. .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. .. .. .. .. a5 .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a1 .. a2 .. a2 .. a2 .. a3"
                ),
                
                EngineTilesUnserializer.UnserializeScreen(
                    "98"
                    + "a7 .. a8 .. a8 .. a8 .. a9"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. a5 .. .. .. .. .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. .. .. a5 .. .. .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. .. .. .. .. a5 .. a6"
                    + "a1 .. a2 .. a2 .. a2 .. a3"
                ),
                
                EngineTilesUnserializer.UnserializeScreen(
                    "97"
                    + "a7 .. a8 .. a8 .. a8 .. a9"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. a5 .. .. .. .. .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a4 .. .. .. a5 .. .. .. a6"
                    + ".. .. .. .. .. .. .. .. .."
                    + "a1 .. a2 .. a2 .. a2 .. a3"
                ),
                
                EngineTilesUnserializer.UnserializeScreen(
                    "22"
                    + "a6 a5"
                    + "(1 c2"
                ),
                
            };
        }
    }
}