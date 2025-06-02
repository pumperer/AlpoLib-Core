// http://xoroshiro.di.unimi.it/

namespace alpoLib.Core.Prng
{
    public interface IPrng : IPrngBase
    {
        void Initialize(ulong s0, ulong s1);
    }
    
    // C# implementation of xoroshiro128+ PRNG
    // http://xoroshiro.di.unimi.it/xoroshiro128plus.c

    // Written in 2016 by David Blackman and Sebastiano Vigna (vigna@acm.org)
    // To the extent possible under law, the author has dedicated all copyright
    // and related and neighboring rights to this software to the public domain
    // worldwide. This software is distributed without any warranty.
    //
    // See <http://creativecommons.org/publicdomain/zero/1.0/>.
    public sealed class XRSR : PrngBase, IPrng
    {
        // This is the successor to xorshift128+. It is the fastest full-period
        // generator passing BigCrush without systematic failures, but due to the
        // relatively short period it is acceptable only for applications with a
        // mild amount of parallelism; otherwise, use a xorshift1024* generator.
        //
        // Beside passing BigCrush, this generator passes the PractRand test suite
        // up to (and included) 16TB, with the exception of binary rank tests,
        // which fail due to the lowest bit being an LFSR; all other bits pass all
        // tests. We suggest to use a sign test to extract a random Boolean value.
        //
        // Note that the generator uses a simulated rotate operation, which most C
        // compilers will turn into a single instruction. In Java, you can use
        // Long.rotateLeft(). In languages that do not make low-level rotation
        // instructions accessible xorshift128+ could be faster.
        //
        // The state must be seeded so that it is not everywhere zero. If you have
        // a 64-bit seed, we suggest to seed a splitmix64 generator and use its
        // output to fill s.

        ulong[] s = new ulong[2];

        public XRSR()
        {
        }

        public XRSR(ulong seed)
        {
            // Sets the seed of this generator.
            // The argument will be used to seed a SplitMix64RandomGenerator, whose output
            // will in turn be used to seed this generator. This approach makes warmup unnecessary,
            // and makes the probability of starting from a state
            // with a large fraction of bits set to zero astronomically small.
            SplitMix64 r = new SplitMix64(seed);
            s[0] = r.Next();
            s[1] = r.Next();
        }

        public void Initialize(ulong s0, ulong s1)
        {
            s[0] = s0;
            s[1] = s1;

            Jump();
        }

        private ulong rotl(ulong x, int k)
        {
            return (x << k) | (x >> (64 - k));
        }
        
        public override ulong Next()
        {
            var s0 = s[0];    // const
            var s1 = s[1];
            var result = s0 + s1; // const

            s1 ^= s0;
            s[0] = rotl(s0, 24) ^ s1 ^ (s1 << 16); // a, b
            s[1] = rotl(s1, 37); // c
            return result;
        }

        public override ulong Next(ulong n)
        {
            ulong t = Next();
            ulong nMinus1 = n - 1;  // const
            // Shortcut for powers of two--high bits
            if ((n & nMinus1) == 0)
            {
                return t >> NumberOfLeadingZeros(nMinus1) & nMinus1;
            }
            // Rejection-based algorithm to get uniform integers in the general case
            for (ulong u = t >> 1; u + nMinus1 - (t = u % n) < 0; u = Next() >> 1)
            {
                // empty
            }

            return t;
        }

        private static ulong[] JUMP = new ulong[2] { 0xbeac0467eba5facb, 0xd86b048b86aa9922 };

        // This is the jump function for the generator. It is equivalent
        // to 2^64 calls to next(); it can be used to generate 2^64
        // non-overlapping subsequences for parallel computations.
        public override void Jump()
        {
            ulong s0 = 0;
            ulong s1 = 0;
            foreach (var t in JUMP)
            {
                for (var b = 0; b < 64; b++)
                {
                    if ((t & (1UL << b)) != 0)
                    {
                        s0 ^= s[0];
                        s1 ^= s[1];
                    }
                    Next();
                }
            }

            s[0] = s0;
            s[1] = s1;
        }
    }

    // C# implementation of xoshiro256** PRNG
    // http://xoroshiro.di.unimi.it/xoshiro256starstar.c

