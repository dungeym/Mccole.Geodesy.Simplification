using DevStreet.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevStreet.Geodesy.Simplification.UnitTesting
{
    [TestClass]
    public class StoppageFilter_Tests
    {
        [TestMethod]
        public void CloseProximity_Timestamp_Assert()
        {
            /*
             * All the timestamps are unique.
             * Longitude to 3rd decimal place is roughly equal to 110m.
             *
             * Evaluate 2 points:
             * - where the difference between them is less than the 'minimumProximity' the 2nd point should be ignored
             * - the first point should be re-used when evaluating the point that is after the '2nd point'
             * Where the 2 points are more than the 'minimumProximity' both should be retained.
             */

            var A = new Trackpoint(0, 0.000, new DateTime(2018, 06, 12, 13, 14, 15, 0));
            var B = new Trackpoint(0, 0.001, new DateTime(2018, 06, 12, 13, 14, 16, 0));
            var C = new Trackpoint(0, 0.002, new DateTime(2018, 06, 12, 13, 14, 17, 0));
            var D = new Trackpoint(0, 0.0025, new DateTime(2018, 06, 12, 13, 14, 18, 0));
            var E = new Trackpoint(0, 0.004, new DateTime(2018, 06, 12, 13, 14, 19, 0));
            var F = new Trackpoint(0, 0.005, new DateTime(2018, 06, 12, 13, 14, 20, 0));
            var G = new Trackpoint(0, 0.0055, new DateTime(2018, 06, 12, 13, 14, 21, 0));
            var H = new Trackpoint(0, 0.0058, new DateTime(2018, 06, 12, 13, 14, 22, 0));
            var I = new Trackpoint(0, 0.006, new DateTime(2018, 06, 12, 13, 14, 23, 0));

            List<ITrackpoint> items = new List<ITrackpoint>();
            items.Add(A);
            items.Add(B);
            items.Add(C);
            items.Add(D);
            items.Add(E);
            items.Add(F);
            items.Add(G);
            items.Add(H);
            items.Add(I);

            IEnumerable<ITrackpoint> result = StoppageFilter.Simplify(items, 105, StoppageFilter.ConvertKphToMps(5), 20);

            Assert.AreEqual(A, result.ElementAt(0));
            Assert.AreEqual(B, result.ElementAt(1));
            Assert.AreEqual(C, result.ElementAt(2));
            Assert.AreEqual(E, result.ElementAt(3));
            Assert.AreEqual(F, result.ElementAt(4));
            Assert.AreEqual(I, result.ElementAt(5));
        }

        [TestMethod]
        public void Duplicate_Timestamp_Assert()
        {
            /*
             * The points are roughly 110m apart.
             *
             * Some of the timestamps are exactly the same, where they are only the first instance should be retained.
             * All other unique timestamps should be retained.
             */

            var A = new Trackpoint(0, 0.000, new DateTime(2018, 06, 12, 13, 14, 15, 0));
            var B = new Trackpoint(0, 0.001, new DateTime(2018, 06, 12, 13, 14, 16, 0));
            var C = new Trackpoint(0, 0.002, new DateTime(2018, 06, 12, 13, 14, 17, 0));
            var D = new Trackpoint(0, 0.003, new DateTime(2018, 06, 12, 13, 14, 17, 0));
            var E = new Trackpoint(0, 0.004, new DateTime(2018, 06, 12, 13, 14, 18, 0));
            var F = new Trackpoint(0, 0.005, new DateTime(2018, 06, 12, 13, 14, 19, 0));
            var G = new Trackpoint(0, 0.006, new DateTime(2018, 06, 12, 13, 14, 19, 0));
            var H = new Trackpoint(0, 0.007, new DateTime(2018, 06, 12, 13, 14, 19, 0));
            var I = new Trackpoint(0, 0.009, new DateTime(2018, 06, 12, 13, 14, 20, 0));

            List<ITrackpoint> items = new List<ITrackpoint>();
            items.Add(A);
            items.Add(B);
            items.Add(C);
            items.Add(D);
            items.Add(E);
            items.Add(F);
            items.Add(G);
            items.Add(H);
            items.Add(I);

            IEnumerable<ITrackpoint> result = StoppageFilter.Simplify(items, 5, StoppageFilter.ConvertKphToMps(5), 20);

            Assert.AreEqual(A, result.ElementAt(0));
            Assert.AreEqual(B, result.ElementAt(1));
            Assert.AreEqual(C, result.ElementAt(2));
            Assert.AreEqual(E, result.ElementAt(3));
            Assert.AreEqual(F, result.ElementAt(4));
            Assert.AreEqual(I, result.ElementAt(5));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ITrackpoint> items = default(IEnumerable<ITrackpoint>);

            StoppageFilter.Simplify(items, 5, 25, 150);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ITrackpoint> items = new List<ITrackpoint>();
            items.Add(new Trackpoint(0, 0.008, new DateTime(2018, 06, 12, 13, 14, 23, 0)));
            double minimumProximity = 2;
            double minimumSpeed = 2;
            double maximumDistance = -1;

            StoppageFilter.Simplify(items, minimumProximity, minimumSpeed, maximumDistance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MinimumProximity_Negative_ThrowsException()
        {
            List<ITrackpoint> items = new List<ITrackpoint>();
            items.Add(new Trackpoint(0, 0.008, new DateTime(2018, 06, 12, 13, 14, 23, 0)));
            double minimumProximity = -1;
            double minimumSpeed = 2;
            double maximumDistance = 2;

            StoppageFilter.Simplify(items, minimumProximity, minimumSpeed, maximumDistance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MinimumSpeed_Negative_ThrowsException()
        {
            List<ITrackpoint> items = new List<ITrackpoint>();
            items.Add(new Trackpoint(0, 0.008, new DateTime(2018, 06, 12, 13, 14, 23, 0)));
            double minimumProximity = 2;
            double minimumSpeed = -1;
            double maximumDistance = 2;

            StoppageFilter.Simplify(items, minimumProximity, minimumSpeed, maximumDistance);
        }

        [TestMethod]
        public void Simplify_Timestamp_Assert()
        {
            /*
             * All the points are more than the 'minimumProximity' a part.
             * All the points have a unique timestamp.
             *
             * A 'Stoppage' is:
             * - (a) where the speed between 2 points is less than minimumSpeed, and
             * - (b) where the distance between those 2 points is not less than the maximumDistance.
             *
             *
             * A        B       C D       E       F G H       I
             * - A->B - speed above the minimum, keep A and B.
             * - B->C - speed above the minimum, keep B and C.
             * - C->D - speed below the minimum and the distance B->D is less than the maximum, keep C, ignore D.
             * - D->E - speed above the minimum, D is already ignored, keep E.
             * - E->F - speed above the minimum, keep E and F.
             * - F->G - speed below the minimum and the distance E->G is less than the maximum, keep F, ignore G.
             * - G->H - speed below the minimum and the distance E->H is less than the maximum, G is already ignored, ignore H.
             * - H->I - speed above the minimum, H is already ignored, keep I.
             */

            var A = new Trackpoint(0, 0.000, new DateTime(2018, 06, 12, 13, 14, 15, 0));
            var B = new Trackpoint(0, 0.001, new DateTime(2018, 06, 12, 13, 14, 16, 0));
            var C = new Trackpoint(0, 0.002, new DateTime(2018, 06, 12, 13, 14, 17, 0));
            var D = new Trackpoint(0, 0.0022, new DateTime(2018, 06, 12, 13, 14, 18, 0));
            var E = new Trackpoint(0, 0.004, new DateTime(2018, 06, 12, 13, 14, 19, 0));
            var F = new Trackpoint(0, 0.005, new DateTime(2018, 06, 12, 13, 14, 20, 0));
            var G = new Trackpoint(0, 0.0052, new DateTime(2018, 06, 12, 13, 14, 21, 0));
            var H = new Trackpoint(0, 0.0053, new DateTime(2018, 06, 12, 13, 14, 22, 0));
            var I = new Trackpoint(0, 0.008, new DateTime(2018, 06, 12, 13, 14, 23, 0));

            List<ITrackpoint> items = new List<ITrackpoint>();
            items.Add(A);
            items.Add(B);
            items.Add(C);
            items.Add(D);
            items.Add(E);
            items.Add(F);
            items.Add(G);
            items.Add(H);
            items.Add(I);

            IEnumerable<ITrackpoint> result = StoppageFilter.Simplify(items, 5, 25, 150);

            Assert.AreEqual(A, result.ElementAt(0));
            Assert.AreEqual(B, result.ElementAt(1));
            Assert.AreEqual(C, result.ElementAt(2));
            Assert.AreEqual(E, result.ElementAt(3));
            Assert.AreEqual(F, result.ElementAt(4));
            Assert.AreEqual(I, result.ElementAt(5));
        }
    }
}
