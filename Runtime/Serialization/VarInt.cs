namespace alpoLib.Core.Serialization
{
    public static class VarInt
    {
        public static int ReadVarInt(BufferStream stream, out int value)
        {
            var b = stream.Bytes[stream.Pointer++];
            if (b < 0xdc)
            {
                value = b;
                return 1;
            }
            else if (b >= 0xe0)
            {
                value = (sbyte)b;
                return 1;
            }
            else
            {
                if (b == 0xdf)
                {
                    value = stream.Bytes[stream.Pointer++];
                    return 2;
                }
                else if (b == 0xde)
                {
                    value = stream.Bytes[stream.Pointer++];
                    value |= ((int)stream.Bytes[stream.Pointer++]) << 8;
                    return 3;
                }
                else if (b == 0xdd)
                {
                    value = stream.Bytes[stream.Pointer++];
                    value |= ((int)stream.Bytes[stream.Pointer++]) << 8;
                    value |= ((int)stream.Bytes[stream.Pointer++]) << 16;
                    return 4;
                }
                else if (b == 0xdc)
                {
                    value = stream.Bytes[stream.Pointer++];
                    value |= ((int)stream.Bytes[stream.Pointer++]) << 8;
                    value |= ((int)stream.Bytes[stream.Pointer++]) << 16;
                    value |= ((int)stream.Bytes[stream.Pointer++]) << 24;
                    return 5;
                }
                else
                {
                    value = 0;
                    return 0;
                }
            }
        }

        public static int WriteVarInt(BufferStream stream, int value)
        {
            var uValue = (uint)value;
            if (uValue < 0xdc)
            {
                stream.Bytes[stream.Pointer++] = (byte)uValue;
                stream.Length++;
                return 1;
            }
            else if (uValue >= 0xffffffe0)
            {
                stream.Bytes[stream.Pointer++] = (byte)uValue;
                stream.Length++;
                return 1;
            }
            else
            {
                if (uValue <= 0xff)
                {
                    stream.Bytes[stream.Pointer++] = (byte)0xdf;
                    stream.Bytes[stream.Pointer++] = (byte)uValue;
                    stream.Length += 2;
                    return 2;
                }
                else if (uValue <= 0xffff)
                {
                    stream.Bytes[stream.Pointer++] = (byte)0xde;
                    stream.Bytes[stream.Pointer++] = (byte)uValue;
                    stream.Bytes[stream.Pointer++] = (byte)(uValue >> 8);
                    stream.Length += 3;
                    return 3;
                }
                else if (uValue <= 0xffffff)
                {
                    stream.Bytes[stream.Pointer++] = (byte)0xdd;
                    stream.Bytes[stream.Pointer++] = (byte)uValue;
                    stream.Bytes[stream.Pointer++] = (byte)(uValue >> 8);
                    stream.Bytes[stream.Pointer++] = (byte)(uValue >> 16);
                    stream.Length += 4;
                    return 4;
                }
                else
                {
                    stream.Bytes[stream.Pointer++] = (byte)0xdc;
                    stream.Bytes[stream.Pointer++] = (byte)uValue;
                    stream.Bytes[stream.Pointer++] = (byte)(uValue >> 8);
                    stream.Bytes[stream.Pointer++] = (byte)(uValue >> 16);
                    stream.Bytes[stream.Pointer++] = (byte)(uValue >> 24);
                    stream.Length += 5;
                    return 5;
                }
            }
        }
    }
}
