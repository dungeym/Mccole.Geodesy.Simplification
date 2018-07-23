using DevStreet.Geodesy.Extension;
using System;

namespace DevStreet.Geodesy.Simplification
{
    /// <summary>
    /// Represents the point-in-time that a specific Waypoint was travelled through.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
    public class Trackpoint : Waypoint, IComparable, ITrackpoint
    {
        /// <summary>
        /// Represents the point-in-time that a specific Waypoint was travelled through.
        /// </summary>
        public Trackpoint()
        {
        }

        /// <summary>
        /// Represents the point-in-time that a specific Waypoint was travelled through.
        /// </summary>
        /// <param name="latitude">The angular distance of a place north or south of the earth's equator.</param>
        /// <param name="longitude">The angular distance of a place east or west of the Greenwich meridian.</param>
        /// <param name="timestamp">The date and time the coordinate was travelled through.</param>
        public Trackpoint(double latitude, double longitude, DateTime timestamp)
            : this(latitude, longitude, 0, timestamp)
        {
        }

        /// <summary>
        /// Represents the point-in-time that a specific Waypoint was travelled through.
        /// </summary>
        /// <param name="latitude">The angular distance of a place north or south of the earth's equator.</param>
        /// <param name="longitude">The angular distance of a place east or west of the Greenwich meridian.</param>
        /// <param name="elevation">The height above sea level of this location.</param>
        /// <param name="timestamp">The date and time the coordinate was travelled through.</param>
        public Trackpoint(double latitude, double longitude, double elevation, DateTime timestamp)
            : base(latitude, longitude, elevation)
        {
            this.Timestamp = timestamp;
        }

        /// <summary>
        /// Represents the point-in-time that a specific Waypoint was travelled through.
        /// </summary>
        /// <param name="latitude">The angular distance of a place north or south of the earth's equator.</param>
        /// <param name="longitude">The angular distance of a place east or west of the Greenwich meridian.</param>
        /// <param name="timestamp">The date and time the coordinate was travelled through.</param>
        public Trackpoint(string latitude, string longitude, DateTime timestamp)
            : this(latitude, longitude, 0, timestamp)
        {
        }

        /// <summary>
        /// Represents the point-in-time that a specific Waypoint was travelled through.
        /// </summary>
        /// <param name="latitude">The angular distance of a place north or south of the earth's equator.</param>
        /// <param name="longitude">The angular distance of a place east or west of the Greenwich meridian.</param>
        /// <param name="elevation">The height above sea level of this location.</param>
        /// <param name="timestamp">The date and time the coordinate was travelled through.</param>
        public Trackpoint(string latitude, string longitude, double elevation, DateTime timestamp)
            : base(latitude, longitude, elevation)
        {
            this.Timestamp = timestamp;
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
        /// The date and time the coordinate was travelled through.
        /// </summary>
        public DateTime Timestamp { get; set; }

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
        /// Determine if the left Trackpoint does not equal the right Trackpoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Trackpoint left, Trackpoint right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determine if the left Trackpoint is less than the right Trackpoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Trackpoint left, Trackpoint right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        /// Determine if the left Trackpoint is less than or equal to the right Trackpoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(Trackpoint left, Trackpoint right)
        {
            return Compare(left, right) <= 0;
        }

        /// <summary>
        /// Determine if the left Trackpoint is equal to the right Trackpoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Trackpoint left, Trackpoint right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return object.ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Determine if the left Trackpoint is greater than the right Trackpoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Trackpoint left, Trackpoint right)
        {
            return Compare(left, right) > 0;
        }

        /// <summary>
        /// Determine if the left Trackpoint is greater than or equal to the right Trackpoint.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Trackpoint left, Trackpoint right)
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

            Trackpoint trackpoint = obj as Trackpoint;
            if (trackpoint != null)
            {
                int result = this.Latitude.CompareTo(trackpoint.Latitude);
                result = result == 0 ? this.Longitude.CompareTo(trackpoint.Longitude) : result;
                return result == 0 ? this.Timestamp.CompareTo(trackpoint.Timestamp) : result;
            }
            else
            {
                throw new InvalidCastException(string.Format("The type '{0}' could not be converted to a {1}.", obj.GetType().Name, nameof(Trackpoint)));
            }
        }

        /// <summary>
        /// Determine if this Trackpoint equals the defined object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as Trackpoint;
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            return this.CompareTo(other) == 0;
        }

        /// <summary>
        /// Get the hash code for this Trackpoint.
        /// </summary>
        /// <returns></returns>

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Latitude.GetHashCode() * 1369) ^ (this.Longitude.GetHashCode() * 37) ^ (this.Timestamp.GetHashCode() * 29);
            }
        }

        /// <summary>
        /// Get the string representation of this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1} @ {2}", this.Latitude, this.Longitude, this.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
