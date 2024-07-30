using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Common.Razor.Select
{
    public static class DncConverter
    {
        public static TValue ChangeType<TValue>(string value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            Type targetType = typeof(TValue);

            try
            {
                if (targetType == typeof(string))
                {
                    return (TValue)(object)value;
                }

                if (targetType == typeof(Guid))
                {
                    return (TValue)(object)new Guid(value);
                }

                if (Nullable.GetUnderlyingType(targetType) is Type underlyingType)
                {
                    targetType = underlyingType;
                }

                if (targetType == typeof(DateTime))
                {

                    return (TValue)(object)DateTime.Parse(value, cultureInfo);
                }

                if (targetType == typeof(bool))
                {
                    return (TValue)(object)bool.Parse(value);
                }

                if (targetType.IsEnum)
                {
                    return (TValue)Enum.Parse(targetType, value);
                }

                if (targetType.IsValueType || targetType.IsPrimitive)
                {
                    return (TValue)Convert.ChangeType(value, targetType, cultureInfo);
                }

                throw new InvalidCastException($"Cannot convert '{value}' to {targetType.Name}.");
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Cannot convert '{value}' to {targetType.Name}.", ex);
            }
        }

        public static TValue ChangeType<TValue>(string value)
        {
            return ChangeType<TValue>(value, CultureInfo.CurrentCulture);
        }
    }

}
