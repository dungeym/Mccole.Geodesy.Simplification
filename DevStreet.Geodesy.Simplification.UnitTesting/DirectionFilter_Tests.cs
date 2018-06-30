using DevStreet.Geodesy.Extension;
using DevStreet.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevStreet.Geodesy.Simplification.UnitTesting
{
    [TestClass]
    public class DirectionFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            int variation = 2;

            DirectionFilter.Simplify(items, variation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_Variation_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            int variation = -1;

            DirectionFilter.Simplify(items, variation);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            /*
             *
             *                              C
             *                                  D
             *                                      E
             *                                          F
             *                                              G
             *                                                  H
             *                                                      I
             *
             *                B
             *
             *
             *
             *
             *
             *
             * A
             */

            /*
             * A->B->C, there's a slight variation in the bearing between A->B and B-> but this difference is below the
             * configured variation so A->B->C is seen as straight line, B is ignored.
             *
             * A->C->D, (B is ignore so A is re-used as the initial point), there's clearly a change in direction that is
             * greater than the configured variation so C is kept.
             *
             * C->D->E, straight line, D is ignored
             * D,E,F,G and H are also in a straight line so they're ignored.
             * I is kept as it's the final point.
             */

            var A = new Coordinate(0, 0);
            var B = new Coordinate(0.0001, 0.0001);
            var C = new Coordinate(0.00019, 0.0002);
            var D = new Coordinate(0.00018, 0.0003);
            var E = new Coordinate(0.00017, 0.0004);
            var F = new Coordinate(0.00016, 0.0005);
            var G = new Coordinate(0.00015, 0.0006);
            var H = new Coordinate(0.00014, 0.0007);
            var I = new Coordinate(0.00013, 0.0008);

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

            List<ICoordinate> result = new List<ICoordinate>(DirectionFilter.Simplify(items, 4));

            Assert.AreEqual(A, result.ElementAt(0));
            Assert.AreEqual(C, result.ElementAt(1));
            Assert.AreEqual(I, result.ElementAt(2));
        }
    }
}
