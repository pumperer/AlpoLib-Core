using System;

namespace alpoLib.Core.Foundation
{
    public struct ReferenceCountBool : IComparable, IComparable<ReferenceCountBool>, IComparable<bool>, IEquatable<bool>, IEquatable<ReferenceCountBool>, IConvertible
    {
        private int _count;
        private bool _value => _count > 0;

        public override string ToString()
        {
            return _count.ToString();
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is bool flag))
                throw new ArgumentException("obj is not a boolean");
            if (this == flag)
                return 0;
            return !this ? -1 : 1;
        }

        public int CompareTo(bool value)
        {
            if (this == value)
                return 0;
            return !this ? -1 : 1;
        }

        public override int GetHashCode() => _value.GetHashCode();

        public int CompareTo(ReferenceCountBool other)
        {
            return _value.CompareTo(other._value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is bool flag)
                return Equals(flag);
            if (obj is int count)
                return _count.Equals(count);
            return _value.Equals(obj);
        }
        
        public bool Equals(bool value) => this == value;
        public bool Equals(ReferenceCountBool other) => this == other;
        
        public static implicit operator bool(in ReferenceCountBool value)
        {
            return value._value;
        }
        
        public static implicit operator ReferenceCountBool(in bool value)
        {
            return new ReferenceCountBool { _count = value ? 1 : 0 };
        }
        
        public static implicit operator ReferenceCountBool(in int value)
        {
            return new ReferenceCountBool { _count = value };
        }
        
        public static implicit operator int(in ReferenceCountBool value)
        {
            return value._count;
        }
        
        public static bool operator ==(in ReferenceCountBool a, in ReferenceCountBool b)
        {
            return a._value == b._value;
        }
        
        public static bool operator !=(in ReferenceCountBool a, in ReferenceCountBool b)
        {
            return a._value != b._value;
        }
        
        public static bool operator ==(in ReferenceCountBool a, in bool b)
        {
            return a._value == b;
        }
        
        public static bool operator !=(in ReferenceCountBool a, in bool b)
        {
            return a._value != b;
        }
        
        public static bool operator ==(in bool a, in ReferenceCountBool b)
        {
            return a == b._value;
        }
        
        public static bool operator !=(in bool a, in ReferenceCountBool b)
        {
            return a != b._value;
        }
        
        public static bool operator ==(in ReferenceCountBool a, in int b)
        {
            return a._count == b;
        }
        
        public static bool operator !=(in ReferenceCountBool a, in int b)
        {
            return a._count != b;
        }
        
        public static bool operator ==(in int a, in ReferenceCountBool b)
        {
            return a == b._count;
        }
        
        public static bool operator !=(in int a, in ReferenceCountBool b)
        {
            return a != b._count;
        }
        
        public static ReferenceCountBool operator ++(in ReferenceCountBool a)
        {
            return new ReferenceCountBool { _count = a._count + 1 };
        }
        
        public static ReferenceCountBool operator --(in ReferenceCountBool a)
        {
            return new ReferenceCountBool { _count = a._count - 1 };
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider) => Convert.ToBoolean(_count);

        byte IConvertible.ToByte(IFormatProvider provider) => Convert.ToByte(_count);

        char IConvertible.ToChar(IFormatProvider provider) => Convert.ToChar(_count);

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException("Cannot convert ReferenceCountBool to DateTime.");
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider) => Convert.ToDecimal(_count);

        double IConvertible.ToDouble(IFormatProvider provider) => Convert.ToDouble(_count);

        short IConvertible.ToInt16(IFormatProvider provider) => Convert.ToInt16(_count);

        int IConvertible.ToInt32(IFormatProvider provider) => _count;

        long IConvertible.ToInt64(IFormatProvider provider) => Convert.ToInt64(_count);

        sbyte IConvertible.ToSByte(IFormatProvider provider) => Convert.ToSByte(_count);

        float IConvertible.ToSingle(IFormatProvider provider) => Convert.ToSingle(_count);

        string IConvertible.ToString(IFormatProvider provider) => _count.ToString();

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => Convert.ChangeType(_count, conversionType);

        ushort IConvertible.ToUInt16(IFormatProvider provider) => Convert.ToUInt16(_count);

        uint IConvertible.ToUInt32(IFormatProvider provider) => Convert.ToUInt32(_count);

        ulong IConvertible.ToUInt64(IFormatProvider provider) => Convert.ToUInt64(_count);
    }
}