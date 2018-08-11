using System;
using System.Text;

namespace Mccole.TestTools
{
    /// <summary>
    /// Utility for generating random data for testing.
    /// </summary>
    public class TestData
    {
        /// <summary>
        /// Returns either True or False.
        /// </summary>
        /// <returns></returns>
        public static bool Boolean()
        {
            return Arbitrary.Boolean();
        }

        /// <summary>
        /// Returns a random DateTime by shifting the current UtcNow by a random number of days either forward or backwards.
        /// </summary>
        /// <returns></returns>
        public static DateTime Date()
        {
            int minimum = (int)DateTime.UtcNow.Subtract(DateTime.MinValue).TotalDays;
            int maximum = (int)DateTime.MaxValue.Subtract(DateTime.UtcNow).TotalDays;
            int offset = Arbitrary.Int32(minimum, maximum);
            return DateTime.UtcNow.AddDays(offset);
        }

        /// <summary>
        /// Returns a random future DateTime by shifting the current UtcNow by a random number of days.
        /// </summary>
        /// <returns></returns>
        public static DateTime FutureDate()
        {
            int maximum = (int)DateTime.MaxValue.Subtract(DateTime.UtcNow).TotalDays;
            int offset = Arbitrary.Int32(0, maximum);
            return DateTime.UtcNow.AddDays(offset);
        }

        /// <summary>
        /// Returns a random past (historical) DateTime by shifting the current UtcNow by a random number of days.
        /// </summary>
        /// <returns></returns>
        public static DateTime PastDate()
        {
            int minimum = (int)DateTime.UtcNow.Subtract(DateTime.MinValue).TotalDays * -1;
            int offset = Arbitrary.Int32(minimum, 0);
            return DateTime.UtcNow.AddDays(offset);
        }

        /// <summary>
        /// Returns a random past (historical) DateTime by shifting the current UtcNow by a random number of days.
        /// <para>The result will be greater than 1-Jan-1970.</para>
        /// </summary>
        /// <returns></returns>
        public static DateTime PastDateWin32()
        {
            DateTime min = new DateTime(1970, 1, 1);
            int minimum = (int)DateTime.UtcNow.Subtract(min).TotalDays * -1;
            int offset = Arbitrary.Int32(minimum, 0);
            return DateTime.UtcNow.AddDays(offset);
        }

        /// <summary>
        /// Returns a non-negative random value between 0 and 255.
        /// </summary>
        /// <returns></returns>
        public static int Tiny()
        {
            return (short)Arbitrary.Int32(0, 256);
        }

        /// <summary>
        /// Returns a non-negative random value between 0 and the specified maximum.
        /// </summary>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated, must be 256 or less.</param>
        /// <returns></returns>
        public static int Tiny(int maximum)
        {
            if (maximum > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(maximum), "The maximum value of Tiny is 255.");
            }

            return (short)Arbitrary.Int32(0, maximum);
        }

