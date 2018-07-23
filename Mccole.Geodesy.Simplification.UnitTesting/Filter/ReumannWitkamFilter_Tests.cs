using Mccole.Geodesy.Calculator;
using Mccole.Geodesy.Extension;
using Mccole.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Mccole.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class ReumannWitkamFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            double tolerance = 2;

            ReumannWitkamFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double tolerance = -1;

            ReumannWitkamFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             * http://psimpl.sourceforge.net/reumann-witkam.html
             *          B           D                               I
             *                  C           E
             *
             * A
             *
             *                                          G
             *                                      F       H
             *
             */

            /*
             * When testing the plane B->C the points D and E are within the tolerance so they are removed.
             * When testing the plane E->F the point G is within the tolerance so it's removed.
             */

            var A = new Coordinate(0, 0);
            var B = new Coordinate(0.005, 0.003);
            var C = new Coordinate(0.0045, 0.005);
            var D = new Coordinate(0.005, 0.006);
            var E = new Coordinate(0.0045, 0.008);
            var F = new Coordinate(-0.002, 0.01);
            var G = new Coordinate(-0.001, 0.02);
            var H = new Coordinate(-0.002, 0.025);
            var I = new Coordinate(0.005, 0.03);

            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(A);
            items.Add(B);
            items.Add(C);
            items.Add(D);
            items.Add(E);
            items.Add(F);
            items.Add(G);
            items.Add(H);
            items.Add(I);

            double tolerance = 175;

            List<ICoordinate> result = new List<ICoordinate>(ReumannWitkamFilter.Simplify(items, tolerance));

            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(B, result[1]);
            Assert.AreEqual(E, result[2]);
            Assert.AreEqual(F, result[3]);
            Assert.AreEqual(H, result[4]);
            Assert.AreEqual(I, result[5]);
        }
    }
}
