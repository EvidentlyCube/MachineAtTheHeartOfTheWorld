using System.Diagnostics;

namespace IrregularMachine.BitmapTexts
{
    [DebuggerDisplay("{Width} => '{Line}'")]
    public class BitmapTextLine
    {
        public readonly float Width;
        public readonly string Line;

        public BitmapTextLine(string line, float width)
        {
            Line = line;
            Width = width;
        }

        protected bool Equals(BitmapTextLine other)
        {
            return Width == other.Width && string.Equals(Line, other.Line);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BitmapTextLine) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Width * 397) ^ (Line != null ? Line.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{Width} => '{Line}'";
        }
    }
}