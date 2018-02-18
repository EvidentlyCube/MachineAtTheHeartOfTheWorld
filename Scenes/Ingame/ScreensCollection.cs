using System.Collections.Generic;
using IrregularMachine.Core;
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
                    new IngameScreenMessage("Let's try to decipher these symbols.", Sfx.VoiceOvers["voice_ingame1a"]),
                    new IngameScreenMessage("Looking at the tablets I get the impression that they are control panels of some sort, perhaps the glyphs are instructions.", Sfx.VoiceOvers["voice_ingame1b"])
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
                    
                    new IngameScreenMessage("This symbol... Isn't it a number one in Gujarati, an Indian language?", Sfx.VoiceOvers["voice_ingame2a"]),
                    new IngameScreenMessage("I wish I could have access to a decent library right now, it would make it easy to verify.", Sfx.VoiceOvers["voice_ingame2b"])
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
                    )
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
                    )
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
                    )
                ),
                
                // MATH SIGNS
                
                // Multiply sign introduction
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d1 O* d8 a5"
                    ),
                    new IngameScreenMessage("This doesn't look like any number I have ever seen but it certainly seems familiar.", Sfx.VoiceOvers["voice_ingame3a"]),
                    new IngameScreenMessage("Perhaps it's not a number at all.", Sfx.VoiceOvers["voice_ingame3b"]),
                    new IngameScreenMessage("All of those similarities... Could it be that whoever made this machine was responsible for the creation of human culture?", Sfx.VoiceOvers["voice_ingame3c"])
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
                
                // Subtraction sign
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d8 o- d1 a5"
                    ),
                    new IngameScreenMessage("Judging by the shape of the cog and the plate I can assume it's another mathematical symbol.", Sfx.VoiceOvers["voice_ingame4a"]),
                    new IngameScreenMessage("Not that it tells me exactly what it does, but it's a clue all right.", Sfx.VoiceOvers["voice_ingame4b"])
                ),
                
                // Multiple subtraction signs
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "32"
                        + "d8 o- d2"
                        + "o- d4 a1"
                    )
                ),
                
                // Summing in the middle of subtraction - right half
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d8 o- d2 d1 a8"
                    )
                ),
                
                // Summing in the middle of subtraction - left half
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d8 d2 o- d4 a8"
                    )
                ),
                
                // Summing in the middle of subtraction - complex scenario
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "43"
                        + "d8 d8 d4 o-"
                        + "d2 d4 d2 d1"
                        + "d1 d2 d1 a3"
                    )
                ),
                
                // Multiple subtractions
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "d8 d2 o-"
                        + "d4 o- d2"
                        + "o- d1 a3"
                    )
                ),
                
                // Multiply and subtract
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "d4 o* d4"
                        + ".. o- d8"
                        + ".. .. a8"
                    )
                ),
                
                // Left to right operation order
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "52"
                        + "d8 o- d4 o* d2"
                        + ".. .. a8 .. .."
                    )
                ),
                
                // Left to right operation order with summing
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "d8 d2 o-"
                        + "d4 d2 o*"
                        + "d1 d1 a8"
                    )
                ),
                
                // Negation of action
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "21"
                        + "o! a6"
                    ),
                    new IngameScreenMessage("This one reminds me of the Chinese Yin and Yang.", Sfx.VoiceOvers["voice_ingame6a"]),
                    new IngameScreenMessage("I could see how the symbol we now use came about from this one, but is there any overlap in their meaning?", Sfx.VoiceOvers["voice_ingame6b"])
                ),
                
                // Negation of more actions
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "44"
                        + "o! a7 o! a3"
                        + "o! a4 o! a1"
                        + "o! a9 o! a8"
                        + "o! a6 o! a2"
                    )
                ),
                
                // Negation of wait
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "21"
                        + "o! a5"
                    )
                ),
                
                // Negation of number
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "d4 o! d2 a4"
                    )
                ),
                
                // Negation of number again
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "32"
                        + "d4 o! d2"
                        + "o! d1 a1"
                    )
                ),
                
                // Negation of multiply
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d8 o! o* d4 a8"
                    )
                ),
                // Negation of multiply again
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d8 o! o* d2 a9"
                    )
                ),
                // Negation of multiply, rounding
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "62"
                        + "d4 .. o! o* d2 a4"
                        + "d4 d1 o! o* d2 a6"
                    )
                ),
                
                // Negation of subtract
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d4 o! o- d2 a7"
                    )
                ),
                
                // Negation of negation
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "31"
                        + "o! o! a6"
                    )
                ),
                
                
                // Negation exam
                new ScreenData(
                    EngineTilesUnserializer.UnserializeScreen(
                        "65"
                        + "d8 d4 o! d4 o! d2"
                        + "o! o* d1 d1 o* d8"
                        + "o! d4 o! d4 d1 a4"
                        + "o! d2 o* d4 o* o!"
                        + "d1 o- d4 a3 o! a5"
                    )
                ),
                
                // Ignore character
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "31"
                        + "oi a5 a2"
                    ),
                    new IngameScreenMessage("I feel like this one has painfully modern meaning.", Sfx.VoiceOvers["voice_ingame7a"]),
                    new IngameScreenMessage("I... Is it possible that this strange place wasn't just crafted by someone in the past, but rather shapes itself to the eye of the beholder?", Sfx.VoiceOvers["voice_ingame7b"]),
                    new IngameScreenMessage("Would someone else coming here see different glyphs?", Sfx.VoiceOvers["voice_ingame7c"]),
                    new IngameScreenMessage("Maybe it's no accident that I've been so capable at understanding this so far.", Sfx.VoiceOvers["voice_ingame7d"])
                ),
                
                // Ignore numbers
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "31"
                        + "oi d8 a9"
                    )
                ),
                
                // Ignore multiplication
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d4 oi o* d4 a8"
                    )
                ),
                
                // Ignore subtraction
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d1 oi o- d8 a5"
                    )
                ),
                
                // Ignore invert
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "61"
                        + "d8 oi o! o! d4 a5"
                    )
                ),
                
                // Invert ignore
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "42"
                        + "o! oi d4 a7"
                        + "oi o! d2 a9"
                    )
                ),
                
                // Ignore ignore
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "31"
                        + "oi oi a2"
                    )
                ),
                
                // Invert ignore exam
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "63"
                        + "o! d4 oi o* o- o!"
                        + "d8 oi a3 o* d2 d1"
                        + "oi oi o! o* d8 a4"
                    )
                ),
                
                // Cameleon intro
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "C2 .. a4"
                        + ".. .. .."
                        + "a6 .. a1"
                    ),
                    new IngameScreenMessage("I've seen this symbol before carved on talismans made by a South American tribe.", Sfx.VoiceOvers["voice_ingame5a"]),
                    new IngameScreenMessage("It symbolized an eye that looks at the wearer and imitates him, distracting evil spirits.", Sfx.VoiceOvers["voice_ingame5b"]),
                    new IngameScreenMessage("But what can it mean here?", Sfx.VoiceOvers["voice_ingame5c"])
                ),
                
                // Cameleon diagonals
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "C3 .. a4"
                        + ".. .. .."
                        + "C9 .. a1"
                    )
                ),
                
                // Cameleon takes numbers
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "d2 C4 C6 d1 a5"
                    )
                ),
                
                // Cameleon follows itself 
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "C2 .. a4"
                        + ".. .. .."
                        + "C6 .. a1"
                    )
                ),
                
                // Cameleon seeing nothing are ignored
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "C4 .. a5"
                        + ".. .. a5"
                        + "C2 .. a5"
                    )
                ),
                
                
                // Endless cameleon loops are ignored 
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "C2 .. C2"
                        + ".. .. .."
                        + "C6 .. a5"
                    )
                ),
                
                // Cameleon exam
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "C2 C3 a9"
                        + ".. a5 C4"
                        + "C6 .. a3"
                    )
                ),
                
                // Brackets intro
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "41"
                        + "(( a4 )) a6"
                    ),
                    new IngameScreenMessage("Interesting, these two are clearly marking a section of the sentence, like parentheses or a quote.", Sfx.VoiceOvers["voice_ingame8a"])
                ),
                
                // Brackets intro 2
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "51"
                        + "(( a4 a5 )) a6"
                    )
                ),
                
                // Brackets multi starts
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "(( a4 .."
                        + "(( a2 .."
                        + "a7 )) a5"
                    )
                ),
                
                // No opening bracket
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + ".. a8 .."
                        + "a4 .. a6"
                        + ".. )) a5"
                    )
                ),
                
                // Multiple closing bracket
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "a7 (( .."
                        + ")) a5 ))"
                        + "a1 )) a3"
                    )
                ),
                
                // Bracket inverted
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "33"
                        + "(( a8 .."
                        + "a4 o! (("
                        + "a1 )) a6"
                    )
                ),
                
                // preparation
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "11"
                        + "a5"
                    )
                ),
                // preparation
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "11"
                        + "a4"
                    )
                ),
                // preparation
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "11"
                        + "a6"
                    )
                ),
                
                // Final one
                new ScreenData(    
                    EngineTilesUnserializer.UnserializeScreen(
                        "65"
                        + "a5 (( d1 C2 o* .."
                        + ".. c9 a9 .. C7 o!"
                        + ".. .. oi )) .. .."
                        + "d1 a4 .. d2 oi (("
                        + "oi o! o! o- d2 a5"
                    ),
                    new IngameScreenMessage("This is it, the final panel.", Sfx.VoiceOvers["voice_ingame10a"]),
                    new IngameScreenMessage("Will solving this repair or destroy the machine?", Sfx.VoiceOvers["voice_ingame10b"]),
                    new IngameScreenMessage("But, most importantly, will this return the world to the way it was?", Sfx.VoiceOvers["voice_ingame10c"]),
                    new IngameScreenMessage("Only one way to know...", Sfx.VoiceOvers["voice_ingame10d"])

                ),
            };
        }
    }
}