using System.Collections.Generic;
using System.Text;


namespace PigeonCms.Controls.ItemFields
{
	public class Translation : Dictionary<string, string>
	{
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			foreach (KeyValuePair<string, string> item in this)
			{
				result.Append(string.Format("{0}={1};", item.Key, item.Value));
			}

			return result.ToString();
		}

		public Translation()
			: base()
		{

		}

		public Translation(IDictionary<string, string> source)
			: base(source)
		{

		}
	}
}
