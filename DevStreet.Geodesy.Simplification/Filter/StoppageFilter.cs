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
    /// An algorithm to simplify a polyline of ITrackpoint(s) by removing 'stoppages'.
    /// </summary>
    public class StoppageFilter : FilterBase
    {
        /// <summary>
        /// The number of metres in a mile.
        /// </summary>
        public const double MetresInMile = 1609.34;

        private static IEnumerable<ITrackpoint> RemoveCloseProximity(IEnumerable<ITrackpoint> items, double minimumProximity)
        {
            return RadialDistanceFilter.Simplify(items, minimumProximity).Cast<ITrackpoint>();
        }

        private static IEnumerable<ITrackpoint> RemoveDuplicateTimestamp(IEnumerable<ITrackpoint> items)
        {
            List<ITrackpoint> results = new List<ITrackpoint>();

            ITrackpoint current = items.ElementAt(0);
            ITrackpoint next;
            for (int i = 1; i < items.Count(); i++)
            {
                next = items.ElementAt(i);

                if (current.Timestamp.Equals(next.Timestamp) == false)
                {
                    results.Add(current);
                    current = next;

                    if (i == items.Count() - 1)
                    {
                        results.Add(next);
                    }
                }
            }

            return results;
        }

        private static IEnumerable<ITrackpoint> RemoveStoppages(IEnumerable<ITrackpoint> items, double minimumSpeed, double maximumDistance)
        {
            List<ITrackpoint> results = new List<ITrackpoint>();

            ITrackpoint previous = items.ElementAt(0);
            ITrackpoint ignore = default(ITrackpoint);

            results.Add(previous);

            ITrackpoint current;
            ITrackpoint next;
            for (int i = 0; i < items.Count() - 1; i++)
            {
                current = items.ElementAt(i);
                next = items.ElementAt(i + 1);

                var distanceCurrent2Next = GeodeticCalculator.Instance.Distance(current, next);
                var time = (next.Timestamp - current.Timestamp).TotalSeconds;
                var speed = distanceCurrent2Next / time;

                if (speed > minimumSpeed)
                {
                    if (current.Equals(ignore) == false)
                    {
                        AddIfNotContains(results, current);
                    }

                    AddIfNotContains(results, next);

                    previous = current;
                    ignore = default(ITrackpoint);
                }
                else
                {
                    var distancePrevious2Next = GeodeticCalculator.Instance.Distance(previous, next);
                    if (distancePrevious2Next > maximumDistance)
                    {
                        AddIfNotContains(results, current);
                        AddIfNotContains(results, next);
                        ignore = default(ITrackpoint);
                    }
                    else
                    {
                        ignore = next;
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Convert a KPH value to MPS (Metres per Second).
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static double ConvertKphToMps(double speed)
        {
            double metres = speed * 1000;
            double seconds = (60 * 60);
            return metres / seconds;
        }

        /// <summary>
        /// Convert an MPH value to MPS (Metres per Second).
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static double ConvertMphToMps(double speed)
        {
            double metres = speed * MetresInMile;
            double seconds = (60 * 60);
            return metres / seconds;
        }

        /// <summary>
        /// Simplify the supplied polyline of ITrackpoint(s) using a combination of techniques to identify and 
        /// remove clusters of ITrackpoint(s) that represent points at which the movement stopped.
        /// <para>Where a cluster of points have the same Timestamp only the first point is retained.</para>
        /// <para>The Radial Distance algorithm is used to remove points in close proximity.</para>
        /// <para>When the speed between one point and the next is below the minimum and the distance is below the minimum, the 'next' point is not retained.</para>
        /// </summary>
        /// <param name="items"></param>
        /// <param name="minimumProximity">The minimum distance a point must be from the next, Radial Distance tolerance.</param>
        /// <param name="minimumSpeed">The minimum speed between points.</param>
        /// <param name="minimumDistance">The minimum distance between low-speed points.</param>
        /// <returns></returns>
        public static IEnumerable<ITrackpoint> Simplify(IEnumerable<ITrackpoint> items, double minimumProximity, double minimumSpeed, double minimumDistance)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "The argument cannot be null.");
            }
            if (minimumProximity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumProximity), "The argument cannot be less than zero.");
            }
            if (minimumSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumSpeed), "The argument cannot be less than zero.");
            }
            if (minimumDistance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumDistance), "The argument cannot be less than zero.");
            }

            /*
             * Remove duplicates by timestamp.
             *
             * Remove those points that are too close to each other (this is often the case when stopped as
             * the GPS continues to record by will 'bounce' around a bit).
             *
             * Using the remaining points calculate the speed between one point and the next.  If the speed is
             * below the minimum speed then calculate the distance between the last point retained and the 'next',
             * if this distance is below the minimum distance then remove the point.  The idea here is that if
             * you were intentionally travelling at below the minimum speed then we'd retain points near the
             * minimum distance from each other to capture this.
             */

            IEnumerable<ITrackpoint> uniqueItems = RemoveDuplicateTimestamp(items);
            IEnumerable<ITrackpoint> sparseItems = RemoveCloseProximity(uniqueItems, minimumProximity);
            IEnumerable<ITrackpoint> results = RemoveStoppages(sparseItems, minimumSpeed, minimumDistance);
            return results;
        }
    }
}
