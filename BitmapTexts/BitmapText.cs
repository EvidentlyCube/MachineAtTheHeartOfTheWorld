using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using RectangleF = System.Drawing.RectangleF;

namespace IrregularMachine.BitmapTexts {
    public class BitmapText : IDisposable {
        private readonly BitmapFont _font;

        private readonly List<BitmapTextTuple> _characters;

        private string _text;

        private List<BitmapTextLine> _lines;
        private BitmapTextHorizontalAlign _horizontalAlign;
        private BitmapTextVerticalAlign _verticalAlign;
        private int _lineSpacing;
        private Color _color;
        private float _alpha;

        public bool _isTextDirty;
        private bool _isTupleListDirty;
        private bool _isPositionDirty;
        private float _textWidth;
        private float _textHeight;
        private float _maxTextWidth;
        private float _maxTextHeight;
        private bool _areAllLinesVisible;

        private PointF _position;
        private SizeF _size;
        private float _fontScale;

        public int VisibleCharacters { get; set; }

        public float X {
            get => Position.X;
            set => Position = new PointF(value, Position.Y);
        }

        public float Y {
            get => Position.Y;
            set => Position = new PointF(Position.X, value);
        }

        public float Right {
            get => Position.X + Width;
            set => Position = new PointF(value - Width, Position.Y);
        }

        public float Bottom {
            get => Position.Y + Height;
            set => Position = new PointF(Position.X, value - Height);
        }

        public float Width {
            get => Size.Width;
            set => Size = new SizeF(value, Size.Height);
        }

        public float Height {
            get => Size.Height;
            set => Size = new SizeF(Size.Width, value);
        }

        public SizeF Size {
            get => _size;
            set {
                if (_size != value) {
                    _size = value;
                    _isTextDirty = true;
                }
            }
        }

        public PointF Position {
            get => _position;
            set {
                if (_position != value) {
                    _position = value;
                    _isPositionDirty = true;
                }
            }
        }

        public RectangleF Bounds => new RectangleF(Position, Size);

        public string Text {
            get => _text;
            set {
                if (_text != value) {
                    _text = value;
                    _isTextDirty = true;
                }
            }
        }

        public int LineSpacing {
            get => _lineSpacing;
            set {
                if (_lineSpacing != value) {
                    _lineSpacing = value;
                    _isTupleListDirty = true;
                }
            }
        }

        public BitmapTextHorizontalAlign HorizontalAlign {
            get => _horizontalAlign;
            set {
                if (_horizontalAlign != value) {
                    _horizontalAlign = value;
                    _isTupleListDirty = true;
                }
            }
        }

        public BitmapTextVerticalAlign VerticalAlign {
            get => _verticalAlign;
            set {
                if (_verticalAlign != value) {
                    _verticalAlign = value;
                    _isTupleListDirty = true;
                }
            }
        }

        public Color Color {
            get => _color;
            set {
                if (_color != value) {
                    _color = value;
                    _isTupleListDirty = true;
                }
            }
        }

        public float Alpha {
            get => _alpha;
            set {
                if (Math.Abs(_alpha - value) > 0.001f) {
                    _alpha = value;
                    _isTupleListDirty = true;
                }
            }
        }

        public float TextWidth {
            get {
                Regenerate();
                return _textWidth;
            }
        }

        public float TextHeight {
            get {
                Regenerate();
                return _textHeight;
            }
        }

        public float MaxTextWidth {
            get {
                Regenerate();
                return _maxTextWidth;
            }
        }

        public float MaxTextHeight {
            get {
                Regenerate();
                return _maxTextHeight;
            }
        }

        public bool AreAllLinesVisible {
            get {
                Regenerate();
                return _areAllLinesVisible;
            }
        }

        public float FontScale {
            get => _fontScale;
            set {
                if (Math.Abs(_fontScale - value) > 0.001f) {
                    _fontScale = value;
                    _isTextDirty = true;
                }
            }
        }

        public int EffectiveLineHeight => _lineSpacing + _font.LineHeight;

        public BitmapText(BitmapFont font, string text = "") : this(font, new SizeF(1920, 1080), text) {
            FitSize();
        }

