using System;

namespace alpoLib.Core.Prng
{
    public abstract class PrngBase : IPrngBase
    {
        public abstract ulong Next();
        public abstract void Jump();

        public abstract ulong Next(ulong n);

        protected static int NumberOfLeadingZeros(ulong i)
        {
            if (i == 0)
                return 64;
            uint n = 1;
            uint x = (uint)(i >> 32);
            if (x == 0) { n += 32; x = (uint)i; }
            if (x >> 16 == 0) { n += 16; x <<= 16; }
            if (x >> 24 == 0) { n += 8; x <<= 8; }
            if (x >> 28 == 0) { n += 4; x <<= 4; }
            if (x >> 30 == 0) { n += 2; x <<= 2; }
            n -= x >> 31;
            return (int)n;
        }

        /**
         * @return the next pseudorandom, uniformly distributed {@code double}
         * value between {@code 0.0} and {@code 1.0} from this
         * random number generator's sequence, using 52 significant bits only.
         */
        // 0.0 ~ 1.0. k / 2^52 분리도.
        public double NextDoubleFast()
        {
            long r = (long)(0x3FFUL << 52 | Next() >> 12);  // MSB가 3FF이므로 unsigned 경계에 닿지 않는다.
            return BitConverter.Int64BitsToDouble(r) - 1.0;
        }

        // 0.0 ~ 1.0. k / 2^53 분리도.
        public double NextDouble()
        {
            return (Next() >> 11) / (double)(1L << 53);
        }

        // 0.0f ~ 1.0f. k / 2^24 분리도.
        public float NextFloat()
        {
            return (Next() >> 40) / (float)(1L << 24);
        }
    }
}