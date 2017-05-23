using System;
using System.Collections.Generic;


//TODO remove file
namespace PigeonCms.Controls.ItemFields
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ImageFieldAttribute : ItemFieldAttribute
	{
		public string AllowedFileTypes { get; set; }

		public ImageFieldAttribute()
			: this(false, string.Empty)
		{

		}

		public ImageFieldAttribute(string allowedFileTypes)
			: this(false, allowedFileTypes)
		{

		}

		public ImageFieldAttribute(bool localized, string allowedFileTypes)
			: base(ItemFieldEditorType.Image, localized)
		{
			AllowedFileTypes = allowedFileTypes;
		}
	}
}
