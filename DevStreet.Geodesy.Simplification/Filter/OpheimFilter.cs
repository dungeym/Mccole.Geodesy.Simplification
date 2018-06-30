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
    /// An implementation of the Opheim algorithm for simplifying a polyline.
    /// </summary>
    public class OpheimFilter : FilterBase
    {
        /*
         * http://psimpl.sourceforge.net/opheim.html
         *
         * The O(n) Opheim routine is very similar to the Reumann-Witkam routine, and can be seen as a constrained
         * version of that Reumann-Witkam routine. Opheim uses both a minimum and a maximum distance tolerance
         * to constrain the search area. For each successive vertex vi, its radial distance to the current key
         * vkey (initially v0) is calculated. The last point within the minimum distance tolerance is used to define
         * a ray R (vkey, vi). If no such vi exists, the ray is defined as R(vkey, vkey+1). For each successive
         * vertex vj beyond vi its perpendicular distance to the ray R is calculated. A new key is found at vj-1,
         * when this distance exceeds the minimum tolerance Or when the radial distance between vj and the vkey exceeds
         * the maximum tolerance. After a new key is found, the process repeats itself.
         */

        private static bool IsDistanceWithinTolerance(ICoordinate pointA, ICoordinate pointB, ICoordinate pointC, double minimumTolerance, double maximumTolerance)
        {
            ICoordinate pointX;
            var d1 = GeodeticCalculator.Instance.DistanceToPlane(pointA, pointB, pointC, out pointX);
            var d2 = GeodeticCalculator.Instance.Distance(pointA, pointX);
            return d1 < minimumTolerance && d2 < maximumTolerance;
        }

        private static int SeekNextCoordinate(IEnumerable<ICoordinate> items, int index, double minimumTolerance, double maximumTolerance)
        {
            for (int i = (index + 2); i < items.Count(); i++)
            {
                if (IsDistanceWithinTolerance(items.ElementAt(index), items.ElementAt(index + 1), items.ElementAt(i), minimumTolerance, maximumTolerance) == false)
                {
                    return i - 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) using the Opheim algorithm and the defined tolerance values.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="minimumTolerance">The minimum tolerance (cross track tolerance), point(s) with a tolerance below this value will not be retained.</param>
        /// <param name="maximumTolerance">The maximum tolerance (radial (ray) tolerance), point(s) with a tolerance below this value will not be retained.</param>
        /// <returns></returns>
        public static IEnumerable<ICoordinate> Simplify(IEnumerable<ICoordinate> items, double minimumTolerance, double maximumTolerance)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }
            if (minimumTolerance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumTolerance), "The argument cannot be less than zero.");
            }
            if (maximumTolerance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumTolerance), "The argument cannot be less than zero.");
            }

            List<ICoordinate> results = new List<ICoordinate>();
            results.Add(items.ElementAt(0));

            int index = 0;
            int key = 0;
            while (key != -1)
            {
                key = SeekNextCoordinate(items, index, minimumTolerance, maximumTolerance);
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