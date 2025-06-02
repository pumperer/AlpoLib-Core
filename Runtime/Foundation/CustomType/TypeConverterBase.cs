using System;
using System.ComponentModel;
using System.Globalization;

namespace alpoLib.Core.Foundation
{
    public abstract class TypeConverterBase<T, F> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(F) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is F f)
                return DoConvertFrom(f);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is T t)
                return DoConvertTo(t);
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public virtual T DoConvertFrom(F value)
        {
            throw new NotImplementedException();
        }

        public virtual string DoConvertTo(T value)
        {
            throw new NotImplementedException();
        }
    }
    
    public abstract class TypeConverterBase<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
                return DoConvertFrom(s);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == typeof(string) ? DoConvertTo((T)value) : base.ConvertTo(context, culture, value, destinationType);
        }

        public virtual T DoConvertFrom(string value)
        {
            throw new NotImplementedException();
        }

        public virtual string DoConvertTo(T value)
        {
            throw new NotImplementedException();
        }
    }
}
