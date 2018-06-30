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
    /// Base filter to simplifying a polyline.
    /// </summary>
    public class FilterBase
    {
        /// <summary>
        /// Base filter to simplifying a polyline.
        /// </summary>

        protected FilterBase()
        {
        }

        /// <summary>
        /// Add the item to the collection if it doesn't already contain it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>

        protected static void AddIfNotContains<T>(ICollection<T> collection, T item) where T : class
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection), "The argument cannot be null.");
            }
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "The argument cannot be null.");
            }

            if (collection.Contains(item) == false)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Add the last item in items to the supplied collection if the collection doesn't already contain the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>

        protected static void AddLastIfNotContains<T>(ICollection<T> collection, IEnumerable<T> items) where T : class
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }

            AddIfNotContains(collection, items.Last());
        }

        /// <summary>
        /// Create an enumeration of SequentialCoordinate objects containing the supplied ICoordinate objects.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>

        public static IEnumerable<SequentialCoordinate> CastToSequentialCoordinate(IEnumerable<ICoordinate> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }

            List<SequentialCoordinate> source = new List<SequentialCoordinate>();
            for (int i = 0; i < items.Count(); i++)
            {
                source.Add(new SequentialCoordinate(i, items.ElementAt(i)));
            }

            return source;
        }
    }
}
