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
    /// An implementation of the Douglas–Peucker algorithm for simplifying a polyline.
    /// </summary>
    public class DouglasPeuckerFilter : FilterBase
    {
        /*
         * https://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm
         * http://psimpl.sourceforge.net/douglas-peucker.html
         *
         * The Douglas-Peucker algorithm uses a point-to-edge distance tolerance. The algorithm starts with a crude
         * simplification that is the single edge joining the first and last vertices of the original polyline. It
         * then computes the distance of all intermediate vertices to that edge. The vertex that is furthest away
         * from that edge, and that has a computed distance that is larger than a specified tolerance, will be
         * marked as a key and added to the simplification. This process will recurse for each edge in the current
         * simplification, until all vertices of the original polyline are within tolerance of the simplification results
         */

        private static void Simplify(IEnumerable<SequentialCoordinate> source, int first, int last, double tolerance, ref List<SequentialCoordinate> results)
        {
            double maximumDistance = 0;
            int indexToKeep = 0;

            // Find the index of the element that is furtherest from the line.
            for (int index = first; index < last; index++)
            {
                double distance = Math.Abs(GeodeticCalculator.Instance.CrossTrackDistance(source.ElementAt(index), source.ElementAt(first), source.ElementAt(last)));
                if (distance > maximumDistance)
                {
                    maximumDistance = distance;
                    indexToKeep = index;
                }
            }

            // Where the point found is greater than the tolerance distance from the line, keep it and keep searching.
            if (maximumDistance > tolerance && indexToKeep != 0)
            {
                results.Add(source.ElementAt(indexToKeep));

                Simplify(source, first, indexToKeep, tolerance, ref results);
                Simplify(source, indexToKeep, last, tolerance, ref results);
            }
        }

        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) using the Douglas–Peucker algorithm and the defined tolerance.
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

            // Use SequentialCoordinate so we can re-sort them after.
            IEnumerable<SequentialCoordinate> source = CastToSequentialCoordinate(items);

            // Keep first.
            List<SequentialCoordinate> results = new List<SequentialCoordinate>();
            results.Add(source.First());

            int first = 0;
            int last = source.Count() - 1;
            Simplify(source, first, last, tolerance, ref results);

            AddLastIfNotContains(results, source);

            // Re-sort the results by the original index value and return.
            results.Sort((point, next) => point.Index.CompareTo(next.Index));
            return results.Select((sc) => sc.Coordinate);
        }
    }
}