        public BitmapText(BitmapFont font, SizeF size, string text = "") {
            _font = font;
            _text = text;
            _horizontalAlign = BitmapTextHorizontalAlign.Left;
            _isTextDirty = true;
            _isPositionDirty = true;
            _size = size;
            _position = new PointF();
            _characters = new List<BitmapTextTuple>();
            _lineSpacing = 1;
            _color = Color.White;
            _alpha = 1;
            _fontScale = 1;
            VisibleCharacters = int.MaxValue;
        }

        public void Dispose() {
            _lines.Clear();
            _characters.Clear();
            _lines = null;
        }

        public void Draw(SpriteBatch renderBatch) {
            Regenerate();

            var length = Math.Min(VisibleCharacters, _characters.Count);
            for (int i = 0; i < length; i++) {
                _characters[i]?.Draw(renderBatch, _fontScale);
            }
        }

        private void Regenerate() {
            if (_isTextDirty) RegenerateLines();

            if (_isTupleListDirty) RegenerateTuples();

            if (_isPositionDirty) RefreshTuplePosition();
        }

        private void RegenerateLines() {
            _isTextDirty = false;
            _isTupleListDirty = true;

            _lines = BitmapTextTools.DivideMultilineIntoLines(_font, _text, _size.Width, _fontScale);
        }

        private void RegenerateTuples() {
            _isTupleListDirty = false;
            _isPositionDirty = true;
            _areAllLinesVisible = true;

            _textWidth = _lines.Max(line => line.Width);
            _maxTextHeight = (_lines.Count - 1) * EffectiveLineHeight + _font.LineHeight;
            _textHeight = Math.Min(Size.Height, _maxTextHeight);
            _maxTextWidth = BitmapTextTools.GetMaxLineWidth(_font, _text);

            _characters.ForEach(x => x?.ReleaseToPool());
            _characters.Clear();

            var color = new Color(_color.R, _color.G, _color.B, (byte)(_color.A * _alpha));
            var position = new Vector2(0, GetLineVerticalOffset());

            foreach (var line in _lines) {
                if (position.Y + _font.LineHeight > _size.Height) {
                    _areAllLinesVisible = false;
                    break;
                }

                position.X = GetLineHorizontalOffset(line.Width);
                foreach (var character in line.Line) {
                    var fontCharacter = _font.GetCharacter(character);
                    if (fontCharacter == null) {
                        _characters.Add(BitmapTextTuple.GetOne(_font.GetCharacter(' '), position, Color.Transparent));
                        continue;
                    }

                    if (fontCharacter.IsWhitespace || position.Y > _size.Height) {
                        _characters.Add(BitmapTextTuple.GetOne(fontCharacter, position, Color.Transparent));
                    } else {
                        _characters.Add(BitmapTextTuple.GetOne(fontCharacter, position, color));
                    }

                    position.X += fontCharacter.Advance * _fontScale;
                }

                position.Y += EffectiveLineHeight * _fontScale;
            }
        }

        private void RefreshTuplePosition() {
            foreach (var character in _characters) {
                if (character == null) continue;

                character.GlobalPosition = new Vector2(
                    character.LocalPosition.X + _position.X + character.FontCharacter.Offset.X * _fontScale,
                    character.LocalPosition.Y + _position.Y + character.FontCharacter.Offset.Y * _fontScale
                );
            }
        }

        private float GetLineHorizontalOffset(float lineWidth) {
            switch (_horizontalAlign) {
                case BitmapTextHorizontalAlign.Left:
                    return 0;

                case BitmapTextHorizontalAlign.Center:
                    return (_size.Width - lineWidth) / 2f;

                case BitmapTextHorizontalAlign.Right:
                    return _size.Width - lineWidth;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float GetLineVerticalOffset() {
            switch (_verticalAlign) {
                case BitmapTextVerticalAlign.Top:
                    return 0;

                case BitmapTextVerticalAlign.Middle:
                    return Math.Max(0, (_size.Height - TextHeight) / 2f);

                case BitmapTextVerticalAlign.Bottom:
                    return Math.Max(0, _size.Height - TextHeight);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void FitSize() {
            if (MaxTextWidth < Width) Width = MaxTextWidth;

           Size = new SizeF(
               TextWidth,
               MaxTextHeight
           );
        }
    }
}