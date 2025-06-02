namespace alpoLib.Core.Prng
{
    public interface IPrngBase
    {
        ulong Next();

        /** Returns a pseudorandom uniformly distributed {@code ulong} value
         * between 0 (inclusive) and the specified value (exclusive), drawn from
         * this random number generator's sequence. The algorithm used to generate
         * the value guarantees that the result is uniform, provided that the
         * sequence of 64-bit values produced by this generator is.
         *
         * @param n the bound on the random number to be returned.
         * @return the next pseudorandom {@code ulong} value between {@code 0} (inclusive) and {@code n} (exclusive).
         */
        ulong Next(ulong n);
        void Jump();
    }
}