using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using alpoLib.Core.Foundation;
using UnityEngine;

namespace alpoLib.Core.Serialization
{
	public class BufferStream
    {
        public byte[] Bytes;
        public int Offset;
        public int Length;
        public int Pointer;

        public BufferStream()
        {
            Pointer = 0;
        }

        public BufferStream(byte[] bytes, int offset, int length)
        {
            Bytes = bytes;
            Offset = offset;
            Length = length;
            Pointer = offset;
        }

        public void EnsureCapacity(int addLength)
        {
            var capacity = Bytes.Length;
            var newPointer = Pointer + addLength;
            if (newPointer <= capacity)
                return;
            
            while (newPointer > capacity)
            {
                if (capacity == 0)
                    capacity = 1;
                capacity *= 2;
            }

            var newBytes = new byte[capacity];
            Buffer.BlockCopy(Bytes, 0, newBytes, 0, Bytes.Length);
            Bytes = newBytes;
        }

        public byte ReadU8()
        {
            return Bytes[Pointer++];
        }

        public ushort ReadU16()
        {
            var buffer = Bytes;
            var offset = Pointer;
            Pointer += 2;
            
            return (ushort)(uint)((buffer[offset + 1] << 8) | buffer[offset]);
        }
        
        public uint ReadU32()
        {
            var buffer = Bytes;
            var offset = Pointer;
            Pointer += 4;

            return (uint)((buffer[offset + 3] << 24) | (buffer[offset + 2] << 16) | (buffer[offset + 1] << 8) | buffer[offset]);
        }
        
        public ulong ReadU64()
        {
            var buffer = Bytes;
            var offset = Pointer;
            Pointer += 8;

            var imm = (ulong)FetchU32(buffer, offset + 4) << 32;
            return imm | FetchU32(buffer, offset);
        }
        
        public sbyte ReadS8()
        {
            return (sbyte)Bytes[Pointer++];
        }
        
        public short ReadS16()
        {
            var buffer = Bytes;
            var offset = Pointer;
            Pointer += 2;

            return (short)(ushort)(uint)((buffer[offset + 1] << 8) | buffer[offset]);
        }
        
        public int ReadS32()
        {
            var buffer = Bytes;
            var offset = Pointer;
            Pointer += 4;

            return (int)(uint)((buffer[offset + 3] << 24) | (buffer[offset + 2] << 16) | (buffer[offset + 1] << 8) | buffer[offset]);
        }
        
        public long ReadS64()
        {
            var buffer = Bytes;
            var offset = Pointer;
            Pointer += 8;

            var imm = (ulong)FetchU32(buffer, offset + 4) << 32;
            return (long)(imm | FetchU32(buffer, offset));
        }
        
        public float ReadF32()
        {
            var offset = Pointer;
            Pointer += 4;

            return BitConverter.ToSingle(Bytes, offset);
        }

        public double ReadF64()
        {
            var offset = Pointer;
            Pointer += 8;

            return BitConverter.ToDouble(Bytes, offset);
        }
        
        private static uint FetchU32(IReadOnlyList<byte> buffer, int offset)
        {
            return (uint)((buffer[offset + 3] << 24) | (buffer[offset + 2] << 16) | (buffer[offset + 1] << 8) | buffer[offset]);
        }
        
        public void WriteS8(sbyte value)
        {
            EnsureCapacity(1);
            Bytes[Pointer] = (byte)value;
            Pointer += 1;
            Length += 1;
        }

        public void WriteS16(short value)
        {
            EnsureCapacity(2);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 2);
            Pointer += 2;
            Length += 2;
        }

        public void WriteS32(int value)
        {
            EnsureCapacity(4);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 4);
            Pointer += 4;
            Length += 4;
        }

        public void WriteS64(long value)
        {
            EnsureCapacity(8);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 8);
            Pointer += 8;
            Length += 8;
        }
        
        public void WriteU8(byte value)
        {
            EnsureCapacity(1);
            Bytes[Pointer] = value;
            Pointer += 1;
            Length += 1;
        }

        public void WriteU16(ushort value)
        {
            EnsureCapacity(2);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 2);
            Pointer += 2;
            Length += 2;
        }

        public void WriteU32(uint value)
        {
            EnsureCapacity(4);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 4);
            Pointer += 4;
            Length += 4;
        }

        public void WriteU64(ulong value)
        {
            EnsureCapacity(8);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 8);
            Pointer += 8;
            Length += 8;
        }
        
        public void WriteF32(float value)
        {
            EnsureCapacity(4);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 4);
            Pointer += 4;
            Length += 4;
        }

        public void WriteF64(double value)
        {
            EnsureCapacity(8);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, Pointer, 8);
            Pointer += 8;
            Length += 8;
        }
        
        public void Write(byte[] buf, int offset, int length)
        {
            EnsureCapacity(length);
            Buffer.BlockCopy(buf, offset, Bytes, Pointer, length);
            Pointer += length;
            Length += length;
        }
        
        public bool ReadBool()
        {
            return ReadU8() != 0;
        }
        
        public void WriteBool(bool value)
        {
            WriteU8(value ? (byte)1 : (byte)0);
        }
        
        public string ReadStr()
        {
            var len = ReadU16();
            var str = Encoding.UTF8.GetString(Bytes, Pointer, len);
            Pointer += len;
            return str;
        }
        
        public void WriteStr(string value)
        {
            value ??= "";
            var buf = Encoding.UTF8.GetBytes(value);
            WriteU16((ushort)buf.Length);
            EnsureCapacity(buf.Length);
            Buffer.BlockCopy(buf, 0, Bytes, Pointer, buf.Length);
            Pointer += buf.Length;
            Length += buf.Length;
        }

        public CustomBoolean ReadCustomBoolean()
        {
            return ReadBool();
        }

        public void WriteCustomBoolean(CustomBoolean value)
        {
            WriteBool(value);
        }

        public CustomDateTime ReadCustomDateTime()
        {
            var useDateTime = ReadBool();
            var ticks = ReadS64();
            // TODO: Local or UTC or PST ??????
            return new CustomDateTime(useDateTime, new DateTime(ticks, DateTimeKind.Local));
        }
        
        public void WriteCustomDateTime(CustomDateTime value)
        {
            WriteBool(value.UseDateTime);
            WriteS64(value.DateTime.Ticks);
        }

        public CustomColor ReadCustomColor()
        {
            var str = ReadStr();
            var converter = TypeDescriptor.GetConverter(typeof(CustomColor)) as CustomColorTypeConverter;
            if (converter != null)
                return converter.DoConvertFrom(str);
            return Color.white;
        }

        public void WriteCustomColor(CustomColor c)
        {
            WriteStr(c.ToString());
        }
    }
}
