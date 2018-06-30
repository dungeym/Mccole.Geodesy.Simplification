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
    /// An implementation of the Radial Distance algorithm for simplifying a polyline.
    /// </summary>
    public class RadialDistanceFilter : FilterBase
    {
        /*
         * http://psimpl.sourceforge.net/radial-distance.html
         *
         * Radial Distance is a brute force O(n) algorithm for polyline simplification. It reduces successive
         * vertices that are clustered too closely to a single vertex, called a key.
         * The resulting keys form the simplified polyline.
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

            /*
             * The first and last vertices are always part of the simplification, and are thus marked as keys.  Starting at the first key (the first vertex),
             * the algorithm walks along the polyline.  All consecutive vertices that fall within a specified distance tolerance from that key are removed.
             * The first encountered vertex that lies further away than the tolerance is marked as a key.  Starting from this new key, the algorithm will
             * start walking again and repeat this process, until it reaches the final key (the last vertex).
             */

            ICoordinate pointA = items.First();
            ICoordinate pointB = null;
            List<ICoordinate> results = new List<ICoordinate>();
            results.Add(pointA);

            for (int i = 1; i < items.Count(); i++)
            {
                pointB = items.ElementAt(i);

                double distance = GeodeticCalculator.Instance.Distance(pointA, pointB);
                if (distance >= tolerance)
                {
                    results.Add(pointB);
                    pointA = pointB;
                }
            }

            AddLastIfNotContains(results, items);
            return results;
        }
    }
}