using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Web.Routing;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;


namespace PigeonCms
{
    /// <summary>
    /// Useful static functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// useful methods to manage json data
        /// </summary>
        public class Json
        {
            public static string ParseString(string fieldName, JObject jsData)
            {
                string res = "";
                try
                {
                    res = (string)jsData[fieldName];
                }
                catch (Exception)
                {
                    PigeonCms.Tracer.Log("Json.ParseString - Invalid Json " + fieldName + " field [" + jsData.ToString() + "]");
                }
                return res;
            }

            public static decimal ParseDecimal(string fieldName, JObject jsData)
            {
                decimal res = 0;
                try
                {
                    res = (decimal)jsData[fieldName];
                }
                catch (Exception)
                {
                    PigeonCms.Tracer.Log("JsonParseDecimal - Invalid Json " + fieldName + " field [" + jsData.ToString() + "]");
                }
                return res;
            }
        }

        /// <summary>
        /// useful methods to manage to work with html
        /// </summary>
        public class Html
        {

            /// <summary>
            /// parse textToParse filling existing [[placeholders]] e {{session_vars}}
            /// </summary>
            /// <param name="textToParse">text to parse</param>
            /// <returns>text parsed</returns>
            public static string FillPlaceholders(string textToParse)
            {
                string key = "";
                string value = "";

                if (!string.IsNullOrEmpty(textToParse))
                {
                    //fill placeholders
                    //ex: [[yourname]] --> nicola
                    Regex regPlace = new Regex("(\\[\\[[a-zA-Z_][a-zA-Z0-9_]+\\]\\])");
                    string[] placeholderTags;
                    placeholderTags = regPlace.Split(textToParse);

                    for (int i = 0; i < placeholderTags.Length; i++)
                    {
                        key = placeholderTags[i];
                        if (key.StartsWith("[[") && key.EndsWith("]]"))
                        {
                            key = key.Remove(0, 2);
                            key = key.Remove(key.Length - 2);
                            value = "";
                            Placeholder ph1 = new PlaceholdersManager().GetByName(key);
                            if (ph1.Visible)
                            {
                                value = ph1.Content;
                            }

                            Regex r1 = new Regex("(\\[\\[" + key + "\\]\\])");
                            textToParse = r1.Replace(textToParse, value);
                        }
                    }

                    //fill session vars
                    //ex: {{yourname}} --> session["yourname"] --> nicola
                    Regex regSessions = new Regex("(\\{\\{[a-zA-Z_][a-zA-Z0-9_]+\\}\\})");
                    string[] sessionTags;
                    sessionTags = regSessions.Split(textToParse);

                    for (int i = 0; i < sessionTags.Length; i++)
                    {
                        key = sessionTags[i];
                        if (key.StartsWith("{{") && key.EndsWith("}}"))
                        {
                            key = key.Remove(0, 2);
                            key = key.Remove(key.Length - 2);
                            value = "";
                            if (HttpContext.Current.Session[key] != null)
                                value = HttpContext.Current.Session[key].ToString();

                            Regex r1 = new Regex("(\\{\\{" + key + "\\}\\})");
                            textToParse = r1.Replace(textToParse, value);
                        }
                    }

                    //parse special chars
                    //ex: \[\]\{\} --> []{}
                    textToParse = textToParse.Replace("\\[", "[");
                    textToParse = textToParse.Replace("\\]", "]");
                    textToParse = textToParse.Replace("\\{", "{");
                    textToParse = textToParse.Replace("\\}", "}");
                }
                return textToParse;
            }

            /// <summary>
            /// create a text preview
            /// </summary>
            /// <param name="theText">the text to preview</param>
            /// <param name="charNo">num of chars in result string. 
            /// 0: no preview
            /// -1: empty result
            /// >0: return the text preview</param>
            /// <param name="textEtcetera">default: ...</param>
            /// <param name="removeTags">chenage theText in plain text, removing all html tags</param>
            /// <returns>the preview text</returns>
            public static string GetTextPreview(string theText, int charNo, string textEtcetera, bool removeTags)
            {
                const int MAX_EXTRALEN = 5;
                string res = "";
                if (textEtcetera == "")
                    textEtcetera = "...";

                if (!string.IsNullOrEmpty(theText))
                    res = theText;

                if (charNo > 0)
                {
                    if (removeTags)
                        res = Html.StripTagsCharArray(res);

                    int i = charNo;
                    char c = ' ';
                    while (i < res.Length)
                    {
                        c = res[i];
                        if (res[i] == ' ' || i == charNo + MAX_EXTRALEN)
                        {
                            res = res.Substring(0, i) + textEtcetera;
                            break;
                        }
                        i++;
                    }
                }
                else if (charNo == -1)
                {
                    res = "";
                }
                else if (charNo == 0)
                { }
                return res;
            }

            public static string GetTextPreview(string theText, int charNo, string textEtcetera)
            {
                return GetTextPreview(theText, charNo, textEtcetera, true);
            }

            /// <summary>
            /// returns the text until readmore system tag
            /// if tag does not exist returns empty text
            /// </summary>
            /// <param name="theText">the text to parse</param>
            /// <returns></returns>
            public static string GetTextIntro(string theText)
            {
                string res = "";
                int readMoreIndex = theText.LastIndexOf(ContentEditorProvider.SystemReadMoreTag);
                if (readMoreIndex > 0)
                    res = theText.Substring(0, readMoreIndex);
                return res;
                //TODO: close not closed tags if any, 
                //see http://www.kad1r.com/article.aspx?articleId=190&Category=ASP.Net&title=Read-more-or-continue-reading-function-in-Asp.net
            }

            /// <summary>
            /// rempove the system tags before render the text on browser
            /// </summary>
            /// <param name="theText"></param>
            /// <returns>the clean text</returns>
            public static string RemoveSystemTags(string theText)
            {
                string res = theText;
                if (!string.IsNullOrEmpty(res))
                {
                    res = res.Replace(ContentEditorProvider.SystemReadMoreTag, "");
                    res = res.Replace(ContentEditorProvider.SystemPagebreakTag, "");
                }
                return res;
            }

            /// <summary>
            /// Remove HTML from string with Regex.
            /// speed: 1(slow)
            /// see url: http://dotnetperls.com/remove-html-tags
            /// see url: http://dotnetperls.com/xhtml-validator
            /// </summary>
            public static string StripTagsRegex(string source)
            {
                return Regex.Replace(source, "<.*?>", string.Empty);
            }

