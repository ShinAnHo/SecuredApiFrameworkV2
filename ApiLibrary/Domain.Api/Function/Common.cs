using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Api
{
    public static class Common
    {
        public static object GetPropValue(this object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }
        public static T GetPropValue<T>(this object obj, string name)
        {
            object retval = GetPropValue(obj, name);
            if (retval == null) { return default; }

            return (T)retval;
        }
        public static DescriptionAttribute GetEnumDescriptionAttribute<T>(this T value) where T : struct
        {
            // The type of the enum, it will be reused.
            Type type = typeof(T);

            // If T is not an enum, get out.
            if (!type.IsEnum)
                throw new InvalidOperationException(
                    "The type parameter T must be an enum type.");

            // If the value isn't defined throw an exception.
            if (!Enum.IsDefined(type, value))
                throw new InvalidEnumArgumentException(
                    "value", Convert.ToInt32(value), type);

            // Get the static field for the value.
            FieldInfo fi = type.GetField(value.ToString(),
                BindingFlags.Static | BindingFlags.Public);

            // Get the description attribute, if there is one.
            return fi.GetCustomAttributes(typeof(DescriptionAttribute), true).
                Cast<DescriptionAttribute>().SingleOrDefault();
        }
        public static string GetEnumDescription(ApiMessageType type, object args = null)
        {
            return string.Format(type.GetEnumDescriptionAttribute().Description, args);
        }
        public static string GetEnumDescription(ApiValidationType type, object args = null)
        {
            return string.Format(type.GetEnumDescriptionAttribute().Description, args);
        }
        public static string JSONSerialize(object value, bool ignoreNullAndEmpty = false)
        {
            if (!ignoreNullAndEmpty)
                return JsonConvert.SerializeObject(value);
            else
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                return JsonConvert.SerializeObject(value, settings);
            }
        }
        public static T JSONDeserialize<T>(string value) where T : class
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        public static bool TryCast<T>(this object obj, out T result)
        {
            try
            {
                if (obj is T)
                {
                    result = (T)obj;
                    return true;
                }

                result = default(T);
                return false;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }
        public static T NullConvert<T>(object obj)
        {
            if (obj != null)
                return (T)obj;
            else if (obj == null && typeof(T) == typeof(string))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFrom("");
            }
            else
                return default(T);
        }
    }
}
