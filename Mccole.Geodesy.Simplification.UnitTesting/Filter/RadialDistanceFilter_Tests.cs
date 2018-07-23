using Mccole.Geodesy.Calculator;
using Mccole.Geodesy.Extension;
using Mccole.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Mccole.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class RadialDistanceFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            double tolerance = 2;

            RadialDistanceFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double tolerance = -1;

            RadialDistanceFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             * http://psimpl.sourceforge.net/radial-distance.html
             *                                              E
             *
             *                              C
             *    a1
             * A   a2           B         b1
             *
             *
             *                                         D
             */

            /*
             * A->a1->a2->B->b1->C->D->E
             * A->a1 - a1 is within the tolerance, remove this.
             * A->a2 - a2 is within the tolerance, remove this.
             * A->B - B is outside the tolerance, keep it.
             * B->b1 - b1 is within the tolerance, remove this.
             * B->C - C is outside the tolerance, keep it.
             * C->D - D is outside the tolerance, keep it.
             * D->E - E is outside the tolerance, keep it.
             */

            var A = new Coordinate(0, 0);
            var a1 = new Coordinate(0.0002, 0.00015);
            var a2 = new Coordinate(0.0, 0.0002);
            var B = new Coordinate(0, 0.00045);
            var b1 = new Coordinate(0.00015, 0.00068);
            var C = new Coordinate(0.0003, 0.00070);
            var D = new Coordinate(-0.00045, 0.0009);
            var E = new Coordinate(0.0006, 0.001);

            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(A);
            items.Add(a1);
            items.Add(a2);
            items.Add(B);
            items.Add(b1);
            items.Add(C);
            items.Add(D);
            items.Add(E);

            double tolerance = 34;

            List<ICoordinate> result = new List<ICoordinate>(RadialDistanceFilter.Simplify(items, tolerance));

            // The minor (lower-case) points should be removed, they're within the tolerance.
            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(B, result[1]);
            Assert.AreEqual(C, result[2]);
            Assert.AreEqual(D, result[3]);

            // Confirmation that the distances are less than the tolerance.
            Assert.IsTrue(GeodeticCalculator.Instance.Distance(A, a1) < tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.Distance(A, a2) < tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.Distance(A, B) > tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.Distance(B, b1) < tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.Distance(B, C) > tolerance);
            Assert.IsTrue(GeodeticCalculator.Instance.Distance(C, D) > tolerance);
        }
    }
}
