using DevStreet.Geodesy.Calculator;
using DevStreet.Geodesy.Extension;
using DevStreet.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DevStreet.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class OpheimFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);

            OpheimFilter.Simplify(items, 5, 25);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumTolerance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double minimumTolerance = 2;
            double maximumTolerance = -1;

            OpheimFilter.Simplify(items, minimumTolerance, maximumTolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MinimumTolerance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double minimumTolerance = -1;
            double maximumTolerance = 2;

            OpheimFilter.Simplify(items, minimumTolerance, maximumTolerance);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             * http://psimpl.sourceforge.net/opheim.html
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
             * The same set-up as Reumann-Witkam.
             * In this case the when testing the plane B->C while both D and E are within the minimum tolerance
             * the distance from B to pointX (the perpendicular point of E to the B->C plane) is greater than the
             * maximum tolerance so the point prior to E (i.e. D) is retained.
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

            double minimumTolerance = 175;
            double maximumTolerance = 330;

            List<ICoordinate> result = new List<ICoordinate>(OpheimFilter.Simplify(items, minimumTolerance, maximumTolerance));

            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(B, result[1]);
            Assert.AreEqual(D, result[2]);
            Assert.AreEqual(E, result[3]);
            Assert.AreEqual(F, result[4]);
            Assert.AreEqual(G, result[5]);
            Assert.AreEqual(H, result[6]);
            Assert.AreEqual(I, result[7]);
        }
    }
}
