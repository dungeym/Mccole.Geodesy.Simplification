using DevStreet.Geodesy.Calculator;
using DevStreet.Geodesy.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStreet.Geodesy.Simplification.Filter
{
    /// <summary>
    /// An implementation of the Reumann-Witkam algorithm for simplifying a polyline.
    /// </summary>
    public class ReumannWitkamFilter : FilterBase
    {
        /*
         * http://psimpl.sourceforge.net/reumann-witkam.html
         *
         * Instead of using a point-to-point (radial) distance tolerance as a rejection criterion (see Radial Distance),
         * the O(n) Reumann-Witkam routine uses a point-to-line (perpendicular) distance tolerance. It defines a line
         * through the first two vertices of the original polyline. For each successive vertex vi, its perpendicular
         * distance to this line is calculated. A new key is found at vi-1, when this distance exceeds the specified
         * tolerance. The vertices vi and vi+1 are then used to define a new line, and the process repeats itself.
         */

        private static bool IsDistanceWithinTolerance(ICoordinate pointA, ICoordinate pointB, ICoordinate pointC, double tolerance)
        {
            var d = GeodeticCalculator.Instance.DistanceToPlane(pointA, pointB, pointC);
            return d < tolerance;
        }

        private static int SeekNextCoordinate(IEnumerable<ICoordinate> items, int index, double tolerance)
        {
            for (int i = (index + 2); i < items.Count(); i++)
            {
                if (IsDistanceWithinTolerance(items.ElementAt(index), items.ElementAt(index + 1), items.ElementAt(i), tolerance) == false)
                {
                    return i - 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) using the Reumann-Witkam algorithm and the defined tolerance.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="tolerance">The minimum tolerance, point(s) with a tolerance below this value will not be retained.</param>
        /// <returns></returns>
        public static IEnumerable<ICoordinate> Simplify(IEnumerable<ICoordinate> items, double tolerance)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }
            if (tolerance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tolerance), "The argument cannot be less than zero.");
            }

            List<ICoordinate> results = new List<ICoordinate>();
            results.Add(items.ElementAt(0));

            int index = 0;
            int key = 0;
            while (key != -1)
            {
                key = SeekNextCoordinate(items, index, tolerance);
                if (key != -1)
                {
                    results.Add(items.ElementAt(key));
                    index = key;
                }
            }

            AddLastIfNotContains(results, items);
            return results;
        }
    }
}