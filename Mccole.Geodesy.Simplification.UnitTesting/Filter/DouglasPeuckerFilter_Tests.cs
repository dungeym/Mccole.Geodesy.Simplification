using Mccole.Geodesy.Calculator;
using Mccole.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Mccole.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class DouglasPeuckerFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            double tolerance = 2;

            DouglasPeuckerFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double tolerance = -1;

            DouglasPeuckerFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             * http://psimpl.sourceforge.net/douglas-peucker.html
             *
             *              A
             *
             *    a1
             *          a2
             * X                                    Y
             *                              b1
             *                                  b2
             *                          B
             */

            /*
             * A is furtherest from X->Y.
             * X->A
             * - the distance of a1 and a2 from X->A is less than the tolerance.
             * - these should be removed.
             * A->Y
             * - the distance of B from A->Y is greater than the tolerance.
             * - B is retained.
             * A->B
             * - there's no points here.
             * B->Y
             * - the distance of b1 and b2 from B->Y is less than the tolerance.
             * - these should be removed.
             */

            var X = new Coordinate(0, 0);
            var a1 = new Coordinate(0.00012, 0.00008);
            var a2 = new Coordinate(0.0001, 0.00012);
            var A = new Coordinate(0.0004, 0.0004);
            var B = new Coordinate(-0.0003, 0.0007);
            var b1 = new Coordinate(-0.00018, 0.0008);
            var b2 = new Coordinate(-0.00011, 0.0009);
            var Y = new Coordinate(0, 0.001);

            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(X);
            items.Add(a1);
            items.Add(a2);
            items.Add(A);
            items.Add(B);
            items.Add(b1);
            items.Add(b2);
            items.Add(Y);

            double tolerance = 12;

            List<ICoordinate> result = new List<ICoordinate>(DouglasPeuckerFilter.Simplify(items, tolerance));

            // X and Y are retained as the start and end points.
            Assert.AreEqual(X, result[0]);
            Assert.AreEqual(A, result[1]);
            Assert.AreEqual(B, result[2]);
            Assert.AreEqual(Y, result[3]);

            // Confirmation.
            Assert.IsTrue(Math.Abs(GeodeticCalculator.Instance.CrossTrackDistance(A, X, Y)) > Math.Abs(GeodeticCalculator.Instance.CrossTrackDistance(B, X, Y)));
            Assert.IsTrue(GeodeticCalculator.Instance.CrossTrackDistance(a1, X, A) < tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.CrossTrackDistance(a2, X, A) < tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.CrossTrackDistance(B, A, Y) > tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.CrossTrackDistance(b1, B, Y) < tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.CrossTrackDistance(b2, B, Y) < tolerance);
        }
    }
}
