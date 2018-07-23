using DevStreet.TestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevStreet.Geodesy.Simplification.UnitTesting
{
    [TestClass]
    public class Waypoint_Tests
    {
        private static double _fixedElevation = 0;

        [TestMethod]
        public void CompareTo_GreaterThan_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left.CompareTo(right);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void CompareTo_InvalidType_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);

            var result = left.CompareTo(DateTime.Now);
        }

        [TestMethod]
        public void CompareTo_LessThan_Assert()
        {
            Waypoint left = new Waypoint(10, 10, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left.CompareTo(right);

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CompareTo_Null_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);

            var result = left.CompareTo(null);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CompareTo_Same_Assert()
        {
            Waypoint left = new Waypoint(10, 10, _fixedElevation);

            var result = left.CompareTo(left);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Constructor_Double_Assert()
        {
            double latitude = TestData.Double();
            double longitude = TestData.Double();

            Waypoint subject = new Waypoint(latitude, longitude, _fixedElevation);

            Assert.AreEqual(latitude, subject.Latitude);
            Assert.AreEqual(longitude, subject.Longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Latitude_Empty_ThrowsException()
        {
            string latitude = string.Empty;
            string longitude = "004 08 02W";

            Waypoint subject = new Waypoint(latitude, longitude, _fixedElevation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Latitude_Null_ThrowsException()
        {
            string latitude = null;
            string longitude = "004 08 02W";

            Waypoint subject = new Waypoint(latitude, longitude, _fixedElevation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Longitude_Empty_ThrowsException()
        {
            string latitude = "50 21 59N";
            string longitude = string.Empty;

            Waypoint subject = new Waypoint(latitude, longitude, _fixedElevation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Longitude_Null_ThrowsException()
        {
            string latitude = "50 21 59N";
            string longitude = null;

            Waypoint subject = new Waypoint(latitude, longitude, _fixedElevation);
        }

        [TestMethod]
        public void Constructor_String_Assert()
        {
            string latitude = "50 21 59N";
            string longitude = "004 08 02W";

            Waypoint subject = new Waypoint(latitude, longitude, _fixedElevation);

            Assert.AreEqual(50.36639, Math.Round(subject.Latitude, 5));
            Assert.AreEqual(-4.13389, Math.Round(subject.Longitude, 5));
        }

        [TestMethod]
        public void Equal_Equal_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(30, 30, _fixedElevation);

            var result = left == right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Equal_Not_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);
            Waypoint right = new Waypoint(30, 30, _fixedElevation);

            var result = left == right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Equal_NotEqual_ByElevation_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(30, 30, _fixedElevation + 1);

            var result = left == right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Equal_Null_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);

            var result = left == null;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GreaterThan_Equal_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left > right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GreaterThan_Less_Assert()
        {
            Waypoint left = new Waypoint(10, 10, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left > right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GreaterThan_More_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left > right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Equal_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left >= right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Less_Assert()
        {
            Waypoint left = new Waypoint(10, 10, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left >= right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_More_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left >= right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void LessThan_Equal_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left < right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void LessThan_Less_Assert()
        {
            Waypoint left = new Waypoint(10, 10, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left < right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void LessThan_More_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left < right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void LessThanEqualTo_Equal_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left <= right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void LessThanEqualTo_Less_Assert()
        {
            Waypoint left = new Waypoint(10, 10, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left <= right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void LessThanEqualTo_More_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(20, 20, _fixedElevation);

            var result = left <= right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void NotEqual_Equal_Assert()
        {
            Waypoint left = new Waypoint(30, 30, _fixedElevation);
            Waypoint right = new Waypoint(30, 30, _fixedElevation);

            var result = left != right;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void NotEqual_Not_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);
            Waypoint right = new Waypoint(30, 30, _fixedElevation);

            var result = left != right;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void NotEqual_Null_Assert()
        {
            Waypoint left = new Waypoint(20, 20, _fixedElevation);

            var result = left != null;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ToDegreeMinuteSecond_Assert()
        {
            Waypoint subject = new Waypoint(41.79620158, 145.5487268, _fixedElevation);

            string result = subject.ToDegreeMinuteSecond();

            Assert.AreEqual("41° 47' 46'' N, 145° 32' 55'' E", result);
        }

        [TestMethod]
        public void ToString_Assert()
        {
            Waypoint subject = new Waypoint(41.79620158, 145.5487268, _fixedElevation);

            string result = subject.ToString();

            Assert.AreEqual("41.79620158, 145.5487268, 0", result);
        }
    }
}