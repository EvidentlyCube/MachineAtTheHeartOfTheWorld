using System.Collections.Generic;
using IrregularMachine.IrregularEngine;
using IrregularMachine.IrregularEngine.Serializer;

namespace IrregularMachine.Scenes.Ingame {
    public static class ScreensCollection {
        public static List<ScreenData> GetScreens() {
            return new List<ScreenData> {
                
                // Initial tutorial, teaching basics
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "a4 a8 a2 a6"
                    ),
                    new IngameScreenMessage("Let's try to decipher these symbols.", null)
                ),
                
                // Teaching that the same symbol can be entered multiple times
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "61"
                        + "a7 a7 a7 a3 a3 a5"
                    )
                ),
                
                // Teaching that multiple lines are read left-to-right up-to-down
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "a7 .. a8"
                        + "a7 a5 .."
                        + ".. .. a8"
                    )
                ),
                
                // NUMBERS
                
                // Teaching that number 1 exists
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "21"
                        + "d1 a5"
                    ),
                    
                    new IngameScreenMessage("A new symbol!", null),
                    new IngameScreenMessage("I think I saw it before, a number in an ancient script.", null)
                ),
                
                // Teaching that numbers are by default summed
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + ".. .. .. a7"
                        + ".. .. d1 a9"
                        + ".. d1 d1 a1"
                        + "d1 d1 d1 a3"
                    )
                ),
                
                // Teaching that spaces still sum numbers
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + "d1 .. d1 d1"
                        + ".. d1 .. d1"
                        + "d1 .. d1 .."
                        + "d1 d1 .. a5"
                    )
                ),
                
                // Introduce number 2
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "42"
                        + ".. d2 .. a8"
                        + "d1 d1 .. a2"
                    ),
                    new IngameScreenMessage("This one also looks like a number. If only I had pen and paper to take notes of what those symbols mean.", null)
                ),
                
                // Show that number 2 sums with itself
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d2 d2 d2 a6"
                    )
                ),
                
                // Show that number 2 sums with number 1
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "42"
                        + "d2 d1 .. a9"
                        + "d1 d2 d1 a6"
                    )
                ),
                
                // Show that number 2 sums with number 1 even with spaces
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d2 .. d1 .. a5"
                    )
                ),
                
                // Introduce number 4
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "62"
                        + ".. .. .. d4 .. a5"
                        + "d1 d1 d1 d1 .. a2"
                    ),
                    new IngameScreenMessage("I wonder if whatever made this mechanism was responsible for creation of human race and culture.", null),
                    new IngameScreenMessage("How else can I explain to myself the similarity between these signs and languages from our past.", null)
                ),
                
                // Number 4 sums with other numbers 
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d4 d1 d2 a1"
                    )
                ),
                
                
                // Preparation for #8
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + "d1 d1 d1 d1"
                        + "d1 d1 d1 d1"
                        + ".. .. .. .."
                        + ".. .. .. a6"
                    )
                ),
                
                // Preparation for #8
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + "d2 .. d2 .."
                        + "d2 .. d2 .."
                        + ".. .. .. .."
                        + ".. .. .. a4"
                    )
                ),
                
                // Preparation for #8
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + "d4 .. .. .."
                        + "d4 .. .. .."
                        + ".. .. .. .."
                        + ".. .. .. a8"
                    )
                ),
                
                // Preparation for #8
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + "d8 .. .. .."
                        + ".. .. .. .."
                        + ".. .. .. .."
                        + ".. .. .. a2"
                    ),
                    new IngameScreenMessage("Yet another number I presume.", null)
                ),
                
                // Multiply sign introduction
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d1 O* d8 a5"
                    ),
                    new IngameScreenMessage("It looks like a mathematical symbol of some kind. Is what it depicts a clue to what it means?", null)
                ),
                
                // Multiply sign more
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d1 O* d1 a4"
                    )
                ),
                
                // Multiply sign multiplies sums
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d1 d1 O* d1 a2"
                    )
                ),
                
                // Multiply sign multiplies sums even with spaces
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "61"
                        + "d2 .. O* .. d4 a8"
                    )
                ),
                
                // Multiply sign multiplies across lines
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "34"
                        + "d1 d1 d1"
                        + ".. O* .."
                        + "d1 .. d2"
                        + ".. a9 .."
                    )
                ),
                
            };
        }
    }
}