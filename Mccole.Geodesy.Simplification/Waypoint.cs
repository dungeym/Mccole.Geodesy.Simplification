using DevStreet.Geodesy.Extension;
using System;

namespace DevStreet.Geodesy.Simplification
{
    /// <summary>
    /// Represents 3 dimensional a point on the earth's surface.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
    public class Waypoint : Coordinate, IComparable, IWaypoint
    {
        /// <summary>
        /// Represents 3 dimensional a point on the earth's surface.
        /// </summary>
        public Waypoint()
        {
        }

        /// <summary>
        /// Represents 3 dimensional a point on the earth's surface.
        /// </summary>
        /// <param name="latitude">The angular distance of a place north or south of the earth's equator.</param>
        /// <param name="longitude">The angular distance of a place east or west of the Greenwich meridian.</param>
        /// <param name="elevation">The height above sea level of this location.</param>
        public Waypoint(double latitude, double longitude, double elevation)
            : base(latitude, longitude)
        {
            this.Elevation = elevation;
        }

        /// <summary>
        /// Represents 3 dimensional a point on the earth's surface.
        /// </summary>
        /// <param name="latitude">The angular distance of a place north or south of the earth's equator.</param>
        /// <param name="longitude">The angular distance of a place east or west of the Greenwich meridian.</param>
        /// <param name="elevation">The height above sea level of this location.</param>
        public Waypoint(string latitude, string longitude, double elevation)
            : base(latitude, longitude)
        {
            this.Elevation = elevation;
        }

        #region DebuggerDisplay

#pragma warning disable S1144

        private string DebuggerDisplay
        {
            get
            {
                return this.ToString();
            }
        }

#pragma warning restore S1144

        #endregion DebuggerDisplay

        /// <summary>
        /// The height above sea level of this location.
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// Compare the left against the right.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static int Compare(IComparable left, IComparable right)
        {
            return left.CompareTo(right);
        }

        /// <summary>
        /// Determine if the left Waypoint does not equal the right Waypoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Waypoint left, Waypoint right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determine if the left Waypoint is less than the right Waypoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Waypoint left, Waypoint right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        /// Determine if the left Waypoint is less than or equal to the right Waypoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(Waypoint left, Waypoint right)
        {
            return Compare(left, right) <= 0;
        }

        /// <summary>
        /// Determine if the left Waypoint is equal to the right Waypoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Waypoint left, Waypoint right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return object.ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Determine if the left Waypoint is greater than the right Waypoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Waypoint left, Waypoint right)
        {
            return Compare(left, right) > 0;
        }

        /// <summary>
        /// Determine if the left Waypoint is greater than or equal to the right Waypoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Waypoint left, Waypoint right)
        {
            return Compare(left, right) >= 0;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether
        /// the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public new int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Waypoint Waypoint = obj as Waypoint;
            if (Waypoint != null)
            {
                int result = this.Latitude.CompareTo(Waypoint.Latitude);
                result = result == 0 ? this.Longitude.CompareTo(Waypoint.Longitude) : result;
                return result == 0 ? this.Elevation.CompareTo(Waypoint.Elevation) : result;
            }
            else
            {
                throw new InvalidCastException(string.Format("The type '{0}' could not be converted to a {1}.", obj.GetType().Name, nameof(Waypoint)));
            }
        }

        /// <summary>
        /// Determine if this Waypoint equals the defined object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as Waypoint;
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            return this.CompareTo(other) == 0;
        }

        /// <summary>
        /// Get the hash code for this Waypoint.
        /// </summary>
        /// <returns></returns>

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Latitude.GetHashCode() * 1369) ^ (this.Longitude.GetHashCode() * 37) ^ (this.Elevation.GetHashCode() * 29);
            }
        }

        /// <summary>
        /// Get the string representation of this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.Latitude, this.Longitude, this.Elevation);
        }
    }
}
