using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace PigeonCms.Core.Helpers
{
    public static class UrlUtils
    {
        public static string ToUrl(this string inputString)
        {
            string s = String.Empty;

            if (!String.IsNullOrEmpty(inputString))
            {
                s = WebUtility.HtmlDecode(inputString.Trim()).ToLower();

                s = Regex.Replace(s, " - ", "-");
                s = Regex.Replace(s, "à", "a");
                s = Regex.Replace(s, "á", "a");
                s = Regex.Replace(s, "â", "a");
                s = Regex.Replace(s, "ä", "a");
                s = Regex.Replace(s, "Ä", "a");
                s = Regex.Replace(s, "ê", "e");
                s = Regex.Replace(s, "è", "e");
                s = Regex.Replace(s, "é", "e");
                s = Regex.Replace(s, "É", "e");
                s = Regex.Replace(s, "ì", "i");
                s = Regex.Replace(s, "í", "i");
                s = Regex.Replace(s, "ò", "o");
                s = Regex.Replace(s, "ó", "o");
                s = Regex.Replace(s, "ö", "o");
                s = Regex.Replace(s, "Ö", "o");
                s = Regex.Replace(s, "õ", "o");
                s = Regex.Replace(s, "ù", "u");
                s = Regex.Replace(s, "ú", "u");
                s = Regex.Replace(s, "ü", "u");
                s = Regex.Replace(s, "Ü", "u");
                s = Regex.Replace(s, "ß", "ss");

                s = Regex.Replace(s, "¾", "3-4");
                s = Regex.Replace(s, "<", "lt");
                s = Regex.Replace(s, ">", "gt");

                s = Regex.Replace(s, "ñ", "n");
                s = Regex.Replace(s, "ç", "c");
                s = Regex.Replace(s, "%", "");
                s = Regex.Replace(s, "!", "");
                s = Regex.Replace(s, "®", "");
                s = Regex.Replace(s, "™", "");
                s = Regex.Replace(s, ",", "");
                s = Regex.Replace(s, ";", "");
                s = Regex.Replace(s, "\\|", "");
                s = Regex.Replace(s, "\\[", "");
                s = Regex.Replace(s, "\\]", "");

                s = Regex.Replace(s, @"([@–°""./&()’+*”øØ<>=$:?£_ ])", "-");
                s = Regex.Replace(s, @"(-&-|' | '|')", "-");
                s = Regex.Replace(s, @"(---|--)", "-");
                s = Regex.Replace(s, "--", "-");

                if (s.StartsWith("-"))
                {
                    s = s.Substring(1, s.Length - 1);
                }

                if (s.EndsWith("-"))
                {
                    s = s.Substring(0, s.Length - 1);
                }
            }
            else
            {
                s = inputString;
            }

            return s;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