        /// <summary>
        /// Returns a non-negative random value between the specified values.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated, must be 256 or less.</param>
        /// <returns></returns>
        public static int Tiny(int minimum, int maximum)
        {
            if (minimum < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimum), "The minimum value of Tiny is 0.");
            }
            if (maximum > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(maximum), "The maximum value of Tiny is 255.");
            }

            return (short)Arbitrary.Int32(minimum, maximum);
        }

        /// <summary>
        /// Returns a non-negative random value between 0 and 32767.
        /// </summary>
        /// <returns></returns>
        public static short Short()
        {
            return (short)Arbitrary.Int32(0, short.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random value between 0 and the specified maximum.
        /// </summary>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated.</param>
        /// <returns></returns>
        public static short Short(short maximum)
        {
            return (short)Arbitrary.Int32(0, maximum);
        }

        /// <summary>
        /// Returns a non-negative random short that is within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated.</param>
        /// <returns></returns>
        public static short Short(short minimum, short maximum)
        {
            return (short)Arbitrary.Int32(minimum, maximum);
        }

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns></returns>
        public static int Int()
        {
            return Arbitrary.Int32();
        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated.</param>
        /// <returns></returns>
        public static int Int(int maximum)
        {
            return Arbitrary.Int32(maximum);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated.</param>
        public static int Int(int minimum, int maximum)
        {
            return Arbitrary.Int32(minimum, maximum);
        }

        /// <summary>
        /// Returns a random integer that is greater than zero and less than or equal to Int32.MaxValue.
        /// </summary>
        /// <returns></returns>
        public static int Positive()
        {
            return Arbitrary.Int32(1, int.MaxValue);
        }

        /// <summary>
        /// Returns a random integer that is greater than zero and less than or equal to Int32.MaxValue.
        /// </summary>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated.</param>
        /// <returns></returns>
        public static int Positive(int maximum)
        {
            return Arbitrary.Int32(1, maximum);
        }

        /// <summary>
        /// Returns a random integer that is greater than or equal to Int32.MinValue and less than zero.
        /// </summary>
        /// <returns></returns>
        public static int Negative()
        {
            return Arbitrary.Int32(int.MinValue, -1);
        }

        /// <summary>
        /// Returns a random integer that is greater than or equal to the specified minimum and less than zero.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <returns></returns>
        public static int Negative(int minimum)
        {
            return Arbitrary.Int32(minimum, 0);
        }

        /// <summary>
        /// Returns a non-negative random long.
        /// </summary>
        /// <returns></returns>
        public static long Long()
        {
            return Arbitrary.Int64(0, long.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random long that is less than the specified maximum.
        /// </summary>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated, maximum must be greater than or equal to 0.</param>
        /// <returns></returns>
        public static long Long(long maximum)
        {
            return Arbitrary.Int64(0, maximum);
        }

        /// <summary>
        /// Returns a random long that is within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum">The inclusive upper bound of the random number returned, [maximum] must be greater than or equal to [minimum].</param>
        /// <returns></returns>
        public static long Long(long minimum, long maximum)
        {
            return Arbitrary.Int64(minimum, long.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random double.
        /// </summary>
        /// <returns></returns>
        public static double Double()
        {
            return Double(0, double.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random double that is less than the specified maximum.
        /// </summary>
        /// <param name="maximum">The inclusive upper bound of the random number to be generated, maximum must be greater than or equal to 0.</param>
        /// <returns></returns>
        public static double Double(double maximum)
        {
            return Double(0, maximum);
        }

        /// <summary>
        /// Returns a random double that is within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum">The inclusive upper bound of the random number returned, [maximum] must be greater than or equal to [minimum].</param>
        /// <returns></returns>
        public static double Double(double minimum, double maximum)
        {
            return Arbitrary.Double() * (maximum - minimum) + minimum;
        }

        /// <summary>
        /// Returns a string of a random length (between 20 and 255) containing a random mixture of upper-case and lower-case alphabetic characters.
        /// </summary>
        /// <returns></returns>
        public static string String()
        {
            TestData random = new TestData();
            return random.GetString(20, 255);
        }

        /// <summary>
        /// Returns a string of the defined length containing a random mixture of upper-case and lower-case alphabetic characters.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string String(int length)
        {
            TestData random = new TestData();
            return random.GetString(length, length);
        }

        /// <summary>
        /// Returns a string of the defined length containing a random mixture of upper-case and lower-case alphabetic characters.
        /// <para>The [contains] text is inserted into the resulting string at a random position other than the start or the end.</para>
        /// </summary>
        /// <param name="length"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static string String(int length, string contains)
        {
            TestData random = new TestData();
            return random.GetString(length, length, contains);
        }

        /// <summary>
        /// Returns a string of random length (between minimum and maximum) containing a random mixture of upper-case and lower-case alphabetic characters.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static string String(int minimum, int maximum)
        {
            TestData random = new TestData();
            return random.GetString(minimum, maximum);
        }

        /// <summary>
        /// Returns a string of random length (between minimum and maximum) containing a random mixture of upper-case and lower-case alphabetic characters.
        /// <para>The [contains] text is inserted into the resulting string at a random position other than the start or the end.</para>
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static string String(int minimum, int maximum, string contains)
        {
            TestData random = new TestData();
            return random.GetString(minimum, maximum, contains);
        }

        private string GetString(int minimum, int maximum)
        {
            return GenerateString(minimum, maximum);
        }

        private string GetString(int minimum, int maximum, string contains)
        {
            if (string.IsNullOrWhiteSpace(contains))
            {
                throw new ArgumentNullException(nameof(contains), "The argument cannot be null.or empty.");
            }

            int length = contains.Length;
            string text = GenerateString(minimum, maximum);

            // Determine the location to insert the 'contains' so the length on the result is not more than the maximum.
            int location = Arbitrary.Int32(0, text.Length - length);

            return text.Insert(location, contains);
        }

        private string GenerateString(int minimum, int maximum)
        {
            int upperCaseA = 65;
            int upperCaseZ = 90;
            int lowerCaseA = 97;
            int lowerCaseZ = 122;
            int length = Arbitrary.Int32(minimum, maximum);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int c;
                if (Arbitrary.Boolean())
                {
                    c = Arbitrary.Int32(upperCaseA, upperCaseZ);
                }
                else
                {
                    c = Arbitrary.Int32(lowerCaseA, lowerCaseZ);
                }
                builder.Append((char)c);
            }

            return builder.ToString();
        }
    }
}
