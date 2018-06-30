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
    /// Wrap an ICoordinate with it's index position from the source data.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("#{Index} {Coordinate}")]
    public class SequentialCoordinate : ICoordinate
    {
        private readonly ICoordinate _coordinate;

        /// <summary>
        /// Wrap an ICoordinate with it's index position from the source data.
        /// </summary>
        /// <param name="index">The index position of the ICoordinate from the source.</param>
        /// <param name="coordinate">The original ICoordinate object.</param>
        internal SequentialCoordinate(int index, ICoordinate coordinate)
        {
            _coordinate = coordinate;
            this.Index = index;
        }

        /// <summary>
        /// The original ICoordinate object.
        /// </summary>
        public ICoordinate Coordinate
        {
            get
            {
                return _coordinate;
            }
        }

        /// <summary>
        /// The index position of the ICoordinate from the source.
        /// </summary>
        public int Index { get; private set; }

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
    }
}