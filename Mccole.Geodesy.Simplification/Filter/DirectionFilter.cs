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
    /// An algorithm to simplify a polyline of ICoordinate(s) by retaining only changes in direction.
    /// </summary>
    public class DirectionFilter : FilterBase
    {
        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) by only retaining points where the change in direction is more than the defined variation.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="variation">Any change in direction less than this will be ignored, the interim point(s) will not be retained.</param>
        /// <returns></returns>
        public static IEnumerable<ICoordinate> Simplify(IEnumerable<ICoordinate> items, double variation)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }
            if (variation < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(variation), "The argument cannot be less than zero.");
            }

            List<ICoordinate> results = new List<ICoordinate>();
            results.Add(items.ElementAt(0));

            for (int i = 1; i < items.Count() - 2; i++)
            {
                ICoordinate previous = results.Last();
                ICoordinate current = items.ElementAt(i);
                ICoordinate next = items.ElementAt(i + 1);

                var b1 = Math.Abs(GeodeticCalculator.Instance.Bearing(previous, current));
                var b2 = Math.Abs(GeodeticCalculator.Instance.Bearing(current, next));

                if (b1 < (b2 - variation) || (b2 + variation) < b1)
                {
                    AddIfNotContains(results, current);
                }
            }

            AddLastIfNotContains(results, items);
            return results;
        }
    }
}