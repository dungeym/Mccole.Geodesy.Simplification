using DevStreet.Geodesy.Extension;
using DevStreet.Geodesy.Simplification.Filter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DevStreet.Geodesy.Simplification.UnitTesting.Filter
{
    [TestClass]
    public class NthPointFilter_Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Simplfy_Items_Null_ThrowsException()
        {
            IEnumerable<ICoordinate> items = default(IEnumerable<ICoordinate>);
            int multiple = 2;

            NthPointFilter.Simplify(items, multiple);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Simplfy_MaximumDistance_Negative_ThrowsException()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            items.Add(new Coordinate(0, 0.008));
            int multiple = -1;

            NthPointFilter.Simplify(items, multiple);
        }

        [TestMethod]
        public void Simplify_Valid_Assert()
        {
            List<ICoordinate> items = new List<ICoordinate>();
            for (int i = 0; i < 30; i++)
            {
                items.Add(new Coordinate(i, i));
            }

            List<ICoordinate> result = new List<ICoordinate>(NthPointFilter.Simplify(items, 4));

            // There are 29 items, the first and the last should be kept.
            // The remainder should all have a Latitude that is a multiple of 4 because that's how I seeded the data.
            Assert.IsTrue(result[0].Latitude.WithinTolerance(0));
            Assert.IsTrue(result[1].Latitude.WithinTolerance(4));
            Assert.IsTrue(result[2].Latitude.WithinTolerance(8));
            Assert.IsTrue(result[3].Latitude.WithinTolerance(12));
            Assert.IsTrue(result[4].Latitude.WithinTolerance(16));
            Assert.IsTrue(result[5].Latitude.WithinTolerance(20));
            Assert.IsTrue(result[6].Latitude.WithinTolerance(24));
            Assert.IsTrue(result[7].Latitude.WithinTolerance(28));
            Assert.IsTrue(result[8].Latitude.WithinTolerance(29));
        }
    }
}