    // Written in 2018 by David Blackman and Sebastiano Vigna (vigna@acm.org)
    // 
    // To the extent possible under law, the author has dedicated all copyright
    // and related and neighboring rights to this software to the public domain
    // worldwide. This software is distributed without any warranty.
    // 
    // See <http://creativecommons.org/publicdomain/zero/1.0/>.
    public sealed class XSR256SS : PrngBase
    {
        // This is xoshiro256** 1.0, our all-purpose, rock-solid generator. It has
        // excellent (sub-ns) speed, a state (256 bits) that is large enough for
        // any parallel application, and it passes all tests we are aware of.
        //
        // For generating just floating-point numbers, xoshiro256+ is even faster.
        //
        // The state must be seeded so that it is not everywhere zero. If you have
        // a 64-bit seed, we suggest to seed a splitmix64 generator and use its
        // output to fill s.

        ulong[] s = new ulong[4];

        public XSR256SS(ulong s0, ulong s1, ulong s2, ulong s3)
        {
            s[0] = s0;
            s[1] = s1;
            s[2] = s2;
            s[3] = s3;
        }

        public XSR256SS(ulong seed)
        {
            // Sets the seed of this generator.
            // The argument will be used to seed a SplitMix64RandomGenerator, whose output
            // will in turn be used to seed this generator. This approach makes warmup unnecessary,
            // and makes the probability of starting from a state
            // with a large fraction of bits set to zero astronomically small.
            SplitMix64 r = new SplitMix64(seed);
            s[0] = r.Next();
            s[1] = r.Next();
            s[2] = r.Next();
            s[3] = r.Next();
        }

        public override ulong Next()
        {
            ulong v = s[1] * 5;
            ulong result_starstar = ((v << 7) | (v >> 57)) * 9;  // const

            ulong t = s[1] << 17;   // const

            s[2] ^= s[0];
            s[3] ^= s[1];
            s[1] ^= s[2];
            s[0] ^= s[3];

            s[2] ^= t;

            v = s[3];
            s[3] = ((v << 45) | (v >> 19));

            return result_starstar;
        }

        public override ulong Next(ulong n)
        {
            ulong t = Next();
            ulong nMinus1 = n - 1;  // const
            // Rejection-based algorithm to get uniform integers in the general case
            for (ulong u = t >> 1; u + nMinus1 - (t = u % n) < 0; u = Next() >> 1)
            {
                // empty
            }

            return t;
        }

        private static ulong[] JUMP = new ulong[4] { 0x180ec6d33cfd0aba, 0xd5a61266f0c9392c, 0xa9582618e03fc9aa, 0x39abdc4529b1661c };

        // This is the jump function for the generator. It is equivalent
        // to 2^128 calls to next(); it can be used to generate 2^128
        // non-overlapping subsequences for parallel computations.
        public override void Jump()
        {
            ulong s0 = 0;
            ulong s1 = 0;
            ulong s2 = 0;
            ulong s3 = 0;
            foreach (var t in JUMP)
            {
                for (var b = 0; b < 64; b++)
                {
                    if ((t & (1UL << b)) != 0)
                    {
                        s0 ^= s[0];
                        s1 ^= s[1];
                        s2 ^= s[2];
                        s3 ^= s[3];
                    }
                    Next();
                }
            }

            s[0] = s0;
            s[1] = s1;
            s[2] = s2;
            s[3] = s3;
        }
    }

    // C# implementation of xoshiro256+ PRNG
    // http://xoroshiro.di.unimi.it/xoshiro256plus.c

    // Written in 2018 by David Blackman and Sebastiano Vigna (vigna@acm.org)
    // 
    // To the extent possible under law, the author has dedicated all copyright
    // and related and neighboring rights to this software to the public domain
    // worldwide. This software is distributed without any warranty.
    // 
    // See <http://creativecommons.org/publicdomain/zero/1.0/>.
    public sealed class XSR256P : PrngBase
    {
        // This is xoshiro256+ 1.0, our best and fastest generator for floating-point
        // numbers. We suggest to use its upper bits for floating-point
        // generation, as it is slightly faster than xoshiro256**. It passes all
        // tests we are aware of except for the lowest three bits, which might
        // fail linearity tests (and just those), so if low linear complexity is
        // not considered an issue (as it is usually the case) it can be used to
        // generate 64-bit outputs, too.
        //
        // We suggest to use a sign test to extract a random Boolean value, and
        // right shifts to extract subsets of bits.
        //
        // The state must be seeded so that it is not everywhere zero. If you have
        // a 64-bit seed, we suggest to seed a splitmix64 generator and use its
        // output to fill s.

