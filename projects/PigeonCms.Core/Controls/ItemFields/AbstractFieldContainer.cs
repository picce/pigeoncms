using System;
using System.Web.UI;


namespace PigeonCms.Controls.ItemFields
{
	public abstract class AbstractFieldContainer : UserControl
	{
		public const int DefaultSize = 12;
		public const int DefaultControlSize = 12;

		public int? ControlLargeSize { get; set; }
		public int? ControlMediumSize { get; set; }
		public int? ControlSmallSize { get; set; }
		public int? ControlExtraSmallSize { get; set; }

		public int? LargeSize { get; set; }
		public int? MediumSize { get; set; }
		public int? SmallSize { get; set; }
		public int? ExtraSmallSize { get; set; }

		public string CSSClass { get; set; }

		public string Label { get; set; }
        public string LabelClass { get; set; }

		public Control InnerControl { get; set; }

		protected string Sizes
		{
			get
			{
				string sizes = "col-lg-" + (LargeSize.HasValue ? LargeSize.Value : DefaultSize);

				if (MediumSize.HasValue)
					sizes += " col-md-" + MediumSize.Value;

				if (SmallSize.HasValue)
					sizes += " col-sm-" + SmallSize.Value;

				if (ExtraSmallSize.HasValue)
					sizes += " col-xs-" + ExtraSmallSize.Value;

				return sizes;
			}
		}

		protected string ControlSizes
		{
			get
			{
				string sizes = "col-lg-" + (ControlLargeSize.HasValue ? ControlLargeSize.Value : DefaultControlSize);

				if (ControlMediumSize.HasValue)
					sizes += " col-md-" + ControlMediumSize.Value;

				if (ControlSmallSize.HasValue)
					sizes += " col-sm-" + ControlSmallSize.Value;

				if (ControlExtraSmallSize.HasValue)
					sizes += " col-xs-" + ControlExtraSmallSize.Value;

				return sizes;
			}
		}
	}
}
