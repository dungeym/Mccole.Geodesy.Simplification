using System;
using System.Security.Cryptography;

namespace Mccole
{
    /// <summary>
    /// Defines methods to generate random data.
    /// </summary>
    public interface IArbitrary
    {
        /// <summary>
        /// Fills the elements of the array with random numbers.
        /// </summary>
        /// <param name="buffer"></param>
        void Bytes(byte[] buffer);

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        double Double();

        /// <summary>
        /// Randomly returns either true or false.
        /// </summary>
        /// <returns></returns>
        bool Boolean();

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        short Int16();

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The exclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        short Int16(short maximum);

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The exclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        short Int16(short minimum, short maximum);

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        int Int32();

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The exclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        int Int32(int maximum);

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The exclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        int Int32(int minimum, int maximum);

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        long Int64();

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The exclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        long Int64(long maximum);

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The exclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        long Int64(long minimum, long maximum);
    }

    /// <summary>
    /// Generate random data.
    /// </summary>
    public class Arbitrary : IArbitrary, IDisposable
    {
        // A better implementation of 'randomness' that uses the [RNGCryptoServiceProvider].
        private readonly byte[] _bufferInt64 = new byte[8];
        private readonly RNGCryptoServiceProvider _provider = new RNGCryptoServiceProvider();
        private static Lazy<IArbitrary> _lazy = new Lazy<IArbitrary>(() => new Arbitrary());

        /// <summary>
        /// Generate random data.
        /// </summary>
        public Arbitrary()
        {
        }

        public static IArbitrary Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        #region Members IArbitrary

        /// <summary>
        /// Fills the elements of the array with random numbers.
        /// </summary>
        /// <param name="buffer"></param>
        void IArbitrary.Bytes(byte[] buffer)
        {
            FillBuffer(buffer);
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        double IArbitrary.Double()
        {
            return GenerateDouble();
        }

        /// <summary>
        /// Randomly returns either true or false.
        /// </summary>
        /// <returns></returns>
        bool IArbitrary.Boolean()
        {
            return GenerateBoolean();
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        short IArbitrary.Int16()
        {
            return (short)Generate(0, short.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        short IArbitrary.Int16(short maximum)
        {
            return (short)Generate(0, maximum);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        short IArbitrary.Int16(short minimum, short maximum)
        {
            return (short)Generate(minimum, maximum);
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        int IArbitrary.Int32()
        {
            return (int)Generate(0, int.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        int IArbitrary.Int32(int maximum)
        {
            return (int)Generate(0, maximum);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        int IArbitrary.Int32(int minimum, int maximum)
        {
            return (int)Generate(minimum, maximum);
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        long IArbitrary.Int64()
        {
            return Generate(0, long.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        long IArbitrary.Int64(long maximum)
        {
            return Generate(0, maximum);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        long IArbitrary.Int64(long minimum, long maximum)
        {
            return Generate(minimum, maximum);
        }
        #endregion Members IArbitrary

        /// <summary>
        /// Fills the elements of the array with random numbers.
        /// </summary>
        /// <param name="buffer"></param>
        public static void Bytes(byte[] buffer)
        {
            Arbitrary.Instance.Bytes(buffer);
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        public static double Double()
        {
            return Arbitrary.Instance.Double();
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        public static bool Boolean()
        {
            return Arbitrary.Instance.Boolean();
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        public static short Int16()
        {
            return Arbitrary.Instance.Int16();
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        public static short Int16(short maximum)
        {
            return Arbitrary.Instance.Int16(maximum);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        public static short Int16(short minimum, short maximum)
        {
            return Arbitrary.Instance.Int16(minimum, maximum);
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        public static int Int32()
        {
            return Arbitrary.Instance.Int32();
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        public static int Int32(int maximum)
        {
            return Arbitrary.Instance.Int32(maximum);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        public static int Int32(int minimum, int maximum)
        {
            return Arbitrary.Instance.Int32(minimum, maximum);
        }

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns></returns>
        public static long Int64()
        {
            return Arbitrary.Instance.Int64();
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than zero.</param>
        /// <returns></returns>
        public static long Int64(long maximum)
        {
            return Arbitrary.Instance.Int64(maximum);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum"> The inclusive upper bound of the random number returned, must be greater than the minimum value.</param>
        /// <returns></returns>
        public static long Int64(long minimum, long maximum)
        {
            return Arbitrary.Instance.Int64(minimum, maximum);
        }

        #region Members

        /// <summary>
        /// Returns a random number that is within a specified range.
        /// </summary>
        /// <param name="minimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="maximum">The inclusive upper bound of the random number returned, [maximum] must be greater than or equal to [minimum].</param>
        /// <returns></returns>
        private long Generate(long minimum, long maximum)
        {
            if (minimum > maximum)
            {
                throw new ArgumentOutOfRangeException("minimum", "The [maximum] must be greater than the [minimum].");
            }

            double difference = maximum - minimum;
            double offset = difference * GenerateDouble();
            return minimum + Convert.ToInt64(offset);
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <returns></returns>
        private double GenerateDouble()
        {
            _provider.GetBytes(_bufferInt64);
            ulong value = BitConverter.ToUInt64(_bufferInt64, 0);
            return value / (ulong.MaxValue + 1.0);
        }

        /// <summary>
        /// Randomly returns either true or false.
        /// </summary>
        /// <returns></returns>
        private bool GenerateBoolean()
        {
            _provider.GetBytes(_bufferInt64);
            long value = BitConverter.ToInt64(_bufferInt64, 0);
            return value >= 0;
        }

        /// <summary>
        /// Fills the elements of the array with random numbers.
        /// </summary>
        /// <param name="buffer">The array to fill.</param>
        private void FillBuffer(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "The argument cannot be null.");
            }

            _provider.GetBytes(buffer);
        }
        #endregion Members

        #region IDisposable Members
        private bool _disposed = false;

        /// <summary>
        /// Finalize...
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        ~Arbitrary()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Managed
                    if (_provider != null)
                    {
                        _provider.Dispose();
                    }
                }
                else
                {
                    // Unmanaged
                }

                _disposed = true;
            }
        }
        #endregion IDisposable Members
    }
}
