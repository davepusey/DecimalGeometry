using System;
using System.Windows;

namespace DecimalGeometry
{
    /// <summary>
    /// A coordinate that represents a two-dimensional position. Based on System.Windows.Point, but uses 'decimal' internally, instead of 'double', to prevent floating-point errors.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// The X value of the coordinate.
        /// </summary>
        public decimal X { get; set; }

        /// <summary>
        /// The Y value of the coordinate.
        /// </summary>
        public decimal Y { get; set; }

        /// <summary>
        /// Creates a new coordinate using default X and Y values.
        /// </summary>
        public Coordinate() { }

        /// <summary>
        /// Creates a new coordinate using the specified X and Y values.
        /// </summary>
        /// <param name="x">The X value of the new coordinate.</param>
        /// <param name="y">The Y value of the new coordinate.</param>
        public Coordinate(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X + "," + Y;
        }

        /// <summary>
        /// The relative angle, from this coordinate, to the specified target coordinate.
        /// </summary>
        /// <param name="c">The target coordinate.</param>
        /// <returns>The angle, in degrees, measured counter-clockwise from the East of this coordinate.</returns>
        public decimal AngleTo(Coordinate c)
        {
            return AngleBetween(this, c);
        }

        /// <summary>
        /// The relative angle, from the specified origin coordinate, to the specified target coordinate.
        /// </summary>
        /// <param name="c1">The origin coordinate.</param>
        /// <param name="c2">The target coordinate.</param>
        /// <returns>The angle, in degrees, measured counter-clockwise from the East of the origin coordinate.</returns>
        public static decimal AngleBetween(Coordinate c1, Coordinate c2)
        {
            decimal x = c2.X - c1.X;
            decimal y = c2.Y - c1.Y;

            decimal radians = (decimal)Math.Atan2((double)y, (double)x);

            return radians * 180 / (decimal)Math.PI;
        }

        /// <summary>
        /// The distance, from this coordinate, to the specified target coordinate.
        /// </summary>
        /// <param name="c">The target coordinate.</param>
        /// <returns>The distance, measured as a single straight line.</returns>
        public decimal DistanceTo(Coordinate c)
        {
            return DistanceBetween(this, c);
        }

        /// <summary>
        /// The distance, from the specified origin coordinate, to the specified target coordinate.
        /// </summary>
        /// <param name="c1">The origin coordinate.</param>
        /// <param name="c2">The target coordinate.</param>
        /// <returns>The distance, measured as a single straight line.</returns>
        public static decimal DistanceBetween(Coordinate c1, Coordinate c2)
        {
            decimal x = c2.X - c1.X;
            decimal y = c2.Y - c1.Y;

            decimal hypotenuse = (x * x) + (y * y);

            return (decimal)Math.Sqrt((double)hypotenuse);
        }

        public static implicit operator Point(Coordinate c)
        {
            return new Point((double)c.X, (double)c.Y);
        }
    }
}
