using System;
using System.Text;
using System.IO;

namespace Westwind.InternetTools
{
	/// <summary>
	/// wwHttp Utility class to provide UrlEncoding without the need to use
	/// the System.Web libraries (too much overhead)
	/// </summary>
	public class wwHttpUtils 
	{
		/// <summary>
		/// UrlEncodes a string without the requirement for System.Web
		/// </summary>
		/// <param name="String"></param>
		/// <returns></returns>
		public static string UrlEncode(string InputString) 
		{
			StringReader sr = new StringReader( InputString);
			StringBuilder sb = new StringBuilder(  InputString.Length );

			while (true) 
			{
				int lnVal = sr.Read();
				if (lnVal == -1)
					break;
				char lcChar = (char) lnVal;

				if (lcChar >= 'a' && lcChar < 'z' || 
					lcChar >= 'A' && lcChar < 'Z' || 
					lcChar >= '0' && lcChar < '9')
					sb.Append(lcChar);
				else if (lcChar == ' ') 
					sb.Append("+");
				else
					sb.AppendFormat("%{0:X2}",lnVal);
			}

			return sb.ToString();
		}

		/// <summary>
		/// UrlDecodes a string without requiring System.Web
		/// </summary>
		/// <param name="InputString">String to decode.</param>
		/// <returns>decoded string</returns>
		public static string UrlDecode(string InputString)
		{
			char temp = ' ';
			StringReader sr = new StringReader(InputString);
			StringBuilder sb = new StringBuilder( InputString.Length );

			while (true) 
			{
				int lnVal = sr.Read();
				if (lnVal == -1)
					break;
				char TChar = (char) lnVal;
				if (TChar == '+')
					sb.Append(' ');
				else if(TChar == '%') 
				{
					// *** read the next 2 chars and parse into a char
					temp = (char) Int32.Parse(((char) sr.Read()).ToString() +  ((char) sr.Read()).ToString(),
												   System.Globalization.NumberStyles.HexNumber);
					sb.Append(temp);
				}
				else
					sb.Append(TChar);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Retrieves a value by key from a UrlEncoded string.
		/// </summary>
		/// <param name="UrlEncodedString">UrlEncoded String</param>
		/// <param name="Key">Key to retrieve value for</param>
		/// <returns>returns the value or "" if the key is not found or the value is blank</returns>
		public static string GetUrlEncodedKey(string UrlEncodedString, string Key) 
		{
			UrlEncodedString = "&" + UrlEncodedString + "&";

			int Index = UrlEncodedString.ToLower().IndexOf("&" + Key.ToLower() + "=");
			if (Index < 0)
				return "";
	
			int lnStart = Index + 2 + Key.Length;

			int Index2 = UrlEncodedString.IndexOf("&",lnStart);
			if (Index2 < 0)
				 return "";

			string Result = wwHttpUtils.UrlDecode(  UrlEncodedString.Substring(lnStart,Index2 - lnStart) );

			return Result;
		}

	}
}
