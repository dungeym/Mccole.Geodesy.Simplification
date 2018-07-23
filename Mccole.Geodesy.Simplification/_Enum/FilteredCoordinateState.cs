using Mccole.Geodesy.Calculator;
using Mccole.Geodesy.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mccole.Geodesy.Simplification
{
    /// <summary>
    /// How the simplification algorithm filtered the ICoordinate.
    /// </summary>
    public enum FilteredCoordinateState
    {
        /// <summary>
        /// The simplification filter excluded this instance of ICoordinate.
        /// </summary>
        Excluded = 0,

        /// <summary>
        /// The simplification filter did not excluded this instance of ICoordinate.
        /// </summary>
        Included = 1
    }
}