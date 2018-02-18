using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IrregularMachine.BitmapTexts {
    public static class BitmapTextTools {
        public static int GetMaxLineWidth(BitmapFont font, string text) {
            return text.Split('\n').Max(x => CalculateTextWidth(x, font));
        }

        public static List<BitmapTextLine> DivideMultilineIntoLines(BitmapFont font, string text, float availableWidth, float fontScale) {
            var finalLines = new List<BitmapTextLine>();
            var lines = text.Split('\n');

            for (var i = 0; i < lines.Length; i++) {
                string line = lines[i];
                if (i != lines.Length - 1) {
                    line += "\n";
                }
                
                finalLines.AddRange(DivideSinglelineIntoLines(font, line, availableWidth, fontScale));
            }

            return finalLines;
        }

        public static List<BitmapTextLine> DivideSinglelineIntoLines(BitmapFont font, string text, float availableWidth, float fontScale) {
            var finalLines = new List<BitmapTextLine>();

            var lineStartPosition = 0;
            var lineEndPosition = 0;
            var wordBeginning = 0;
            var hadBreakingPoint = false;
            var nextCharacterStartPoint = 0f;

            var isLineFinished = false;

            for (var characterIndex = 0; characterIndex < text.Length; characterIndex++) {
                char character = text[characterIndex];
                var fontCharacter = font.GetCharacter(character);

                if (IsNewline(character)) {
                    isLineFinished = true;
                    lineEndPosition = characterIndex + 1;
                    hadBreakingPoint = true;
                } else if (!font.HasCharacter(character)) {
                    continue;
                } else if (fontCharacter.Width > 0 && nextCharacterStartPoint + fontCharacter.Width * fontScale > availableWidth) {
                    isLineFinished = true;
                    if (hadBreakingPoint) {
                        lineEndPosition = wordBeginning;
                    } else if (wordBeginning == characterIndex) {
                        lineEndPosition = characterIndex + 1;
                    } else {
                        lineEndPosition = characterIndex;
                    }
                } else {
                    nextCharacterStartPoint += fontCharacter.Advance * fontScale;

                    if (IsLineBreakCharacter(character)) {
                        wordBeginning = characterIndex + 1;
                        hadBreakingPoint = true;
                    }
                }

                if (isLineFinished) {
                    if (lineEndPosition > text.Length) {
                        break;
                    }
                    var newLine = text.Substring(lineStartPosition, lineEndPosition - lineStartPosition);
                    var lineWidth = CalculateTextWidth(newLine, font) * fontScale;
                    Debug.Assert(lineWidth <= availableWidth || newLine.Length == 1);
                    finalLines.Add(new BitmapTextLine(newLine, lineWidth));
                    lineStartPosition = lineEndPosition;
                    wordBeginning = lineEndPosition;
                    nextCharacterStartPoint = 0;
                    isLineFinished = false;
                    hadBreakingPoint = false;
                    characterIndex = lineEndPosition - 1;
                }
            }

            var finalLine = text.Substring(lineStartPosition);
            if (finalLine.Length > 0) {
                var lineWidth = CalculateTextWidth(finalLine, font) * fontScale;
                Debug.Assert(lineWidth <= availableWidth || finalLine.Length == 1);
                finalLines.Add(new BitmapTextLine(finalLine, lineWidth));
            } else if (text == "") {
                finalLines.Add(new BitmapTextLine("", 0));
            }

            return finalLines;
        }

        private static bool IsNewline(char character) {
            return character == '\n' || character == '\r';
        }

        private static bool IsLineBreakCharacter(char character) {
            return character == ' ' || character == '\t';
        }

        private static int CalculateTextWidth(string text, BitmapFont font) {
            var lineWidth = 0;
            var nextCharacterStartPoint = 0;
            foreach (var character in text) {
                var fontCharacter = font.GetCharacter(character);

                if (fontCharacter == null) {
                    continue;
                }

                if (fontCharacter.Width > 0 && !fontCharacter.IsWhitespace) {
                    lineWidth = nextCharacterStartPoint + fontCharacter.Width;
                }

                nextCharacterStartPoint += fontCharacter.Advance;
            }

            return lineWidth;
        }
    }
}