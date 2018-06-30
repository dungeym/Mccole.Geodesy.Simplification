using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStreet.Geodesy.Simplification
{
    /// <summary>
    /// Compare the source and the filtered to determine how the simplification algorithm acted upon each ICoordinate.
    /// </summary>
    public static class FilterAnalyser
    {
        /// <summary>
        /// Compare the source and the filtered to determine how the simplification algorithm acted upon each ICoordinate.
        /// </summary>
        /// <param name="source">The ICoordinate(s) that were supplied to the simplification algorithm.</param>
        /// /// <param name="filtered">The ICoordinate(s) that were returned from the simplification algorithm.</param>
        /// <returns></returns>
        public static IEnumerable<FilteredCoordinate<T>> Quantify<T>(IEnumerable<ICoordinate> source, IEnumerable<ICoordinate> filtered) where T : ICoordinate
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "The argument cannot be null.");
            }
            if (filtered == null)
            {
                throw new ArgumentNullException(nameof(filtered), "The argument cannot be null.");
            }
            if (filtered.Count() > source.Count())
            {
                throw new ArgumentException("The filtered argument cannot contain more items that the source.");
            }

            int offset = 0;
            List<FilteredCoordinate<T>> result = new List<FilteredCoordinate<T>>();

            foreach (ICoordinate f in filtered)
            {
                ICoordinate s = source.ElementAt(offset++);

                if (f.Equals(s))
                {
                    result.Add(new FilteredCoordinate<T>(result.Count, s, FilteredCoordinateState.Included));
                }
                else
                {
                    // Retain the excluded Source item.
                    result.Add(new FilteredCoordinate<T>(result.Count, s, FilteredCoordinateState.Excluded));

                    // Move the Offset to the index from Source that matches the current Filtered item.
                    bool exitNow = false;
                    while (exitNow == false)
                    {
                        s = source.ElementAt(offset++);

                        if (f.Equals(s) == false)
                        {
                            result.Add(new FilteredCoordinate<T>(result.Count, s, FilteredCoordinateState.Excluded));
                        }
                        else
                        {
                            result.Add(new FilteredCoordinate<T>(result.Count, s, FilteredCoordinateState.Included));
                            exitNow = true;
                        }
                    }
                }
            }

            return result;
        }
    }
}
