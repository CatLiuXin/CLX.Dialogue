using System.Collections.Generic;

namespace CLX.Extensions.Raws
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        #region ToInt Functions
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static int ToInt(this string str, int defaultValue)
        {
            int v;
            if (int.TryParse(str, out v))
            {
                return v;
            }
            return defaultValue;
        }

        public static bool TryToInt(this string str, out int value)
        {
            return int.TryParse(str, out value);
        }
        #endregion

        #region ToFloat Functions
        public static float ToFloat(this string str)
        {
            return float.Parse(str);
        }

        public static float ToFloat(this string str, float defaultValue)
        {
            float v;
            if (float.TryParse(str, out v))
            {
                return v;
            }
            return defaultValue;
        }

        public static bool TryToFloat(this string str, out float value)
        {
            return float.TryParse(str, out value);
        }

        #endregion

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string JoinWith(this IEnumerable<string> strings,string separator)
        {
            return string.Join(separator, strings);
        }

    }
}
