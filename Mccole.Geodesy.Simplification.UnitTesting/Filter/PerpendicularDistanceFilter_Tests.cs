using Mccole.Geodesy.Calculator;
using Mccole.Geodesy.Extension;
using Mccole.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Mccole.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class PerpendicularDistanceFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            double tolerance = 2;

            PerpendicularDistanceFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double tolerance = -1;

            PerpendicularDistanceFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             * http://psimpl.sourceforge.net/perpendicular-distance.html
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
             * The same set-up as Reumann-Witkam and Opheim.
             * When testing the distance to the line B->D for C it's within the tolerance so the point C is removed.
             * When testing the distance to the line F->H for G it's within the tolerance so the point G is removed.
             * All other points are retained.
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

            double tolerance = 120;

            List<ICoordinate> result = new List<ICoordinate>(PerpendicularDistanceFilter.Simplify(items, tolerance));

            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(B, result[1]);
            Assert.AreEqual(D, result[2]);
            Assert.AreEqual(E, result[3]);
            Assert.AreEqual(F, result[4]);
            Assert.AreEqual(H, result[5]);
            Assert.AreEqual(I, result[6]);
        }
    }
}
