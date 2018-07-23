using Mccole.Geodesy.Calculator;
using Mccole.Geodesy.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mccole.Geodesy.Simplification.Filter
{
    /// <summary>
    /// An implementation of the Perpendicular Distance algorithm for simplifying a polyline.
    /// </summary>
    public class PerpendicularDistanceFilter : FilterBase
    {
        /*
         * http://psimpl.sourceforge.net/perpendicular-distance.html
         *
         * Instead of using a point-to-point (radial) distance tolerance as a rejection criterion (see Radial Distance),
         * the O(n) Perpendicular Distance routine uses a point-to-segment distance tolerance. For each vertex vi,
         * its perpendicular distance to the line segment S(vi-1, vi+1) is computed. All vertices whose distance is
         * smaller than the given tolerance will be removed.
         */

        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) using the Radial Distance algorithm and the defined tolerance.
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

            int index = 1;
            while (index != items.Count() - 1)
            {
                var vm1 = index - 1;
                var vp1 = index + 1;
                var d = GeodeticCalculator.Instance.DistanceToLine(items.ElementAt(vm1), items.ElementAt(vp1), items.ElementAt(index));
                if (d > tolerance || d.WithinTolerance(-1))
                {
                    results.Add(items.ElementAt(index));
                    index += 1;
                }
                else
                {
                    results.Add(items.ElementAt(vp1));
                    index += 2;
                }
            }

            AddLastIfNotContains(results, items);
            return results;
        }
    }
}