using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Enumeration
    {
        public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (TEnum value in Enum.GetValues(enumerationType))
            {
                var key = (int)Enum.Parse(enumerationType, value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                dictionary.Add(key, attributes.Length > 0 ? attributes[0].Description : value.ToString());
            }

            return dictionary;
        }

        public static object GetByKey<TEnum>(int valor) where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            foreach (TEnum value in Enum.GetValues(enumerationType))
            {
                var key = (int)Enum.Parse(enumerationType, value.ToString());
                if (key == valor)
                { return value; }
            }
            return null;
        }
        public static object GetByDesc<TEnum>(string valor) where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
                throw new ArgumentException("Enumeration type is expected.");

            var dictionary = new Dictionary<int, string>();

            foreach (TEnum value in Enum.GetValues(enumerationType))
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                string desc = attributes.Length > 0 ? attributes[0].Description : value.ToString();
                if (desc.ToLower() == valor.ToLower())
                { return value; }
            }

            return null;
        }
    }
}
