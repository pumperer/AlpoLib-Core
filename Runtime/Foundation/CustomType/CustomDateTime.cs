using System;
using System.ComponentModel;

namespace alpoLib.Core.Foundation
{
    [Serializable]
    [TypeConverter(typeof(CustomDateTimeConverter))]
    public struct CustomDateTime
    {
        public bool UseDateTime { get; private set; }
        public DateTime DateTime { get; private set; }
        
        public CustomDateTime(bool useDateTime, DateTime dateTime)
        {
            UseDateTime = useDateTime;
            DateTime = dateTime;
        }

        public static implicit operator CustomDateTime(long value)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(value);
            return new CustomDateTime(true, dt);
        }

		public static implicit operator CustomDateTime(DateTime v)
		{
            return new CustomDateTime(true, v);
		}

        public static implicit operator DateTime(CustomDateTime v)
        {
            return v.DateTime;
        }

        public static implicit operator long(CustomDateTime v)
        {
            return ((DateTimeOffset)v.DateTime).ToUnixTimeSeconds();
        }
	}

    internal class CustomDateTimeConverter : TypeConverterBase<CustomDateTime, long>
    {
        public override CustomDateTime DoConvertFrom(long value)
        {
            // if (!long.TryParse(value, out var timeStamp))
            //     throw new ArgumentException($"CustomDateTimeConverter value parsing error! : {value}");

            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(value).ToLocalTime();
            return new CustomDateTime(true, dt);
        }

        public override string DoConvertTo(CustomDateTime value)
        {
            var dto = ((DateTimeOffset)value.DateTime);
            return dto.ToUnixTimeSeconds().ToString();
        }
    }
}
