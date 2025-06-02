using alpoLib.Core.Prng;

namespace alpoLib.Core.Security
{
    public class PrngEncryptionProvider<T> where T : IPrng
    {
        private T prng;
        private ulong currentWord;
        private int offset = 0;

        public PrngEncryptionProvider(T prng, ulong key1, ulong key2)
        {
            prng.Initialize(key1, key2);
            for (var i = 0; i < 16; i++)
                prng.Next();
            this.prng = prng;
        }

        public PrngEncryptionProvider(T prng, ulong key)
            : this(prng, key, new SplitMix64(key).Next())
        {
        }

        public PrngEncryptionProvider(T prng, int key)
        {
            //var kk1 = 16306769427172274070L;
            //var kk2 = 6372668087440736104L;
            prng.Initialize(17542168795432105770L ^ ((ulong)key << 32), 5124879505066705789L ^ ((ulong)key << 16));
            for (var i = 0; i < 16; i++)
                prng.Next();
            this.prng = prng;
        }
        
        private void Continue()
        {
            currentWord = prng.Next();
            offset = 0;
        }
        
        public byte NextByte()
        {
            if (offset == 0)
                Continue();

            // LSB ---> MSB
            var bitOff = offset << 3;
            offset = (offset + 1) & 7;  // % 8
            return (byte)(currentWord >> bitOff) /* & 0xff*/;
        }
        
        public void ProcessBytes(byte[] bytes)
        {
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] ^= NextByte();
        }
    }
}