            /// <summary>
            /// Compiled regular expression for performance.
            /// </summary>
            static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

            /// <summary>
            /// Remove HTML from string with compiled Regex.
            /// speed: 2(medium)
            /// see url: http://dotnetperls.com/remove-html-tags
            /// see url: http://dotnetperls.com/xhtml-validator
            /// </summary>
            public static string StripTagsRegexCompiled(string source)
            {
                return _htmlRegex.Replace(source, string.Empty);
            }

            /// <summary>
            /// Remove HTML tags from string using char array.
            /// speed: 3(fast)
            /// see url: http://dotnetperls.com/remove-html-tags
            /// see url: http://dotnetperls.com/xhtml-validator
            /// </summary>
            public static string StripTagsCharArray(string source)
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                return new string(array, 0, arrayIndex);
            }

            /// <summary>
            /// Whether the HTML is likely valid. Error parameter will be empty
            /// if no errors were found.
            /// </summary>
            static public void CheckHtml(string html, out string error)
            {
                // Store our tags in a stack
                Stack<string> tags = new Stack<string>();

                // Initialize out parameter to empty
                error = string.Empty;

                // Count of parenthesis
                int parenthesisR = 0;
                int parenthesisL = 0;

                // Traverse entire HTML
                for (int i = 0; i < html.Length; i++)
                {
                    char c = html[i];
                    if (c == '<')
                    {
                        bool isClose;
                        bool isSolo;

                        // Look ahead at this tag
                        string tag = lookAhead(html, i, out isClose, out isSolo);

                        // Make sure tag is lowercase
                        if (tag.ToLower() != tag)
                        {
                            error = "upper: " + tag;
                            return;
                        }

                        // Make sure solo tags are parsed as solo tags
                        if (_soloTags.ContainsKey(tag))
                        {
                            if (!isSolo)
                            {
                                error = "!solo: " + tag;
                                return;
                            }
                        }
                        else
                        {
                            // We are on a regular end or start tag
                            if (isClose)
                            {
                                // We can't close a tag that isn't on the stack
                                if (tags.Count == 0)
                                {
                                    error = "closing: " + tag;
                                    return;
                                }

                                // Tag on stack must be equal to this closing tag
                                if (tags.Peek() == tag)
                                {
                                    // Remove the start tag from the stack
                                    tags.Pop();
                                }
                                else
                                {
                                    // Mismatched closing tag
                                    error = "!match: " + tag;
                                    return;
                                }
                            }
                            else
                            {
                                // Add tag to stack
                                tags.Push(tag);
                            }
                        }
                        i += tag.Length;
                    }
                    else if (c == '&')
                    {
                        // & must never be followed by space or other &
                        if ((i + 1) < html.Length)
                        {
                            char next = html[i + 1];

                            if (char.IsWhiteSpace(next) ||
                                next == '&')
                            {
                                error = "ampersand";
                                return;
                            }
                        }
                    }
                    else if (c == '\t')
                    {
                        error = "tab";
                        return;
                    }
                    else if (c == '(')
                    {
                        parenthesisL++;
                    }
                    else if (c == ')')
                    {
                        parenthesisR++;
                    }
                }

                // If we have tags in the stack, write them to error
                foreach (string tagName in tags)
                {
                    error += "extra:" + tagName + " ";
                }

                // Require even number of parenthesis
                if (parenthesisL != parenthesisR)
                {
                    error = "!even ";
                }
            }

            /// <summary>
            /// Called at the start of an html tag. We look forward and record information
            /// about our tag. Handles start tags, close tags, and solo tags. 'Collects'
            /// an entire tag.
            /// </summary>
            /// <returns>Tag name.</returns>
            static private string lookAhead(string html, int start, out bool isClose,
                out bool isSolo)
            {
                isClose = false;
                isSolo = false;

                StringBuilder tagName = new StringBuilder();
                int slashPos = -1;      // Stores the position of the final slash
                bool space = false;     // Whether we have encountered a space
                bool quote = false;     // Whether we are in a quote

                // Begin scanning the tag
                int i;
                for (i = 0; ; i++)
                {
                    // Get the position in main html
                    int pos = start + i;

                    // Don't go outside the html
                    if (pos >= html.Length)
                    {
                        return "x";
                    }

                    // The character we are looking at
                    char c = html[pos];

                    // See if a space has been encountered
                    if (char.IsWhiteSpace(c))
                    {
                        space = true;
                    }

                    // Add to our tag name if none of these are present
                    if (space == false &&
                        c != '<' &&
                        c != '>' &&
                        c != '/')
                    {
                        tagName.Append(c);
                    }

                    // Record position of slash if not inside a quoted area
                    if (c == '/' &&
                        quote == false)
                    {
                        slashPos = i;
                    }

                    // End at the > bracket
                    if (c == '>')
                    {
                        break;
                    }

                    // Record whether we are in a quoted area
                    if (c == '\"')
                    {
                        quote = !quote;
                    }
                }

                // Determine if this is a solo or closing tag
                if (slashPos != -1)
                {
                    // If slash is at the end so this is solo
                    if (slashPos + 1 == i)
                    {
                        isSolo = true;
                    }
                    else
                    {
                        isClose = true;
                    }
                }

                // Return the name of the tag collected
                string name = tagName.ToString();
                if (name.Length == 0)
                {
                    return "empty";
                }
                else
                {
                    return name;
                }
            }

