using System;
using System.Collections.Generic;
using System.Windows;

namespace DecimalGeometry
{
    /// <summary>
    /// A bounding box that is the minimum size required to encompass all of the specified coordinates. Based on System.Windows.Rect, but uses 'decimal' internally, instead of 'double', to prevent floating-point errors.
    /// </summary>
    public class BoundingBox
    {
        private decimal _left;
        private decimal _top;
        private decimal _right;
        private decimal _bottom;

        /// <summary>
        /// The X value of the left side of the bounding box.
        /// </summary>
        public decimal Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
                TopLeft.X = value;
                BottomLeft.X = value;
                RecalculateWidth();
                RecalculateCenter();
            }
        }

        /// <summary>
        /// The Y value of the top side of the bounding box.
        /// </summary>
        public decimal Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
                TopLeft.Y = value;
                TopRight.Y = value;
                RecalculateHeight();
                RecalculateCenter();
            }
        }

        /// <summary>
        /// The X value of the right side of the bounding box.
        /// </summary>
        public decimal Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
                TopRight.X = value;
                BottomRight.X = value;
                RecalculateWidth();
                RecalculateCenter();
            }
        }

        /// <summary>
        /// The Y value of the bottom side of the bounding box.
        /// </summary>
        public decimal Bottom
        {
            get
            {
                return _bottom;
            }
            set
            {
                _bottom = value;
                BottomLeft.Y = value;
                BottomRight.Y = value;
                RecalculateHeight();
                RecalculateCenter();
            }
        }

        /// <summary>
        /// The width of the bounding box.
        /// </summary>
        public decimal Width { get; private set; }

        /// <summary>
        /// The height of the bounding box.
        /// </summary>
        public decimal Height { get; private set; }

        /// <summary>
        /// A coordinate representing the center of the bounding box.
        /// </summary>
        public Coordinate Center { get; private set; } = new();

        /// <summary>
        /// A coordinate representing the center of the bounding box, relative to the top left corner.
        /// </summary>
        public Coordinate RelativeCenter { get; private set; } = new();

        /// <summary>
        /// A coordinate representing the top left corner of the bounding box.
        /// </summary>
        public Coordinate TopLeft { get; private set; } = new();

        /// <summary>
        /// A coordinate representing the top right corner of the bounding box.
        /// </summary>
        public Coordinate TopRight { get; private set; } = new();

        /// <summary>
        /// A coordinate representing the bottom left corner of the bounding box.
        /// </summary>
        public Coordinate BottomLeft { get; private set; } = new();

        /// <summary>
        /// A coordinate representing the bottom right corner of the bounding box.
        /// </summary>
        public Coordinate BottomRight { get; private set; } = new();

        /// <summary>
        /// Creates a new bounding box that encompasses the specified list of coordinates.
        /// </summary>
        /// <param name="coordinates">The list of coordinates to be encompassed by the bounding box.</param>
        public BoundingBox(List<Coordinate> coordinates) : this(coordinates[0])
        {
            for (int i = 1; i < coordinates.Count; i++)
            {
                Encompass(coordinates[i]);
            }
        }

        /// <summary>
        /// Creates a new bounding box using the specified coordinate. The initial width and height will equal zero.
        /// </summary>
        /// <param name="c">The first coordinate to be encompassed by the bounding box.</param>
        public BoundingBox(Coordinate c) : this(c.X, c.Y)
        {
        }

        /// <summary>
        /// Creates a new bounding box using the specified two sides. The initial width and height will equal zero.
        /// </summary>
        /// <param name="left">The X value of the left side of the bounding box.</param>
        /// <param name="top">The Y value of the top side of the bounding box.</param>
        public BoundingBox(decimal left, decimal top) : this(left, top, left, top)
        {
        }

        /// <summary>
        /// Creates a new bounding box using the specified four sides.
        /// </summary>
        /// <param name="left">The X value of the left side of the bounding box.</param>
        /// <param name="top">The Y value of the top side of the bounding box.</param>
        /// <param name="right">The X value of the right side of the bounding box.</param>
        /// <param name="bottom">The Y value of the bottom side of the bounding box.</param>
        /// <exception cref="ArgumentException">Thrown when either 'right' is less than 'left', or 'bottom' is less than 'top'.</exception>
        public BoundingBox(decimal left, decimal top, decimal right, decimal bottom)
        {
            if (right < left) throw new ArgumentException("The value of '" + nameof(right) + "' cannot be less than the value of '" + nameof(left) + "'.", nameof(right));
            if (bottom < top) throw new ArgumentException("The value of '" + nameof(bottom) + "' cannot be less than the value of '" + nameof(top) + "'.", nameof(bottom));

            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return Top + "," + Left + "," + Width + "," + Height;
        }

        private void RecalculateWidth()
        {
            Width = Right - Left;
        }

        private void RecalculateHeight()
        {
            Height = Bottom - Top;
        }

        private void RecalculateCenter()
        {
            RelativeCenter.X = Width / 2;
            RelativeCenter.Y = Height / 2;

            Center.X = Left + RelativeCenter.X;
            Center.Y = Top + RelativeCenter.Y;
        }

        /// <summary>
        /// Expands the bounding box to also encompass the specified list of coordinates.
        /// </summary>
        /// <param name="coordinates">The list of additional coordinates to be encompassed.</param>
        public void Encompass(List<Coordinate> coordinates)
        {
            foreach (Coordinate c in coordinates) Encompass(c);
        }

        /// <summary>
        /// Expands the bounding box to also encompass the specified coordinate.
        /// </summary>
        /// <param name="c">The additional coordinate to be encompassed.</param>
        public void Encompass(Coordinate c)
        {
            if (c.X < Left) Left = c.X;
            if (c.X > Right) Right = c.X;
            if (c.Y < Top) Top = c.Y;
            if (c.Y > Bottom) Bottom = c.Y;
        }

        public static implicit operator Rect(BoundingBox b)
        {
            return new Rect(b.TopLeft, b.BottomRight);
        }
    }
}
