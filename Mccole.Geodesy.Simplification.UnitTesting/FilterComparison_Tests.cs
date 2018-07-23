using Mccole.Geodesy.Simplification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mccole.Geodesy.Simplification.UnitTesting
{
    [TestClass]
    public class FilterComparison_Tests
    {
        [TestMethod]
        public void ICoordinate_Quantify_Filter_DoesNot_Match_Source_Assert()
        {
            ICoordinate a = new Coordinate(0, 0);
            ICoordinate b = new Coordinate(1, 1);
            ICoordinate c = new Coordinate(2, 2);
            ICoordinate d = new Coordinate(3, 3);
            ICoordinate e = new Coordinate(4, 4);
            List<ICoordinate> filtered = new List<ICoordinate>(new ICoordinate[] { b, d, e });
            List<ICoordinate> source = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });

            IEnumerable<FilteredCoordinate<ICoordinate>> result = FilterAnalyser.Quantify<ICoordinate>( source, filtered);

            Assert.AreEqual(FilteredCoordinateState.Excluded, result.ElementAt(0).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(1).State);
            Assert.AreEqual(FilteredCoordinateState.Excluded, result.ElementAt(2).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(3).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(4).State);
        }

        [TestMethod]
        public void ICoordinate_Compare_Filter_Matches_Source_All_Included_Assert()
        {
            ICoordinate a = new Coordinate(0, 0);
            ICoordinate b = new Coordinate(1, 1);
            ICoordinate c = new Coordinate(2, 2);
            ICoordinate d = new Coordinate(3, 3);
            ICoordinate e = new Coordinate(4, 4);
            List<ICoordinate> filtered = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });
            List<ICoordinate> source = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });

            IEnumerable<FilteredCoordinate<ICoordinate>> result = FilterAnalyser.Quantify<ICoordinate>(source, filtered);

            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(0).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(1).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(2).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(3).State);
            Assert.AreEqual(FilteredCoordinateState.Included, result.ElementAt(4).State);
        }

        [TestMethod]
        public void ICoordinate_Compare_Filter_Matches_Source_Objects_Match_Assert()
        {
            ICoordinate a = new Coordinate(0, 0);
            ICoordinate b = new Coordinate(1, 1);
            ICoordinate c = new Coordinate(2, 2);
            ICoordinate d = new Coordinate(3, 3);
            ICoordinate e = new Coordinate(4, 4);
            List<ICoordinate> filtered = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });
            List<ICoordinate> source = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });

            IEnumerable<FilteredCoordinate<ICoordinate>> result = FilterAnalyser.Quantify<ICoordinate>(source, filtered);

            Assert.IsTrue(object.ReferenceEquals(a, result.ElementAt(0).Coordinate));
            Assert.IsTrue(object.ReferenceEquals(b, result.ElementAt(1).Coordinate));
            Assert.IsTrue(object.ReferenceEquals(c, result.ElementAt(2).Coordinate));
            Assert.IsTrue(object.ReferenceEquals(d, result.ElementAt(3).Coordinate));
            Assert.IsTrue(object.ReferenceEquals(e, result.ElementAt(4).Coordinate));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ICoordinate_Compare_Filtered_MoreThan_Source_ThrowsException()
        {
            List<ICoordinate> filtered = new List<ICoordinate>();
            filtered.Add(new Coordinate(0, 0));
            filtered.Add(new Coordinate(1, 1));
            List<ICoordinate> source = new List<ICoordinate>();
            source.Add(new Coordinate(0, 0));

            FilterAnalyser.Quantify<ICoordinate>(source, filtered);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ICoordinate_Compare_Filtered_Null_ThrowsException()
        {
            List<ICoordinate> source = new List<ICoordinate>();

            FilterAnalyser.Quantify<ICoordinate>(null, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ICoordinate_Compare_Source_Null_ThrowsException()
        {
            List<ICoordinate> filtered = new List<ICoordinate>();

            FilterAnalyser.Quantify<ICoordinate>(filtered, null);
        }

        //[TestMethod]
        //public void ITrackpoint_Compare_Filter_DoesNot_Match_Source_Assert()
        //{
        //    ICoordinate a = new Trackpoint(0, 0, DateTime.Now);
        //    ICoordinate b = new Trackpoint(1, 1, DateTime.Now);
        //    ICoordinate c = new Trackpoint(2, 2, DateTime.Now);
        //    ICoordinate d = new Trackpoint(3, 3, DateTime.Now);
        //    ICoordinate e = new Trackpoint(4, 4, DateTime.Now);
        //    ICoordinate f = new Trackpoint(5, 5, DateTime.Now);
        //    ICoordinate g = new Trackpoint(6, 6, DateTime.Now);
        //    ICoordinate h = new Trackpoint(7, 7, DateTime.Now);
        //    ICoordinate i = new Trackpoint(8, 8, DateTime.Now);
        //    ICoordinate j = new Trackpoint(9, 9, DateTime.Now);
        //    List<ICoordinate> filtered = new List<ICoordinate>(new ICoordinate[] { a, b, c, f, g, i, j });
        //    List<ICoordinate> source = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e, f, g, h, i, j });

        //    IEnumerable<FilteredTrackpoint<ICoordinate>> result = FilterComparison.Compare<ICoordinate>(source, filtered);

        //    Assert.AreEqual(FilteredTrackpointState.Start, result.ElementAt(0).State);
        //    Assert.AreEqual(FilteredTrackpointState.Point, result.ElementAt(1).State);
        //    Assert.AreEqual(FilteredTrackpointState.End, result.ElementAt(2).State);
        //    Assert.AreEqual(FilteredTrackpointState.None, result.ElementAt(3).State);
        //    Assert.AreEqual(FilteredTrackpointState.None, result.ElementAt(4).State);
        //    Assert.AreEqual(FilteredTrackpointState.Start, result.ElementAt(5).State);
        //    Assert.AreEqual(FilteredTrackpointState.End, result.ElementAt(6).State);
        //    Assert.AreEqual(FilteredTrackpointState.None, result.ElementAt(7).State);
        //    Assert.AreEqual(FilteredTrackpointState.Start, result.ElementAt(8).State);
        //    Assert.AreEqual(FilteredTrackpointState.End, result.ElementAt(9).State);
        //}

        //[TestMethod]
        //public void ITrackpoint_Compare_Filter_Matches_Source_Assert()
        //{
        //    ICoordinate a = new Trackpoint(0, 0, DateTime.Now);
        //    ICoordinate b = new Trackpoint(1, 1, DateTime.Now);
        //    ICoordinate c = new Trackpoint(2, 2, DateTime.Now);
        //    ICoordinate d = new Trackpoint(3, 3, DateTime.Now);
        //    ICoordinate e = new Trackpoint(4, 4, DateTime.Now);
        //    List<ICoordinate> filtered = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });
        //    List<ICoordinate> source = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });

        //    IEnumerable<FilteredTrackpoint<ICoordinate>> result = FilterComparison.Compare<ICoordinate>(source, filtered);

        //    Assert.AreEqual(FilteredTrackpointState.Start, result.ElementAt(0).State);
        //    Assert.AreEqual(FilteredTrackpointState.Point, result.ElementAt(1).State);
        //    Assert.AreEqual(FilteredTrackpointState.Point, result.ElementAt(2).State);
        //    Assert.AreEqual(FilteredTrackpointState.Point, result.ElementAt(3).State);
        //    Assert.AreEqual(FilteredTrackpointState.End, result.ElementAt(4).State);
        //}

        //[TestMethod]
        //public void ITrackpoint_Compare_Filter_Matches_Source_Objects_Match_Assert()
        //{
        //    ICoordinate a = new Trackpoint(0, 0, DateTime.Now);
        //    ICoordinate b = new Trackpoint(1, 1, DateTime.Now);
        //    ICoordinate c = new Trackpoint(2, 2, DateTime.Now);
        //    ICoordinate d = new Trackpoint(3, 3, DateTime.Now);
        //    ICoordinate e = new Trackpoint(4, 4, DateTime.Now);
        //    List<ICoordinate> filtered = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });
        //    List<ICoordinate> source = new List<ICoordinate>(new ICoordinate[] { a, b, c, d, e });

        //    IEnumerable<FilteredTrackpoint<ICoordinate>> result = FilterComparison.Compare<ICoordinate>(source, filtered);

        //    Assert.IsTrue(object.ReferenceEquals(a, result.ElementAt(0).Trackpoint));
        //    Assert.IsTrue(object.ReferenceEquals(b, result.ElementAt(1).Trackpoint));
        //    Assert.IsTrue(object.ReferenceEquals(c, result.ElementAt(2).Trackpoint));
        //    Assert.IsTrue(object.ReferenceEquals(d, result.ElementAt(3).Trackpoint));
        //    Assert.IsTrue(object.ReferenceEquals(e, result.ElementAt(4).Trackpoint));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void ITrackpoint_Compare_Filtered_MoreThan_Source_ThrowsException()
        //{
        //    List<ICoordinate> filtered = new List<ICoordinate>();
        //    filtered.Add(new Trackpoint(0, 0, DateTime.Now));
        //    filtered.Add(new Trackpoint(1, 1, DateTime.Now));
        //    List<ICoordinate> source = new List<ICoordinate>();
        //    source.Add(new Trackpoint(0, 0, DateTime.Now));

        //    FilterComparison.Compare<ICoordinate>(source, filtered);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ITrackpoint_Compare_Filtered_Null_ThrowsException()
        //{
        //    List<ICoordinate> source = new List<ICoordinate>();

        //    FilterComparison.Compare<ICoordinate>(null, source);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ITrackpoint_Compare_Source_Null_ThrowsException()
        //{
        //    List<ICoordinate> filtered = new List<ICoordinate>();

        //    FilterComparison.Compare<ICoordinate>(filtered, null);
        //}
    }
}
