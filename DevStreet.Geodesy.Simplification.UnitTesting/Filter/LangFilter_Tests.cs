using DevStreet.Geodesy.Extension;
using DevStreet.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DevStreet.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class LangFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            double tolerance = 2;

            LangFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            double tolerance = -1;

            LangFilter.Simplify(items, tolerance);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             * http://psimpl.sourceforge.net/lang.html
             *                                                       H
             *              C
             *      B               D
             *
             * A                                                G
             *                                          F
             *                           E
             *
             */

            /*
             * A and H are retained as they are the start and end.
             * Testing the line A->E D is outside the tolerance.
             * Testing the line A->D both B and C are within the tolerance, they're removed.
             * Testing the line D->H G is outside the tolerance.
             * Testing the line D->G F is outside the tolerance.
             * Testing the line D->F E is within the tolerance and is removed.
             * Testing the line F->H G is within the tolerance and is removed.
             */

            var A = new Coordinate(0, 0);
            var B = new Coordinate(0.002, 0.002);
            var C = new Coordinate(0.003, 0.004);
            var D = new Coordinate(0.002, 0.0065);
            var E = new Coordinate(-0.002, 0.008);
            var F = new Coordinate(-0.001, 0.011);
            var G = new Coordinate(0, 0.013);
            var H = new Coordinate(0.004, 0.014);

            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(A);
            items.Add(B);
            items.Add(C);
            items.Add(D);
            items.Add(E);
            items.Add(F);
            items.Add(G);
            items.Add(H);

            double tolerance = 350;

            List<ICoordinate> result = new List<ICoordinate>(LangFilter.Simplify(items, tolerance));

            Assert.AreEqual(A, result[0]);
            Assert.AreEqual(D, result[1]);
            Assert.AreEqual(F, result[2]);
            Assert.AreEqual(H, result[3]);
        }
    }
}
