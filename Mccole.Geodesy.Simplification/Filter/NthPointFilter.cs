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
    /// An implementation of the Nth Point algorithm for simplifying a polyline.
    /// </summary>
    public class NthPointFilter : FilterBase
    {
        /*
         * http://psimpl.sourceforge.net/nth-point.html
         *
         * The nth point routine is a naive O(n) algorithm for polyline simplification.
         * It keeps only the first, last, and each nth point. All other points are removed.
         */

        /// <summary>
        /// Simplify the supplied polyline of ICoordinate(s) using the Nth Point algorithm and the defined multiple.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="multiple">On the first, last and point where index value of the point is a multiple of this value will be retained.</param>
        /// <returns></returns>
        public static IEnumerable<ICoordinate> Simplify(IEnumerable<ICoordinate> items, int multiple)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }
            if (multiple < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(multiple), "The argument cannot be less than zero.");
            }

            List<ICoordinate> results = new List<ICoordinate>();
            results.Add(items.ElementAt(0));

            // Add only the items where the index of the item is a multiple of the value.
            // Exclude the first item, it's already been retained.
            results.AddRange(items.Where((c, i) => { return i != 0 && ((i % multiple) == 0); }));

            AddLastIfNotContains(results, items);
            return results;
        }
    }
}