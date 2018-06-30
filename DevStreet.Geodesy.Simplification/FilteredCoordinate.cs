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
    /// Information about an ICoordinate was filtered by a simplification algorithm.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("#{Index} {Coordinate} {State},nq")]
    public class FilteredCoordinate<T> : ICoordinate where T : ICoordinate
    {
        private readonly ICoordinate _coordinate;

        /// <summary>
        /// Information about an ICoordinate was filtered by a simplification algorithm.
        /// </summary>
        /// <param name="index">The zero-based index of the ICoordinate in the source data.</param>
        /// <param name="coordinate">The original ICoordinate object.</param>
        /// <param name="state">How the simplification algorithm filtered the ICoordinate.</param>
        internal FilteredCoordinate(int index, ICoordinate coordinate, FilteredCoordinateState state)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate), "The argument cannot be null.");
            }

            _coordinate = coordinate;
            this.Index = index;
            this.State = state;
        }

        /// <summary>
        /// The zero-based index of the ICoordinate in the source.
        /// </summary>
        public int Index { get; private set; } 

        /// <summary>
        /// The original ICoordinate object.
        /// </summary>
        public T Coordinate
        {
            get
            {
                return (T)_coordinate;
            }
        }

        /// <summary>
        /// The latitude from the original ICoordinate object.
        /// </summary>
        public double Latitude
        {
            get
            {
                return _coordinate.Latitude;
            }

            set
            {
                _coordinate.Latitude = value;
            }
        }

        /// <summary>
        /// The longitude from the original ICoordinate object.
        /// </summary>
        public double Longitude
        {
            get
            {
                return _coordinate.Longitude;
            }

            set
            {
                _coordinate.Longitude = value;
            }
        }

        /// <summary>
        /// How the simplification algorithm filtered the ICoordinate.
        /// </summary>
        public FilteredCoordinateState State { get; private set; }
    }
}