using DevStreet.Geodesy.Calculator;
using DevStreet.Geodesy.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStreet.Geodesy.Simplification
{

    /// <summary>
    /// Wrapper object used to reflect how the ITrackpoint was filtered by a simplification method.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Trackpoint}")]
    public class FilteredTrackpoint<T> : ITrackpoint where T : ITrackpoint
    {
        private readonly ITrackpoint _trackpoint;

        /// <summary>
        /// Wrapper object used to reflect how the ITrackpoint was filtered by a simplification method.
        /// </summary>
        /// <param name="index">The zero-based index of the ITrackpoint in the source.</param>
        /// <param name="trackpoint"></param>
        /// <param name="state"></param>
        internal FilteredTrackpoint(int index, ITrackpoint trackpoint, FilteredTrackpointState state)
        {
            if (trackpoint == null)
            {
                throw new ArgumentNullException(nameof(trackpoint), "The argument cannot be null.");
            }

            _trackpoint = trackpoint;
            this.Index = index;
            this.State = state;
        }

        /// <summary>
        /// The zero-based index of the ITrackpoint in the source.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// The elevation from the original ITrackpoint object.
        /// </summary>
        public double Elevation
        {
            get
            {
                return _trackpoint.Elevation;
            }

            set
            {
                _trackpoint.Elevation = value;
            }
        }

        /// <summary>
        /// The latitude from the original ITrackpoint object.
        /// </summary>
        public double Latitude
        {
            get
            {
                return _trackpoint.Latitude;
            }

            set
            {
                _trackpoint.Latitude = value;
            }
        }

        /// <summary>
        /// The longitude from the original ITrackpoint object.
        /// </summary>
        public double Longitude
        {
            get
            {
                return _trackpoint.Longitude;
            }

            set
            {
                _trackpoint.Longitude = value;
            }
        }

        /// <summary>
        /// The filtered state of this ITrackpoint.
        /// </summary>
        public FilteredTrackpointState State { get; private set; }

        /// <summary>
        /// The timestamp from the original ITrackpoint object.
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return _trackpoint.Timestamp;
            }

            set
            {
                _trackpoint.Timestamp = value;
            }
        }

        /// <summary>
        /// The original ITrackpoint object.
        /// </summary>
        public T Trackpoint
        {
            get
            {
                return (T)_trackpoint;
            }
        }
    }
}