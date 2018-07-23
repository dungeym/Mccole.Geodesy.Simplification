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
    /// The filtered state of this ITrackpoint.
    /// </summary>
    public enum FilteredTrackpointState
    {
        /// <summary>
        /// The simplification filter excluded this instance of ITrackpoint.
        /// </summary>
        None = 0,

        /// <summary>
        /// The simplification filter identified this instance of ITrackpoint as the start of a segment.
        /// </summary>
        Start = 1,

        /// <summary>
        /// The simplification filter identified this instance of ITrackpoint as a point along a segment.
        /// </summary>
        Point = 2,

        /// <summary>
        /// The simplification filter identified this instance of ITrackpoint as the end of a segment.
        /// </summary>
        End = 3
    }
}