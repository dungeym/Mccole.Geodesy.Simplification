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
    /// An implementation of the Lang algorithm for simplifying a polyline.
    /// </summary>
    public class LangFilter : FilterBase
    {
        /*
         * http://psimpl.sourceforge.net/lang.html
         *
         * The Lang simplification algorithm defines a fixed size search-region. The first and last points of that
         * search region form a segment. This segment is used to calculate the perpendicular distance to each
         * intermediate point. If any calculated distance is larger than the specified tolerance, the search region
         * will be shrunk by excluding its last point. This process will continue until all calculated distances
         * fall below the specified tolerance, or when there are no more intermediate points. All intermediate
         * points are removed and a new search region is defined starting at the last point from old search region.
         */

        private static void AddIfNotAlreadyInList(ICollection<ICoordinate> items, ICoordinate point)
        {
            if (items.Contains(point) == false)
            {
                items.Add(point);
            }
        }

        private static bool Evaluate(IEnumerable<ICoordinate> items, double tolerance, int left, int right)
        {
            bool toleranceBreach = false;
            for (int i = right - 1; i > left; i--)
            {
                ICoordinate pointX;
                var d = GeodeticCalculator.Instance.DistanceToLine(items.ElementAt(left), items.ElementAt(right), items.ElementAt(i), out pointX);
                if (pointX == null || d > tolerance)
                {
                    toleranceBreach = true;
                    break;
                }
            }

            return toleranceBreach;
        }

        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) using the Lang algorithm and the defined tolerance.
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

            // Keep the first item.
            results.Add(items.ElementAt(0));

            var step = 4;
            var left = 0;
            var right = step;

            do
            {
                while (left != right && Evaluate(items, tolerance, left, right))
                {
                    right--;
                }

                AddIfNotAlreadyInList(results, items.ElementAt(left));
                AddIfNotAlreadyInList(results, items.ElementAt(right));

                left = right;
                right += step;

                if (right >= items.Count())
                {
                    right = items.Count() - 1;
                }
            } while (left < right);

            AddLastIfNotContains(results, items);
            return results;
        }
    }
}