        ulong[] s = new ulong[4];

        public XSR256P(ulong s0, ulong s1, ulong s2, ulong s3)
        {
            s[0] = s0;
            s[1] = s1;
            s[2] = s2;
            s[3] = s3;
        }

        public XSR256P(ulong seed)
        {
            // Sets the seed of this generator.
            // The argument will be used to seed a SplitMix64RandomGenerator, whose output
            // will in turn be used to seed this generator. This approach makes warmup unnecessary,
            // and makes the probability of starting from a state
            // with a large fraction of bits set to zero astronomically small.
            SplitMix64 r = new SplitMix64(seed);
            s[0] = r.Next();
            s[1] = r.Next();
            s[2] = r.Next();
            s[3] = r.Next();
        }

        public override ulong Next()
        {
            var result_plus = s[0] + s[3];  // const

            var t = s[1] << 17;  // const

            s[2] ^= s[0];
            s[3] ^= s[1];
            s[1] ^= s[2];
            s[0] ^= s[3];

            s[2] ^= t;

            t = s[3];
            s[3] = (t << 45) | (t >> 19);

            return result_plus;
        }

        public override ulong Next(ulong n)
        {
            var t = Next();
            var nMinus1 = n - 1;  // const
            // Shortcut for powers of two--high bits
            if ((n & nMinus1) == 0)
            {
                return t >> NumberOfLeadingZeros(nMinus1) & nMinus1;
            }
            // Rejection-based algorithm to get uniform integers in the general case
            for (var u = t >> 1; u + nMinus1 - (t = u % n) < 0; u = Next() >> 1)
            {
                // empty
            }

            return t;
        }

        private static ulong[] JUMP = new ulong[4] { 0x180ec6d33cfd0aba, 0xd5a61266f0c9392c, 0xa9582618e03fc9aa, 0x39abdc4529b1661c };

        // This is the jump function for the generator. It is equivalent
        // to 2^128 calls to next(); it can be used to generate 2^128
        // non-overlapping subsequences for parallel computations.
        public override void Jump()
        {
            ulong s0 = 0;
            ulong s1 = 0;
            ulong s2 = 0;
            ulong s3 = 0;
            foreach (var t in JUMP)
            {
                for (var b = 0; b < 64; b++)
                {
                    if ((t & (1UL << b)) != 0)
                    {
                        s0 ^= s[0];
                        s1 ^= s[1];
                        s2 ^= s[2];
                        s3 ^= s[3];
                    }
                    Next();
                }
            }

            s[0] = s0;
            s[1] = s1;
            s[2] = s2;
            s[3] = s3;
        }
    }

    // xorshift128+
    public sealed class XSP : PrngBase, IPrng
    {
        ulong[] s = new ulong[2];

        public XSP()
        {
        }

        public XSP(ulong seed)
        {
            var r = new SplitMix64(seed);
            s[0] = r.Next();
            s[1] = r.Next();
        }

        public void Initialize(ulong s0, ulong s1)
        {
            s[0] = s0;
            s[1] = s1;
        }

        public override ulong Next()
        {
            var x = s[0];
            var y = s[1]; // const
            s[0] = y;
            x ^= x << 23; // a
            s[1] = x ^ y ^ (x >> 17) ^ (y >> 26); // b, c

            return s[1] + y;
        }

        public override ulong Next(ulong n)
        {
            // No special provision for n power of two: all our bits are good.
            for (; ; )
            {
                var bits = Next() >> 1;
                var value = bits % n;
                if (bits - value + (n - 1) >= 0) return value;
            }
        }

        public override void Jump()
        {
            // nop
        }
    }
}