            /// <summary>
            /// Tags that must be closed in the start
            /// </summary>
            static Dictionary<string, bool> _soloTags = new Dictionary<string, bool>() 
            { 
                {"img", true},{"br", true} 
            };

        }

        /// <summary>
        /// useful methods for mobile devices
        /// </summary>
        public class Mobile
        {
            public enum DeviceTypeEnum
            {
                Android,
                Iphone,
                Ipad,
                Kindle,
                Blackbarry,
                Unknown
            }

            public static bool IsMobileDevice(HttpContext ctx)
            {
                string userAgent = ctx.Request.UserAgent.ToLower();

                if (userAgent.Contains("blackberry")
                      || userAgent.Contains("iphone")
                      || userAgent.Contains("ipad")
                      || userAgent.Contains("kindle")
                      || userAgent.Contains("android"))
                    return true;

                return ctx.Request.Browser.IsMobileDevice;
            }

            public static DeviceTypeEnum DeviceType(HttpContext ctx)
            {
                DeviceTypeEnum res = DeviceTypeEnum.Unknown;
                string userAgent = ctx.Request.UserAgent.ToLower();
                switch (userAgent)
                {
                    case "android":
                        res = DeviceTypeEnum.Android;
                        break;
                    case "blackberry":
                        res = DeviceTypeEnum.Blackbarry;
                        break;
                    case "iphone":
                        res = DeviceTypeEnum.Iphone;
                        break;
                    case "ipad":
                        res = DeviceTypeEnum.Ipad;
                        break;
                    case "kindle":
                        res = DeviceTypeEnum.Kindle;
                        break;
                }
                return res;
            }
        }

        /// <summary>
        /// useful methods to manage js scripts
        /// </summary>
        public class Script
        {
            public static void RegisterClientScriptBlock(Control control, string key, string script)
            {
                RegisterClientScriptBlock(control, key, script, true);
            }

            public static void RegisterClientScriptBlock(Control control, string key, string script, bool addScriptTags)
            {
                ScriptManager.RegisterClientScriptBlock(control, typeof(bool)/*GetType()*/, key, script, addScriptTags);
            }

            public static void RegisterClientScriptInclude(Control control, string key, string url)
            {
                ScriptManager.RegisterClientScriptInclude(control, typeof(bool)/*GetType()*/, key, url);
            }

            public static void RegisterClientScriptInclude(Page page, string key, string url)
            {
                ScriptManager.RegisterClientScriptInclude(page, typeof(bool)/*GetType()*/, key, url);
            }

            public static void RegisterStartupScript(Control control, string key, string script)
            {
                RegisterStartupScript(control, key, script, true);
            }

            public static void RegisterStartupScript(Control control, string key, string script, bool addScriptTags)
            {
                ScriptManager.RegisterStartupScript(control, typeof(bool), key, script, addScriptTags);
            }

            /// <summary>
            /// Encodes a string to be represented as a string literal. The format
            /// is essentially a JSON string.
            /// The string returned includes outer quotes 
            /// Example Output: "Hello \"Rick\"!\r\nRock on"
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public static string EncodeJsString(string s)
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append("\"");

                if (!string.IsNullOrEmpty(s))
                {
                    foreach (char c in s)
                    {
                        switch (c)
                        {
                            case '\"':
                                sb.Append("\\\"");
                                break;
                            case '\'':
                                sb.Append("\\\'");
                                break;
                            case '\\':
                                sb.Append("\\\\");
                                break;
                            case '\b':
                                sb.Append("\\b");
                                break;
                            case '\f':
                                sb.Append("\\f");
                                break;
                            case '\n':
                                sb.Append("\\n");
                                break;
                            case '\r':
                                sb.Append("\\r");
                                break;
                            case '\t':
                                sb.Append("\\t");
                                break;
                            default:
                                int i = (int)c;
                                if (i < 32 || i > 127)
                                {
                                    sb.AppendFormat("\\u{0:X04}", i);
                                }
                                else
                                {
                                    sb.Append(c);
                                }
                                break;
                        }
                    }
                }
                //sb.Append("\"");

                return sb.ToString();
            }
        }

        /// <summary>
        /// useful crypt decript methods
        /// </summary>
        public class Encryption
        {
            const string SKIP_KEY = "nn";

            public static string Decrypt(string textToDecrypt, string key)
            {
                byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
                if (key == SKIP_KEY)
                {
                    return Encoding.UTF8.GetString(encryptedData);
                }
                else
                {
                    RijndaelManaged rijndaelCipher = new RijndaelManaged(); //AES
                    rijndaelCipher.Mode = CipherMode.CBC;
                    rijndaelCipher.Padding = PaddingMode.PKCS7;

                    rijndaelCipher.KeySize = 0x80;
                    rijndaelCipher.BlockSize = 0x80;
                    //byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                    byte[] keyBytes = new byte[0x10];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length)
                    {
                        len = keyBytes.Length;
                    }
                    Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    rijndaelCipher.IV = keyBytes;
                    byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    return Encoding.UTF8.GetString(plainText);
                }
            }

            public static string Encrypt(string textToEncrypt, string key)
            {
                byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
                if (key == SKIP_KEY)
                {
                    return Convert.ToBase64String(plainText);
                }
                else
                {
                    RijndaelManaged rijndaelCipher = new RijndaelManaged();
                    rijndaelCipher.Mode = CipherMode.CBC;
                    rijndaelCipher.Padding = PaddingMode.PKCS7;

                    rijndaelCipher.KeySize = 0x80;
                    rijndaelCipher.BlockSize = 0x80;
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
                    byte[] keyBytes = new byte[0x10];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length)
                    {
                        len = keyBytes.Length;
                    }
                    Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    rijndaelCipher.IV = keyBytes;
                    ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                    //byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
                    return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
                }
            }
        }

        public class StaticData
        {
            /*public class Nation
            {
                public string Code = "";
                public string Name = "";
                public bool VatEnabled = true;

                public Nation(string code, string name, bool vatEnabled)
                {
                    this.Code = code;
                    this.Name = name;
                    this.VatEnabled = vatEnabled;
                }
            }*/

            public static Dictionary<string, string> Nations
            {
                get
                {
                    var list = new Dictionary<string, string>();
                    list.Add("IT", "Italy");
                    list.Add("AF", "Afghanistan");
                    list.Add("AL", "Albania");
                    list.Add("DZ", "Algeria");
                    list.Add("AS", "American Samoa");
                    list.Add("AD", "Andorra");
                    list.Add("AO", "Angola");
                    list.Add("AI", "Anguilla");
                    list.Add("AQ", "Antarctica");
                    list.Add("AG", "Antigua And Barbuda");
                    list.Add("AR", "Argentina");
                    list.Add("AM", "Armenia");
                    list.Add("AW", "Aruba");
                    list.Add("AU", "Australia");
                    list.Add("AT", "Austria");
                    list.Add("AZ", "Azerbaijan");
                    list.Add("BS", "Bahamas");
                    list.Add("BH", "Bahrain");
                    list.Add("BD", "Bangladesh");
                    list.Add("BB", "Barbados");
                    list.Add("BY", "Belarus");
                    list.Add("BE", "Belgium");
                    list.Add("BZ", "Belize");
                    list.Add("BJ", "Benin");
                    list.Add("BM", "Bermuda");
                    list.Add("BT", "Bhutan");
                    list.Add("BO", "Bolivia, Plurinational State Of");
                    list.Add("BA", "Bosnia And Herzegovina");
                    list.Add("BW", "Botswana");
                    list.Add("BV", "Bouvet Island");
                    list.Add("BR", "Brazil");
                    list.Add("IO", "British Indian Ocean Territory");
                    list.Add("BN", "Brunei Darussalam");
                    list.Add("BG", "Bulgaria");
                    list.Add("BF", "Burkina Faso");
                    list.Add("BI", "Burundi");
                    list.Add("KH", "Cambodia");
                    list.Add("CM", "Cameroon");
                    list.Add("CA", "Canada");
                    list.Add("CV", "Cape Verde");
                    list.Add("KY", "Cayman Islands");
                    list.Add("CF", "Central African Republic");
                    list.Add("TD", "Chad");
                    list.Add("CL", "Chile");
                    list.Add("CN", "China");
                    list.Add("CX", "Christmas Island");
                    list.Add("CC", "Cocos (Keeling) Islands");
                    list.Add("CO", "Colombia");
                    list.Add("KM", "Comoros");
                    list.Add("CG", "Congo");
                    list.Add("CD", "Congo, The Democratic Republic Of The");
                    list.Add("CK", "Cook Islands");
                    list.Add("CR", "Costa Rica");
                    list.Add("CI", "Côte D'Ivoire");
                    list.Add("HR", "Croatia");
                    list.Add("CU", "Cuba");
                    list.Add("CY", "Cyprus");
                    list.Add("CZ", "Czech Republic");
                    list.Add("DK", "Denmark");
                    list.Add("DJ", "Djibouti");
                    list.Add("DM", "Dominica");
                    list.Add("DO", "Dominican Republic");
                    list.Add("EC", "Ecuador");
                    list.Add("EG", "Egypt");
                    list.Add("SV", "El Salvador");
                    list.Add("GQ", "Equatorial Guinea");
                    list.Add("ER", "Eritrea");
                    list.Add("EE", "Estonia");
                    list.Add("ET", "Ethiopia");
                    list.Add("FK", "Falkland Islands (Malvinas)");
                    list.Add("FO", "Faroe Islands");
                    list.Add("FJ", "Fiji");
                    list.Add("FI", "Finland");
                    list.Add("FR", "France");
                    list.Add("GF", "French Guiana");
                    list.Add("PF", "French Polynesia");
                    list.Add("TF", "French Southern Territories");
                    list.Add("GA", "Gabon");
                    list.Add("GM", "Gambia");
                    list.Add("GE", "Georgia");
                    list.Add("DE", "Germany");
                    list.Add("GH", "Ghana");
                    list.Add("GI", "Gibraltar");
                    list.Add("GR", "Greece");
                    list.Add("GL", "Greenland");
                    list.Add("GD", "Grenada");
                    list.Add("GP", "Guadeloupe");
                    list.Add("GU", "Guam");
                    list.Add("GT", "Guatemala");
                    list.Add("GG", "Guernsey");
                    list.Add("GN", "Guinea");
                    list.Add("GW", "Guinea-Bissau");
                    list.Add("GY", "Guyana");
                    list.Add("HT", "Haiti");
                    list.Add("HM", "Heard Island And Mcdonald Islands");
                    list.Add("HN", "Honduras");
                    list.Add("HK", "Hong Kong");
                    list.Add("HU", "Hungary");
                    list.Add("IS", "Iceland");
                    list.Add("IN", "India");
                    list.Add("ID", "Indonesia");
                    list.Add("IR", "Iran, Islamic Republic Of");
                    list.Add("IQ", "Iraq");
                    list.Add("IE", "Ireland");
                    list.Add("IM", "Isle Of Man");
                    list.Add("IL", "Israel");
                    list.Add("JM", "Jamaica");
                    list.Add("JP", "Japan");
                    list.Add("JE", "Jersey");
                    list.Add("JO", "Jordan");
                    list.Add("KZ", "Kazakhstan");
                    list.Add("KE", "Kenya");
                    list.Add("KI", "Kiribati");
                    list.Add("KP", "Korea, Democratic People's Republic Of");
                    list.Add("KR", "Korea, Republic Of");
                    list.Add("KW", "Kuwait");
                    list.Add("KG", "Kyrgyzstan");
                    list.Add("LA", "Lao People's Democratic Republic");
                    list.Add("LV", "Latvia");
                    list.Add("LB", "Lebanon");
                    list.Add("LS", "Lesotho");
                    list.Add("LR", "Liberia");
                    list.Add("LY", "Libyan Arab Jamahiriya");
                    list.Add("LI", "Liechtenstein");
                    list.Add("LT", "Lithuania");
                    list.Add("LU", "Luxembourg");
                    list.Add("MO", "Macao");
                    list.Add("MK", "Macedonia, The Former Yugoslav Republic Of");
                    list.Add("MG", "Madagascar");
                    list.Add("MW", "Malawi");
                    list.Add("MY", "Malaysia");
                    list.Add("MV", "Maldives");
                    list.Add("ML", "Mali");
                    list.Add("MT", "Malta");
                    list.Add("MH", "Marshall Islands");
                    list.Add("MQ", "Martinique");
                    list.Add("MR", "Mauritania");
                    list.Add("MU", "Mauritius");
                    list.Add("YT", "Mayotte");
                    list.Add("MX", "Mexico");
                    list.Add("FM", "Micronesia, Federated States Of");
                    list.Add("MD", "Moldova, Republic Of");
                    list.Add("MC", "Monaco");
                    list.Add("MN", "Mongolia");
                    list.Add("ME", "Montenegro");
                    list.Add("MS", "Montserrat");
                    list.Add("MA", "Morocco");
                    list.Add("MZ", "Mozambique");
                    list.Add("MM", "Myanmar");
                    list.Add("NA", "Namibia");
                    list.Add("NR", "Nauru");
                    list.Add("NP", "Nepal");
                    list.Add("NL", "Netherlands");
                    list.Add("AN", "Netherlands Antilles");
                    list.Add("NC", "New Caledonia");
                    list.Add("NZ", "New Zealand");
                    list.Add("NI", "Nicaragua");
                    list.Add("NE", "Niger");
                    list.Add("NG", "Nigeria");
                    list.Add("NU", "Niue");
                    list.Add("NF", "Norfolk Island");
                    list.Add("MP", "Northern Mariana Islands");
                    list.Add("NO", "Norway");
                    list.Add("OM", "Oman");
                    list.Add("PK", "Pakistan");
                    list.Add("PW", "Palau");
                    list.Add("PS", "Palestinian Territory, Occupied");
                    list.Add("PA", "Panama");
                    list.Add("PG", "Papua New Guinea");
                    list.Add("PY", "Paraguay");
                    list.Add("PE", "Peru");
                    list.Add("PH", "Philippines");
                    list.Add("PN", "Pitcairn");
                    list.Add("PL", "Poland");
                    list.Add("PT", "Portugal");
                    list.Add("PR", "Puerto Rico");
                    list.Add("QA", "Qatar");
                    list.Add("RE", "Réunion");
                    list.Add("RO", "Romania");
                    list.Add("RU", "Russian Federation");
                    list.Add("RW", "Rwanda");
                    list.Add("BL", "Saint Barthélemy");
                    list.Add("SH", "Saint Helena, Ascension And Tristan Da Cunha");
                    list.Add("KN", "Saint Kitts And Nevis");
                    list.Add("LC", "Saint Lucia");
                    list.Add("MF", "Saint Martin");
                    list.Add("PM", "Saint Pierre And Miquelon");
                    list.Add("VC", "Saint Vincent And The Grenadines");
                    list.Add("WS", "Samoa");
                    list.Add("SM", "San Marino");
                    list.Add("ST", "Sao Tome And Principe");
                    list.Add("SA", "Saudi Arabia");
                    list.Add("SN", "Senegal");
                    list.Add("RS", "Serbia");
                    list.Add("SC", "Seychelles");
                    list.Add("SL", "Sierra Leone");
                    list.Add("SG", "Singapore");
                    list.Add("SK", "Slovakia");
                    list.Add("SI", "Slovenia");
                    list.Add("SB", "Solomon Islands");
                    list.Add("SO", "Somalia");
                    list.Add("ZA", "South Africa");
                    list.Add("GS", "South Georgia And The South Sandwich Islands");
                    list.Add("ES", "Spain");
                    list.Add("LK", "Sri Lanka");
                    list.Add("SD", "Sudan");
                    list.Add("SR", "Suriname");
                    list.Add("SJ", "Svalbard And Jan Mayen");
                    list.Add("SZ", "Swaziland");
                    list.Add("SE", "Sweden");
                    list.Add("CH", "Switzerland");
                    list.Add("SY", "Syrian Arab Republic");
                    list.Add("TW", "Taiwan, the Republic of China");
                    list.Add("TJ", "Tajikistan");
                    list.Add("TZ", "Tanzania, United Republic Of");
                    list.Add("TH", "Thailand");
                    list.Add("TL", "Timor-Leste");
                    list.Add("TG", "Togo");
                    list.Add("TK", "Tokelau");
                    list.Add("TO", "Tonga");
                    list.Add("TT", "Trinidad And Tobago");
                    list.Add("TN", "Tunisia");
                    list.Add("TR", "Turkey");
                    list.Add("TM", "Turkmenistan");
                    list.Add("TC", "Turks And Caicos Islands");
                    list.Add("TV", "Tuvalu");
                    list.Add("UG", "Uganda");
                    list.Add("UA", "Ukraine");
                    list.Add("AE", "United Arab Emirates");
                    list.Add("GB", "United Kingdom");
                    list.Add("US", "United States");
                    list.Add("UM", "United States Minor Outlying Islands");
                    list.Add("UY", "Uruguay");
                    list.Add("UZ", "Uzbekistan");
                    list.Add("VU", "Vanuatu");
                    list.Add("VA", "Vatican City State");
                    list.Add("VE", "Venezuela, Bolivarian Republic Of");
                    list.Add("VN", "Viet Nam");
                    list.Add("VG", "Virgin Islands, British");
                    list.Add("VI", "Virgin Islands, U.S.");
                    list.Add("WF", "Wallis And Futuna");
                    list.Add("EH", "Western Sahara");
                    list.Add("YE", "Yemen");
                    list.Add("ZM", "Zambia");
                    list.Add("ZW", "Zimbabwe");
                    list.Add("AX", "Åland Islands");

                    return list;
                }
            }
        }

        /// <summary>
        /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
        /// Provides a method for performing a deep copy of an object.
        /// Binary Serialization is used to perform the copy.
        /// </summary>
        public static class ObjectCopier
        {
            /// <summary>
            /// Perform a deep Copy of the object.
            /// </summary>
            /// <typeparam name="T">The type of object being copied.</typeparam>
            /// <param name="source">The object instance to copy.</param>
            /// <returns>The copied object.</returns>
            public static T Clone<T>(T source)
            {
                if (!typeof(T).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                // Don't serialize a null object, simply return the default for that object
                if (Object.ReferenceEquals(source, null))
                {
                    return default(T);
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
            }
        }

        /// <summary>
        /// Gets the first day of a week where day (parameter) belongs. weekStart (parameter) specifies the starting day of week.
        /// </summary>
        /// <returns></returns> 
        public static DateTime FirstDayOfWeek(DateTime day, DayOfWeek weekStarts)
        {
            DateTime d = day;
            while (d.DayOfWeek != weekStarts)
            {
                d = d.AddDays(-1);
            }
            return d;
        }

        /// <summary>
        /// Add a glyph to the ordered column in a gridview
        /// </summary>
        /// <example>
        ///protected void GridViewProducts_RowCreated(object sender, GridViewRowEventArgs e)
        ///{
        ///    if (e.Row.RowType == DataControlRowType.Header)
        ///        AddGlyph(GridViewProducts, e.Row);
        ///}
        /// </example>
        /// <param name="grid">the GridView</param>
        /// <param name="item">the Row</param>
        public static void AddGlyph(GridView grid, GridViewRow item)
        {
            //Label glyph = new Label();
            //glyph.EnableTheming = false;
            //glyph.Font.Name = "Webdings";
            //glyph.Font.Size = FontUnit.XSmall;
            //glyph.Font.Bold = true;
            //glyph.ForeColor = System.Drawing.Color.DarkGray;
            //glyph.Text = (grid.SortDirection == SortDirection.Ascending ? "?" : "?");

            Label space = new Label();
            space.Text = " ";

            Image imgSort = new Image();
            imgSort.SkinID = (grid.SortDirection == SortDirection.Ascending ? "ImgSortAsc" : "ImgSortDesc");

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                string colExpr = grid.Columns[i].SortExpression;
                if (colExpr != "" && colExpr == grid.SortExpression)
                    //item.Cells[i].Controls.Add(space);
                    item.Cells[i].Controls.Add(imgSort);
            }
        }

        /// <summary>
        /// add onclick script to allow static file tracking
        /// </summary>
        /// <param name="trackParam">the param in for the script</param>
        /// <param name="addScript">false:do nothing</param>
        /// <returns></returns>
        public static string AddTracking(string trackParam, bool addScript)
        {
            string res = "";
            string currUrl = HttpContext.Current.Request.FilePath + "#";
            if (addScript && !string.IsNullOrEmpty(trackParam))
                res = " onclick=\"pageTracker._trackPageview('"+ currUrl + trackParam + "');\" ";
            return res;
        }

        /// <summary>
        /// add onclick script to allow static file tracking
        /// </summary>
        /// <param name="trakParams">list of params used to build the param path</param>
        /// <param name="addScript">false:do nothing</param>
        /// <returns></returns>
        public static string AddTracking(string[]trakParams, bool addScript)
        {
            string res = "";
            if (trakParams != null)
            {
                string path = "";
                foreach (string param in trakParams)
                {
                    if (!string.IsNullOrEmpty(param))
                        path += "/" + param;
                }
                res = AddTracking(path, addScript);
            }
            return res;
        }

        /// <summary>
        /// transform an url in a valid alias
        /// </summary>
        /// <param name="url">url or string to sanitize</param>
        /// <returns>a valid url</returns>
        public static string GetUrlAlias(string url)
        {
            string res = url.ToLower();
            res = res.Replace(" ","-");
            res = HttpUtility.UrlEncode(res);
            return res;
        }

        public static string GetCurrCultureName()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month">the month</param>
        /// <returns>month name in current culture</returns>
        public static string GetMonthName(int month)
        {
            string res = "";
            string label = "month";
            if (month >= 1 && month <= 12)
            {
                label += month.ToString();
                res = GetLabel(label);
            }
            return res;
        }

        public static string GetAbsoluteUrl()
        {
            return GetAbsoluteUrl("");
        }

        public static string GetAbsoluteUrl(string url)
        {
            return GetAbsoluteUrl(url, false);
        }


        public static string GetAbsoluteUrl(string url, bool useSsl)
        {
            string protocol = "http://";
            if (useSsl)
                protocol = "https://";

            if (url.StartsWith("http://"))
                url = url.Replace("http://", "");

            if (url.StartsWith("https://"))
                url = url.Replace("https://", "");

            string res = protocol +
                HttpContext.Current.Request.Url.Authority +
                HttpContext.Current.Request.ApplicationPath;
            if (!res.EndsWith("/")) res += "/";
            if (url.StartsWith("/"))
                url = url.Substring(1);
            res += url;
            return res;
        }

        public static string GetRoutedUrl(Menu menuEntry)
        {
            return GetRoutedUrl(menuEntry, null, "", Config.AddPageSuffix);
        }

        public static string GetRoutedUrl(Menu menuEntry, string queryString, bool addPagePostFix)
        {
            return GetRoutedUrl(menuEntry, null, queryString, addPagePostFix);
        }

        public static string GetRoutedUrl(Menu menuEntry, 
            RouteValueDictionary routeValueDictionary, string queryString, bool addPagePostFix)
        {
            var res = new StringBuilder();

            try
            {
                var defaults = new RouteValueDictionary { { "pagename", menuEntry.Alias.ToLower() } };
                if (routeValueDictionary != null)
                {
                    foreach (var item in routeValueDictionary)
                    {
                        if (defaults.ContainsKey(item.Key)) 
                            defaults.Remove(item.Key);
                        defaults.Add(item.Key, item.Value);
                    }
                }
                res = res.Append(
                    RouteTable.Routes.GetVirtualPath(
                    null, menuEntry.RouteName, defaults).VirtualPath
                );
                if (addPagePostFix)
                    res = res.Append(".aspx");
                if (!string.IsNullOrEmpty(queryString))
                {
                    res = res.Append("?");
                    res = res.Append(queryString);
                }
            }
            catch (Exception ex)
            {
                Tracer.Log("GetRoutedUrl, missing RouteValueDictionary: " + ex.ToString(), TracerItemType.Error);
                throw new ArgumentException("missing RouteValueDictionary", ex);
            }
            return res.ToString();
        }

        public static string GetThemedImageSrc(string fileName)
        {
            return GetThemedImageSrc(fileName, "", "");
        }

        public static string GetThemedImageSrc(string fileName, string pageTheme)
        {
            return GetThemedImageSrc(fileName, pageTheme, "");
        }

        /// <summary>
        /// get image url for current page theme
        /// </summary>
        /// <param name="fileName">the image file name</param>
        /// <param name="pageTheme">name of theme; "" for current theme</param>
        /// <param name="defaultFileName">file to use if filename is not found; "" for no default file</param>
        /// <returns>absolute path of the image</returns>
        public static string GetThemedImageSrc(string fileName, string pageTheme, string defaultFileName)
        {
            string res = "";
            if (string.IsNullOrEmpty(pageTheme))
            {
                Page page = (Page)HttpContext.Current.CurrentHandler;
                pageTheme = page.Theme;
            }
            if (!string.IsNullOrEmpty(defaultFileName))
            {
                if (File.Exists(
                    HttpContext.Current.Server.MapPath(
                    "~/App_Themes/" + pageTheme + "/Images/" + fileName)))
                    res = VirtualPathUtility.ToAbsolute("~/") + "App_Themes/" + pageTheme + "/Images/" + fileName;
                else
                    res = VirtualPathUtility.ToAbsolute("~/") + "App_Themes/" + pageTheme + "/Images/" + defaultFileName;
            }
            else
                res = VirtualPathUtility.ToAbsolute("~/") + "App_Themes/" + pageTheme + "/Images/" + fileName;

            return res;
        }

        /// <summary>
        /// get css url for current page theme
        /// </summary>
        /// <param name="fileName">the css file name</param>
        /// <returns></returns>
        public static string GetThemedCssSrc(string fileName)
        {
            Page page = (Page)HttpContext.Current.CurrentHandler;
            return GetThemedCssSrc(fileName, page.Theme);
        }

        public static string GetThemedCssSrc(string fileName, string pageTheme)
        {
            string res = VirtualPathUtility.ToAbsolute("~/") + "App_Themes/" + pageTheme /*Config.CurrentTheme*/ + "/" + fileName;
            return res;
        }

        public static string GetLabel(string stringKey)
        {
            string res = "";
            if (HttpContext.GetGlobalResourceObject("PublicLabels", stringKey) != null)
                res = HttpContext.GetGlobalResourceObject("PublicLabels", stringKey).ToString();
            //if (Resources.AdminLabels.ResourceManager.GetString(stringKey) != null)
            //    res = Resources.AdminLabels.ResourceManager.GetString(stringKey);
            
            return res;
        }

        public static string GetLabel(string stringKey, string defaultValue)
        {
            string res = GetLabel(stringKey);
            if (res == "")
                res = defaultValue;
            return res;
        }

        public static string GetLabel(string stringKey, string defaultValue, Control targetControl)
        {
            return GetLabel(stringKey, defaultValue, targetControl, "");
        }

        public static string GetLabel(string stringKey, string defaultValue, Control targetControl, string title)
        {
            if (!string.IsNullOrEmpty(title))
                title = "title='" + title + "'";
            string clientID = "";
            if (targetControl != null)
                clientID = targetControl.ClientID;

            string res = GetLabel(stringKey, defaultValue);
            res = "<label for='" + clientID + "' " + title + ">" + res + "</label>";
            return res;
        }

        public static string GetErrorLabel(string stringKey, string defaultValue)
        {
            string res = GetLabel("Err" + stringKey);
            if (res == "")
                res = defaultValue;
            return "<span class='error'>" + res + "</span>";
        }

        public static string List2String(List<string> stringList)
        {
            return List2String(stringList, "|");
        }

        public static string List2String(List<string> stringList, string separator)
        {
            string res = "";
            foreach (string item in stringList)
            {
                if (!string.IsNullOrEmpty(item))
                    res += item + separator;
            }
            return res;
        }

        public static List<string> String2List(string theString)
        {
            return String2List(theString, '|');
        }

        public static List<string> String2List(string theString, char stringSepataror)
        {
            var res = new List<string>();
            if (theString != null)
            {
                string[] arr = theString.Split(stringSepataror);
                foreach (string item in arr)
                {
                    string item2Add = item;
                    if (stringSepataror == '|')
                        item2Add = item.Replace("{{tilde}}", "|");  //tilde esape sequence

                    res.Add(item2Add);
                }
            }
            return res;
        }

        public static List<string> String2List(string theString, string stringSepataror)
        {
            var res = new List<string>();
            string[] splitter = { stringSepataror };
            if (theString != null)
            {
                string[] arr = theString.Split(splitter, StringSplitOptions.None);
                foreach (string item in arr)
                {
                    res.Add(item);
                }
            }
            return res;
        }

        public static List<string> RemoveDuplicatesFromList(List<string> stringList)
        {
            var res = new List<string>();
            foreach (string item in stringList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!res.Contains(item)) res.Add(item);
                }
            }
            return res;
        }

        /// <summary>
        /// get params dictionary result from serialize string
        /// ex: paramString = "par1:=xx|par2:=yy"
        /// result={par1,xx},{par2,yy}
        /// </summary>
        /// <param name="paramString">inline serialized params</param>
        /// <returns>dictionary object, (paramkey,paramvalue)</returns>
        public static Dictionary<string, string> GetParamsDictFromString(string paramString)
        {
            string[] splitter = { ":=" };
            Dictionary<String, String> result = new Dictionary<string, string>();
            List<string> paramsList = Utility.String2List(paramString);
            foreach (string item in paramsList)
            {
                string[] arr = item.Split(splitter, StringSplitOptions.None);
                string key = "";
                if (arr.Length > 0) key = arr[0];
                string value = "";
                if (arr.Length > 1) value = arr[1];
                if (!string.IsNullOrEmpty(key))
                {
                    result.Add(key, value);
                }
            }
            return result;
        }

        /// <summary>
        /// set drop selected value
        /// </summary>
        /// <param name="drop">the DropDownList</param>
        /// <param name="value">the value</param>
        public static void SetDropByValue(DropDownList drop, string value)
        {
            drop.SelectedIndex = drop.Items.IndexOf(drop.Items.FindByValue(value));
        }

        /// <summary>
        /// set radio selected value
        /// </summary>
        /// <param name="radio">the RadioButtonList</param>
        /// <param name="value">the value</param>
        public static void SetRadioListByValue(RadioButtonList radio, string value)
        {
            radio.SelectedIndex = radio.Items.IndexOf(radio.Items.FindByValue(value));
        }

        /// <summary>
        /// set ListItem values
        /// </summary>
        /// <param name="list">the ListBox control</param>
        /// <param name="values">a list of string</param>
        public static void SetListBoxByValues(ListControl list, List<string> values, bool clearSelectionBefore)
        {
            if (clearSelectionBefore)
                list.ClearSelection();

            foreach (ListItem item in list.Items)
            {
                if (values.Contains(item.Value))
                    item.Selected = true;
            }
        }

        public static void SetListBoxByValues(ListControl list, List<string> values)
        {
            SetListBoxByValues(list, values, true);
        }

        public static void SetListBoxByValues(ListControl list, string value)
        {
            List<string> values = new List<string>();
            values.Add(value);
            SetListBoxByValues(list, values, true);
        }

        public static void SetListBoxByValues(ListControl list, int value)
        {
            List<string> values = new List<string>();
            values.Add(value.ToString());
            SetListBoxByValues(list, values, true);
        }

        public static void SetListBoxByValues(ListControl list, List<int> values)
        {
            List<string> stringValues = new List<string>();
            foreach (int item in values)
            {
                stringValues.Add(item.ToString());
            }
            SetListBoxByValues(list, stringValues, true);
        }

        /// <summary>
        /// check if inputemail is syntactically valid or not
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string inputEmail)
        {
            if (string.IsNullOrEmpty(inputEmail))
                return false;

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(inputEmail);
        }

        /// <summary>
        /// tell if the field is empty or contains the default fck value
        /// that can be deleted only in 'source code' fck editor mode
        /// </summary>
        /// <param name="fieldValue">the fck field content</param>
        /// <returns></returns>
        public static bool IsEmptyFckField(string fieldValue)
        {
            bool result = false;
            if (string.IsNullOrEmpty(fieldValue))
                result = true;
            else
            {
                if (fieldValue.Trim() == "<br />")
                    result = true;
                else if (fieldValue.Trim() == "<br type=\"_moz\" />")
                    result = true;
                else if (fieldValue.Trim() == "&nbsp;")
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// <see cref="Utility._Session()" />
        /// </summary>
        public static Object _Session(string name)
        {
            return _Session(name, false);
        }

        /// <summary>
        /// retrieve a session obj using HttpContext.Current.Session[name] to prevent NullReferenceException
        /// given by Page.Session[name] on session timed out
        /// </summary>
        /// <param name="name">identify the object to retrieve from sessionastate</param>
        /// <param name="redirTimeOutOnNull">redirect to default timeout page</param>
        /// <returns>an Object to cast</returns>
        public static Object _Session(string name, bool redirTimeOutOnNull)
        {
            Object result;
            result = HttpContext.Current.Session[name];
            if (result == null && redirTimeOutOnNull)
                HttpContext.Current.Response.Redirect(Config.SessionTimeOutUrl);
            return result;
        }

        public static Object _Application(string name)
        {
            Object result;
            result = HttpContext.Current.Application[name];
            if (result == null)
            {
                //HttpContext.Current.Response.Redirect(Config.SessionTimeOutUrl);
                throw new PigeonCms.CustomException("Missed _Application '" + name + "'", CustomExceptionSeverity.Warning, CustomExceptionLogLevel.Log);
            }
            else
            {
                return result;
            }
        }

        public static string _SessionID()
        {
            string res = "";
            if (HttpContext.Current.Session != null)
                res = HttpContext.Current.Session.SessionID;
            return res;
        }

        public static string BuildQueryString(string url, NameValueCollection parameters)
        {
            return BuildQueryString(url, parameters, false);
        }

        public static string BuildQueryString(string url, NameValueCollection parameters, bool addAuthority)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "";
                if (addAuthority)
                    url +=  HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
                url += HttpContext.Current.Request.Path;
            }

            StringBuilder sb = new StringBuilder(url);
            /*if (url.Contains(".aspx?"))
                sb.Append("&");
            else*/
            sb.Append("?");

            IEnumerator parEnumerator = parameters.GetEnumerator();
            while (parEnumerator.MoveNext())
            {
                // get the current query parameter
                string key = parEnumerator.Current.ToString();

                // insert the parameter into the url
                sb.Append(string.Format("{0}={1}&", key, HttpUtility.UrlEncode(parameters[key])));
            }

            var qry = HttpContext.Current.Request.QueryString;
            IEnumerator qryEnumerator = qry.GetEnumerator();
            while (qryEnumerator.MoveNext())
            {
                bool add = true;

                // get the current query parameter
                string key = qryEnumerator.Current.ToString();
                
                parEnumerator.Reset();
                while (parEnumerator.MoveNext())
                {
                    if (key == parEnumerator.Current.ToString())
                    {
                        add = false;
                        break;
                    }
                }

                if (add)
                    sb.Append(string.Format("{0}={1}&", key, HttpUtility.UrlEncode(qry[key])));
            }

            // remove the last ampersand
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// wrapper for HttpContext.Current.Request.QueryString
        /// </summary>
        /// <param name="name">the param name</param>
        /// <returns>the query string value. "" if param is null</returns>
        public static string _QueryString(string name)
        {
            string result = "";
            if (HttpContext.Current.Request.QueryString[name] != null)
                result = HttpContext.Current.Request.QueryString[name].ToString();
            return result;
        }

        /// <summary>
        /// wrapper for HttpContext.Current.Request post data
        /// </summary>
        /// <param name="name">the param name</param>
        /// <returns>the post string value. "" if param is null</returns>
        public static string _PostString(string name)
        {
            string result = "";
            if (HttpContext.Current.Request[name] != null)
                result = HttpContext.Current.Request[name].ToString();
            return result;
        }

        /// <summary>
        /// Generic Recursive overload of Page.FindControl
        /// </summary>
        /// <typeparam name="T">type of the control</typeparam>
        /// <param name="parentControl">root control</param>
        /// <param name="id">Cotrol Id identifier</param>
        /// <returns></returns>
        public static T FindControlRecursive<T>(Control parentControl, string id) where T: Control
        {
            T ctrl = default(T);

            if ((parentControl is T) && (parentControl.ID == id))
                return (T)parentControl;

            foreach (Control c in parentControl.Controls)
            {
                ctrl = FindControlRecursive<T>(c, id);
                if (ctrl != null) break;
            }
            return ctrl;
        }

        private static Control FindControlRecursive(Control parentControl, string id)
        {
            return FindControlRecursive<Control>(parentControl, id);
        }

        public enum TristateBool
        {
            False = 0,
            True,
            NotSet
        }

        public static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(sourceFolder))
                throw new Exception("source folder not found ("+ sourceFolder +")");

            if (!Directory.Exists( destFolder ))
                Directory.CreateDirectory( destFolder );
            string[] files = Directory.GetFiles( sourceFolder );
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }
            string[] folders = Directory.GetDirectories( sourceFolder );
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        public static void DeleteFolderContent(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch { }
                }
            }
        }

        public static long GetDirectorySize(string virtualPath)
        {
            long res = 0;
            string path = HttpContext.Current.Request.MapPath(virtualPath);

            if (Directory.Exists(path))
            {
                string[] a = Directory.GetFiles(path, "*.*");
                foreach (string name in a)
                {
                    FileInfo info = new FileInfo(name);
                    res += info.Length;
                }
            }
            return res;
        }

        /// <summary>
        /// file length in B, KB, MB, GB
        /// tnx to http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-using-net
        /// </summary>
        public static string GetFileHumanLength(long fileLength)
        {
            string result = "";
            if (fileLength > 0)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = fileLength;
                int order = 0;
                while (len >= 1024 && order + 1 < sizes.Length)
                {
                    order++;
                    len = len / 1024;
                }
                result = String.Format("{0:0.##} {1}", len, sizes[order]);
            }
            return result;
        }

        public static string GetFileExt(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }
    }
}
