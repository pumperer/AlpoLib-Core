using System;
using System.ComponentModel;
using UnityEngine;

namespace alpoLib.Core.Foundation
{
    [Serializable]
    [TypeConverter(typeof(CustomColorTypeConverter))]
    public struct CustomColor
    {
        private Color value;
        
        public CustomColor(Color c = new())
        {
            value = c;
        }

        public static implicit operator CustomColor(Color c)
        {
            return new CustomColor(c);
        }

        public static implicit operator Color(CustomColor cc)
        {
            return cc.value;
        }

        public override string ToString()
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(value)}";
        }
    }

    internal class CustomColorTypeConverter : TypeConverterBase<CustomColor>
    {
        public override CustomColor DoConvertFrom(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Color.white;

            if (ColorUtility.TryParseHtmlString(value, out var c))
                return c;
            return Color.white;
        }
    }
}