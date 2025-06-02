using System;
using System.ComponentModel;

namespace alpoLib.Core.Foundation
{
    [Serializable]
    [TypeConverter(typeof(CustomBooleanTypeConverter))]
    public struct CustomBoolean
    {
        public static readonly CustomBoolean False = new(false);
        public static readonly CustomBoolean True = new(true);
        
        private bool value;

        public CustomBoolean(bool value = false)
        {
            this.value = value;
        }

        public static implicit operator CustomBoolean(bool val)
        {
            return val ? True : False;
        }
        
        public static implicit operator bool(CustomBoolean val)
        {
            return val.value;
        }

        public static bool operator true(CustomBoolean val)
        {
            return val.value;
        }

        public static bool operator false(CustomBoolean val)
        {
            return !val.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    internal class CustomBooleanTypeConverter : TypeConverterBase<CustomBoolean>
    {
        public override CustomBoolean DoConvertFrom(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            switch (value.Trim().ToUpper())
            {
                case "TRUE":
                case "YES":
                case "Y":
                case "1":
                case "-1":
                    return true;

                // case "FALSE":
                // case "NO":
                // case "N":
                // case "0":
                //     return false;

                default:
                    return false;
            }
        }
    }
}
