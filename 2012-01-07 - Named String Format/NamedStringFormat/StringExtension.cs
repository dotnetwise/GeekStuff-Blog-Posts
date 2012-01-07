using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

namespace NamedStringFormat
{
    public static class StringExtension
    {
        public static string Inject(this string formatString, object injectionObject)
        {
            return formatString.InternalInject(GetPropertyHash(injectionObject), null);
        }

        public static string Inject(this string formatString, object injectionObject, CultureInfo culture)
        {
            return formatString.InternalInject(GetPropertyHash(injectionObject), culture);
        }

        private static string InternalInject(this string formatString, Dictionary<string, object> attributes, CultureInfo culture)
        {
            string result = formatString;
            if (attributes.Count == 0 || formatString == null)
                return result;

            foreach (string attributeKey in attributes.Keys)
            {
                result = result.InternalInjectSingleValue(attributeKey, attributes[attributeKey], culture);
            }
            return result;
        }

        private static string InternalInjectSingleValue(this string formatString, string key, object value, CultureInfo culture)
        {
            string result = formatString;

            MatchCollection matches = Regex.Matches(formatString, "{(" + key + "(.*?))}");
            foreach (Match match in matches)
            {
                string complement = string.Empty;
                string matchValue = match.Groups[1].Value;

                int indexOfFormatStart = matchValue.IndexOfAny(new[] { ',', ':' });
                if (indexOfFormatStart >= 0)
                {
                    complement = matchValue.Substring(indexOfFormatStart);
                    matchValue = matchValue.Substring(0, indexOfFormatStart);
                }

                object replacedValue = value;
                if (matchValue.Contains("."))
                {
                    string propertyPath = matchValue.Substring(matchValue.IndexOf(".") + 1);

                    string[] properties = propertyPath.Split('.');
                    foreach (var propertyName in properties)
                    {
                        var property = replacedValue.GetType().GetProperty(propertyName);
                        if (property == null)
                            return result;

                        replacedValue = property.GetValue(replacedValue, null);
                    }
                }

                string innerFormat = "{0}";
                if (complement.Length > 0)
                    innerFormat = string.Concat("{0", complement, "}");

                string replaceWith = string.Format(culture, innerFormat, replacedValue);
                result = result.Replace(match.ToString(), replaceWith);
            }
            return result;

        }

        private static Dictionary<string, object> GetPropertyHash(object properties)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            if (properties != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(properties);
                foreach (PropertyDescriptor prop in props)
                {
                    values.Add(prop.Name, prop.GetValue(properties));
                }
            }
            return values;
        }

    }
}
